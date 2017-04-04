using UnityEngine;
using System.Collections;


namespace Mizuno {

	/// <summary>
	/// プレイヤー用クラス
	/// </summary>
	public class Player : MonoBehaviour {

		#region enum

		enum _PlayerActionMode {
			ACTION_NORMAL = 0,  // 通常
			ACTION_JUMP,        // ジャンプ
			ACTION_SPEEDUP,     // ダッシュ
			ACTION_SLIDING,     // スライディング

			ACTION_MAX,
		};

		#endregion enum


		#region variable

		//----- PC上のボタン
		[SerializeField]private KeyCode		m_KeyJump;
		[SerializeField]private KeyCode		m_KeyDash;
		[SerializeField]private KeyCode		m_KeySliding;

		// 現在の難易度
		int		m_nCurLevel = 0;

		//----- パーティクル
		[SerializeField]private ParticleSystem		m_DashParticle;

		//----- アクションの種類
		int	m_cActionMode;
		bool	m_bAction = false;

		//----- ジャンプ関連
		// 地面の高さ
		[SerializeField, Range(0.0f, 3.0f)] private float	m_fGroundHeight = 0.0f;
		// 物理公式
		[SerializeField]private float	m_fV0 = 1.0f;		// 初速度
		float			m_fTime = 0.0f;		// 時間
		[SerializeField]private float	m_fGravity = 9.8f;	// 重力
		bool			m_bJumpAnim = false;		// ジャンプアニメーション中（アニメーションスクリプト内で操作
		public bool JumpAnim { get {return m_bJumpAnim || m_bOn;} }

		//----- スライディング関連
		BoxCollider		m_Collider;
		bool			m_bSlidingAnim = false;		// スライディングアニメーション中（アニメーションスクリプト内で操作

		//----- ダッシュ関連
		bool				m_bDash = false;
		[SerializeField]private float[]		m_fSpeedDash = {1.0f,1.0f,1.0f};	// 走る速度
		public float DashSpeed{ get{return m_fSpeedDash[m_nCurLevel];} }
		[SerializeField]private float[]		m_fSpeedAdd = {1.0f, 1.0f, 1.0f};	// 加算速度
		[SerializeField]private float[]		m_fSpeedMin = {1.5f, 1.5f, 1.5f};	// 最低速度
		[SerializeField]private float[]		m_fSpeedMax = {5.0f, 5.0f, 5.0f};	// 最大速度
		public float DashSpeedMax{ get{ return m_fSpeedMax[m_nCurLevel]; } }
		Mover		m_Move = null;
		bool		m_bDashSE = false;


		//----- 当たり判定情報
		bool	m_bHit = false;
		bool	m_bOn  = false;

		//----- アニメーション
		Animator	m_Anim;
		[SerializeField, Range(0.0f, 5.0f)]private float	m_fAnimRunMinTime		= 1.0f;
		[SerializeField, Range(0.0f, 5.0f)]private float	m_fAnimRunMaxTime		= 1.0f;

		//----- クリア
		bool	m_bClear = false;
		float	m_fAfterTime = 0.0f;
		public void Clear() { m_bClear = true; m_Collider.enabled = false; }

		#endregion variable


		#region method

		/// <summary>
		/// Windows上での操作
		/// </summary>
		void WindowsInput() {
			// ジャンプ
			if (Input.GetKeyDown(m_KeyJump))
				Jump();

			// スライディング
			if (Input.GetKeyDown(m_KeySliding))
				Sliding();

			// ダッシュ
			if (Input.GetKey(m_KeyDash))
				SpeedUp();
			else
				SpeedDown();
		}

		/// <summary>
		/// ジャンプの動き
		/// </summary>
		void JumpMove() {
			// ボタン入力時
			if (m_bAction) {
				m_fTime = 0.0f;
				m_bAction = false;
				m_DashParticle.Stop();
				m_Anim.SetTrigger("tJump");
				m_Anim.speed = 1.0f;
				m_bJumpAnim = true;
				SoundManager.Instance.PlaySE(9);
				m_bDashSE = true;
			}

			// 障害物の上
			if (m_bOn) return;

			// 時間加算
			m_fTime += Time.deltaTime;

			// 座標変更
			Vector3 pos = transform.position;
			pos.y = m_fV0 * m_fTime - (1.0f / 2.0f * m_fGravity * (m_fTime * m_fTime));
			pos.y += m_fGroundHeight;
			// 地面判定
			if (pos.y <= m_fGroundHeight) {
				pos.y = m_fGroundHeight;

				if (m_bDashSE) {
					SoundManager.Instance.PlaySE(10);
					m_bDashSE = false;
				}

				// ジャンプアニメーション終了
				if (!m_bJumpAnim) {
					if (m_bDash)
						ChangeAction(_PlayerActionMode.ACTION_SPEEDUP);
					else
						ChangeAction(_PlayerActionMode.ACTION_NORMAL);
					m_DashParticle.Play();
				}
			}

			// 更新
			transform.position = pos;
		}

		/// <summary>
		/// スライディングの動き
		/// </summary>
		void SlidingMove() {
			// ボタン入力時
			if (m_bAction) {
				m_fTime = 0.0f;
				m_bAction = false;
				// あたり判定変更
				ChangeCollision(true);
				m_DashParticle.Stop();
				m_Anim.SetTrigger("tSliding");
				m_bSlidingAnim = true;

				// サウンド再生
				SoundManager.Instance.PlaySE(7);
				m_Anim.speed = 1.0f;
			}
			// 1つ出す
			m_DashParticle.Emit(1);

			// 時間加算
			m_fTime += Time.deltaTime;

			// 一定時間経過後戻す
			if (!m_bSlidingAnim) {
				if (m_bDash)
					ChangeAction(_PlayerActionMode.ACTION_SPEEDUP);
				else
					ChangeAction(_PlayerActionMode.ACTION_NORMAL);
				ChangeCollision(false);
				m_DashParticle.Play();
			}
		}

		/// <summary>
		/// ダッシュの動き
		/// </summary>
		void DashMove() {
			// スピードアップ
			if (m_bDash) ChangeSpeed(m_fSpeedAdd[m_nCurLevel]);
			else ChangeSpeed(-m_fSpeedAdd[m_nCurLevel]);

			// アニメーション速度の変更
			m_Anim.speed = AnimSpeed(m_fAnimRunMinTime, m_fAnimRunMaxTime);
		}

		/// <summary>
		/// 速度変更
		/// </summary>
		/// <param name="speed">速度</param>
		void ChangeSpeed(float speed) {
			m_fSpeedDash[m_nCurLevel] += speed;

			// 値制限
			m_fSpeedDash[m_nCurLevel] = Mathf.Clamp(m_fSpeedDash[m_nCurLevel], m_fSpeedMin[m_nCurLevel], m_fSpeedMax[m_nCurLevel]);
		}

		/// <summary>
		/// アクションの変更
		/// </summary>
		/// <param name="_action">変更するアクション</param>
		void ChangeAction(_PlayerActionMode _action) {
			m_cActionMode = (int)_action;
		}


		/// <summary>
		/// アニメーション速度計算
		/// </summary>
		/// <returns>新しい速度</returns>
		/// <param name="Min">最低速度</param>
		/// <param name="Max">最高速度</param>
		float AnimSpeed(float Min, float Max) {
			// 現在の移動速度の割合
			float SpeedRate = (m_fSpeedDash[m_nCurLevel] - m_fSpeedMin[m_nCurLevel]) / (m_fSpeedMax[m_nCurLevel] - m_fSpeedMin[m_nCurLevel]);
			// アニメーションの加速度
			float AnimAccel = SpeedRate * (Max - Min);

			return AnimAccel + Min;
		}


		/// <summary>
		/// ジャンプ入力
		/// </summary>
		public void Jump() {
			// ジャンプ中か確認
			if (m_cActionMode == (int)_PlayerActionMode.ACTION_JUMP ||
				m_cActionMode == (int)_PlayerActionMode.ACTION_SLIDING) {
				return;
			}

			// 使用確認
			if (!this.enabled)
				return;

			ChangeAction(_PlayerActionMode.ACTION_JUMP);
			m_bAction = true;
		}

		/// <summary>
		/// 速度アップ入力
		/// </summary>
		public void SpeedUp() {
			m_bDash = true;

			// 実行可能かチェック
			if (m_cActionMode != (int)_PlayerActionMode.ACTION_NORMAL &&
				m_cActionMode != (int)_PlayerActionMode.ACTION_SPEEDUP) {
				return;
			}

			ChangeAction(_PlayerActionMode.ACTION_SPEEDUP);
		}

		/// <summary>
		/// 速度ダウン入力
		/// </summary>
		public void SpeedDown() {
			m_bDash = false;
		}

		/// <summary>
		/// スライディング入力
		/// </summary>
		public void Sliding() {
			// 実行可能かチェック
			if (m_cActionMode == (int)_PlayerActionMode.ACTION_SLIDING ||
			   	m_cActionMode == (int)_PlayerActionMode.ACTION_JUMP) {
				return;
			}
			ChangeAction(_PlayerActionMode.ACTION_SLIDING);
			m_bAction = true;
		}

		/// <summary>
		/// スライディング用当たり判定の変更
		/// </summary>
		/// <param name="Trig">スライディング確認</param>
		void ChangeCollision(bool Trig) {
			if (Trig) {
				// スライディング中
				m_Collider.center = new Vector3(0f, 0.25f, 0f);
				m_Collider.size = new Vector3(1f, 0.5f, 2f);
			} else {
				// 通常
				m_Collider.center = new Vector3(0f, 1f, 0f);
				m_Collider.size = new Vector3(1f, 2f, 1f);
			}
		}

		/// <summary>
		/// ジャンプアニメーション終了
		/// </summary>
		public void JumpAnimEnd() {
			m_bJumpAnim = false;
		}

		/// <summary>
		/// スライディングアニメーション終了
		/// </summary>
		public void SlidingAnimEnd() {
			m_bSlidingAnim = false;
		}

		/// <summary>
		/// クリア後の行動
		/// </summary>
		public void ClearMove() {
			m_fAfterTime += Time.deltaTime;

			if (m_fAfterTime >= 8.0f)
				Destroy(this);

			m_Move.LocalMove(DashSpeed * Time.deltaTime);

			if (m_bJumpAnim)
				JumpMove();
		}

		/// <summary>
		/// ヒット時呼び出し
		/// </summary>
		void Hit() {
			m_bHit = true;
		}

		/// <summary>
		/// ヒット中に障害物が消えた
		/// </summary>
		public void DeleteObst() {
			m_bHit = false;
			m_bOn = false;
		}

		#endregion method


		#region unity method

		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake() {
			m_Move = GetComponent<Mover> ();
			m_Anim = GetComponent<Animator> ();
			m_Collider = GetComponent<BoxCollider> ();
			transform.position = new Vector3 (transform.position.x, m_fGroundHeight * 1.5f, transform.position.z);
		}


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			// 現在の難易度を取得
			m_nCurLevel = (int)LevelController.Instance.GetCurLevel();

			// 初期アクション
			m_cActionMode = (int)_PlayerActionMode.ACTION_SPEEDUP;

			// アクションの時間
			m_fTime = 0.0f;

			// 当たり判定
			ChangeCollision(false);
			m_bOn = false;
			m_bHit = false;

			// ダッシュ関連
			m_fSpeedDash[m_nCurLevel] = m_fSpeedMin[m_nCurLevel];
		}

		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// クリア
			if (m_bClear) {
				ClearMove ();
				return;
			}

			// PC上での操作
			if (Application.isEditor ||
				Application.platform == RuntimePlatform.WindowsPlayer ||
				Application.platform == RuntimePlatform.OSXPlayer) {
				WindowsInput ();
			}


			//----- 前方へ移動
			if (!m_bHit) {
				m_Move.LocalMove (m_fSpeedDash[m_nCurLevel] * Time.deltaTime);
			}

			//----- アクション
			switch (m_cActionMode) {
			// ジャンプ
			case (int)_PlayerActionMode.ACTION_JUMP:
				JumpMove ();
				return;

			// 通常状態　ダッシュ
			case (int)_PlayerActionMode.ACTION_NORMAL:
			case (int)_PlayerActionMode.ACTION_SPEEDUP:
				DashMove ();
				return;

			// スライディング
			case (int)_PlayerActionMode.ACTION_SLIDING:
				SlidingMove ();
				return;
			};
		}

		/// <summary>
		/// 当たり判定であたった瞬間
		/// </summary>
		/// <param name="col">当たった相手</param>
		void OnTriggerEnter(Collider col) {
			//障害物と当たった場合
			if (col.gameObject.tag == "Obstacle") {
				BoxCollider Pcol = m_Collider;

				//プレイヤーが上から来たのか?            
				if (transform.position.y + Pcol.center.y - Pcol.size.y * 0.5f >= col.transform.position.y + col.transform.localScale.y * 0.5f) {
					//上から
					//				Debug.Log ("のった");
					m_bOn = true;
					SoundManager.Instance.PlaySE(10);
				} else {
					Hit();
					//				Debug.Log ("あたった");
				}
			}
		}

		/// <summary>
		/// 当たり判定で離れた瞬間
		/// </summary>
		/// <param name="col">当たった相手</param>
		void OnTriggerExit(Collider col) {
			// 障害物と
			if (col.gameObject.tag == "Obstacle" || col.gameObject.tag == "HitObstacle") {
				m_bHit = false;
				m_bOn = false;
			}
		}


		#endregion unity method
	}
}
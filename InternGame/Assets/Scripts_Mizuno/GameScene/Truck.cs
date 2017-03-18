using UnityEngine;
using System.Collections;
using Asada;


namespace Mizuno {

	/// <summary>
	/// トラックの動き
	/// </summary>
	public class Truck : MonoBehaviour {

		enum _MoveType {
			TYPE_NONE = 0,	// 通常
			TYPE_CHASE,		// 並走中
			TYPE_CHASEEND,	// 並走終了
		};

		enum _DeathType {
			DEATH_NORMAL = 0,	// 吹き飛ばし
			DEATH_PRESS,		// ゴールまでお供
			DEATH_SMASH,		// つぶれる
			//DEATH_BURST,		// はじける

			DEATH_MAX,
		};

		//現在のレベル
		int m_nNowLevel = 0;

		// 移動速度
		[SerializeField]private float[]		m_fSpeed = {1.0f,1.0f,1.0f};
		public float Speed{ get{ return m_fSpeed[m_nNowLevel]; } }

		// 加速度
		[SerializeField]private float[]		m_fAccelerate = {3.0f,3.0f,3.0f};

		// 移動
		Mover		m_Mover = null;

		// ゲーム時間
		[SerializeField]private GameTimer	m_Timer;

		[SerializeField]private int m_nGoalBeforeCnt;				//ゴールの何個前に移動するか
		[SerializeField]private ParticleSystem[] CollisionParticle;	//Goalとぶつかったときに出すParticle

		//----- チェイス用
		[SerializeField, Range(0.0f, 20.0f)]private float	m_fChaseBeginTime = 10.0f;
		[SerializeField]private Player 	m_Target;
		[SerializeField]private float	m_fChaseDist;	// 追跡開始距離
		_MoveType						m_MoveType = _MoveType.TYPE_NONE;
		float							m_fChaseTime = 0.0f;
		[SerializeField]private float	m_fChaseMaxTime = 1.0f;

		// 煽りのクラクション
		[SerializeField]private float	m_fFanInterval = 1.0f;		// 連続間の間隔
		[SerializeField]private float	m_fFanNext = 2.0f;			// 鳴らし終わった後の次までの時間
		float							m_fFanTimeInterval = 0.0f;	// 連続間の計測用
		float 							m_fFanTimeNext = 0.0f;		// 次までの計測用
		int								m_nFanNum;


		/// <summary>
		/// 初期化
		/// </summary>
		void Start() {
			//現在のレベル取得
			m_nNowLevel = (int)LevelController.Instance.GetCurLevel();

			m_Mover = GetComponent<Mover> ();

			for (int i = 0; i < CollisionParticle.Length; i++) {
				CollisionParticle [i].Stop ();
			}
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update() {
			Driving ();
		}

		/// <summary>
		/// 前方へ進む
		/// </summary>
		void Driving() {
			float time;

			// 時間計測
			m_fChaseTime += Time.deltaTime;

			switch (m_MoveType) {
			// 通常
			case _MoveType.TYPE_NONE:
				// 加速度
				time = m_Timer.GameTime * m_fAccelerate [m_nNowLevel];
				m_Mover.LocalMove ((m_fSpeed [m_nNowLevel] + time) * Time.deltaTime);

				// 並走距離の確認
				if (m_fChaseBeginTime <= m_Timer.GameTime) {
					m_MoveType = CheckTargetDist ();
					m_fChaseTime = 0.0f;

					if (m_MoveType != _MoveType.TYPE_NONE) {
						SoundManager.Instance.RePlaySE (0);
					}
				}
				break;

			// 並走中
			case _MoveType.TYPE_CHASE:
				m_Mover.LocalMove (m_Target.DashSpeedMax * Time.deltaTime);

				FanSound ();

				if (m_fChaseTime >= m_fChaseMaxTime) {
					m_MoveType = _MoveType.TYPE_CHASEEND;
					m_fChaseTime = 0;
				}
				break;

			// 並走終了
			case _MoveType.TYPE_CHASEEND:
				// 加速度
				time = m_fChaseTime * m_fAccelerate[m_nNowLevel];
				m_Mover.LocalMove ((m_Target.DashSpeedMax + time) * Time.deltaTime);
				break;
			};
		}

		/// <summary>
		/// プレイヤーとの距離を計算
		/// </summary>
		_MoveType CheckTargetDist() {
			float Dist = Vector3.Distance (transform.position, m_Target.transform.position);
			return Dist <= m_fChaseDist ? _MoveType.TYPE_CHASE : _MoveType.TYPE_NONE;
		}

		/// <summary>
		/// 煽りのクラクション
		/// </summary>
		void FanSound() {
			// 回数再設定
			if (m_nFanNum <= 0) {
				m_fFanTimeNext += Time.deltaTime;
				if (m_fFanTimeNext >= m_fFanNext) {
					m_nFanNum = Random.Range (2, 5);
					m_fFanTimeNext = 0.0f;
					m_fFanTimeInterval = 0.0f;
				}
				return;
			}

			m_fFanTimeInterval += Time.deltaTime;
			if (m_fFanTimeInterval >= m_fFanInterval) {
				m_fFanTimeInterval = 0.0f;
				--m_nFanNum;
				SoundManager.Instance.RePlaySE (0);
			}
		}

		/// <summary>
		/// ゴールまでのチップ数
		/// </summary>
		/// <returns>チップ数</returns>
		int UntilGoal() {
			StageController StageCont = GameObject.Find ("StageController").GetComponent<StageController> ();
			// 自身のチップ番号
			int nIndex = (int)(transform.position.z / StageCont.StageTipSize);

			return StageCont.GoalTipIndex - nIndex;
		}

		/// <summary>
		/// 演出を決定
		/// </summary>
		_DeathType DeathAnalys() {
//			_DeathType type = _DeathType.DEATH_SMASH;
			_DeathType type = (_DeathType)Random.Range(0, (int)_DeathType.DEATH_MAX);

			switch (type) {
			case _DeathType.DEATH_NORMAL:
				//移動先より遠かったら移動する
				if (3 > UntilGoal()) {
					return _DeathType.DEATH_SMASH;
				}
				break;

			// ゴールまでお供
			case _DeathType.DEATH_PRESS:
				//移動先より遠かったら移動する
				if (5 > UntilGoal()) {
					return _DeathType.DEATH_SMASH;
				}
				break;

			// つぶれる
			case _DeathType.DEATH_SMASH:
				// 空中確認
				if (GameObject.Find ("Player").GetComponent<Player> ().JumpAnim) {
					return _DeathType.DEATH_NORMAL;
				}
				break;
			}

			return type;
		}

		/// <summary>
		/// ゲームオーバー演出を管理
		/// </summary>
		void DeathJudge( GameObject Obj ) {
			switch (DeathAnalys ()) {
			// 吹き飛ばし
			case _DeathType.DEATH_NORMAL:
				Obj.gameObject.GetComponent<Animator> ().enabled = false;
				Camera.main.GetComponent<LookCamera> ().enabled = true;
				break;

			// ゴールまでお供
			case _DeathType.DEATH_PRESS:
				GetComponent<TruckPress> ().enabled = true;
				break;
			
			// つぶれる
			case _DeathType.DEATH_SMASH:
				GetComponent<TruckSmash> ().enabled = true;
				if (UntilGoal () <= 1) {
					GetComponent<TruckSmash> ().NearGoal = true;
				}
				break;

			//case _DeathType.DEATH_BURST:
				//GameObject.Find ("Character1_Reference").SetActive (false);
				//GameObject.Find ("mesh_root").SetActive (false);
				//Obj.transform.Translate (0.0f, 0.0f, 10.0f);
				//
				//m_niku.transform.position = Obj.transform.position;
				//Vector3 pos = m_niku.transform.position;
				//pos.y += 10.0f;
				//pos.z += 15.0f;
				//m_niku.transform.position = pos;
				//m_niku.SetActive (true);
				//this.enabled = false;
				//break;
			};
		}

		/// <summary>
		/// パーティ来るを出す
		/// </summary>
		public void OnParticle() {
			for (int i = 0; i < CollisionParticle.Length; i++) {
				CollisionParticle [i].Play ();
			}
		}


		/// <summary>
		/// 当たり判定
		/// </summary>
		/// <param name="col">当たった相手</param>
		void OnTriggerEnter(Collider col) {
			if (!this.enabled)
				return;

			// プレイヤーを倒した
			if (col.gameObject.tag == "Player") {
				// 自身はもう必要ない
				this.enabled = false;

				// ゲームオーバー
				DeathJudge( col.gameObject );

				// サウンド再生
				SoundManager.Instance.PlaySE(6);
			}

			// ゴールした
			if (col.gameObject.tag == "Goal") {
				// カメラを揺らす
				Camera.main.GetComponent<ChaseCamera>().enabled = false;
				Camera.main.GetComponent<ShakeCamera> ().enabled = true;

				// パーティクル
				OnParticle ();

				//揺れを止める
				gameObject.GetComponent<ShakeTruck>().enabled = false;

				// 自身を止める
				this.enabled = false;
			}
		}



		/// <summary>
		/// プレイヤーがゴールした時
		/// </summary>
		public void GoalMove()
		{
			//ステージコントローラ取得
			StageController StaCol =  GameObject.Find("StageController").GetComponent<StageController>();

			//自分の場所を計算
			int nIndex = (int)(transform.position.z / StaCol.StageTipSize);


			//移動先より遠かったら移動する
			if (nIndex < StaCol.GoalTipIndex - m_nGoalBeforeCnt) {
				transform.position = new Vector3 (transform.position.x,
												  transform.position.y,
					(StaCol.GoalTipIndex - m_nGoalBeforeCnt) * StaCol.StageTipSize);
			}
		}
	}
}
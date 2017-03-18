using UnityEngine;
using System.Collections;
using Asada;


namespace Mizuno {

	public class Car : MonoBehaviour {

		// 現在の難易度
		int		m_nCurLevel = 0;

		//----- 移動
		Mover					m_Mover;				// 管理者
		[SerializeField]private float[]	m_fMoveSpeed;	// 速度

		// プレイヤー
		GameObject				m_Player;

		//----- 攻撃
		[SerializeField]float	m_fAttackDist = 10.0f;	// れんじ
		bool					m_bAttack = false;		// フラグ

		//----- ジャンプ
		float								m_fInitHeight;		// 初期高さ
		[SerializeField]private float[]		m_fV0;				// 初速度
		float								m_fTime = 0.0f;		// 経過時間
		[SerializeField]private float		m_fGravity = 9.8f;	// 重力

		// 警告
		bool	m_bCaution = false;
		[SerializeField]private float[]		m_fCautionDist;		// 警告距離

		// サウンド
		bool	m_bSE = false;


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			// 現在の難易度を取得
			m_nCurLevel = (int)LevelController.Instance.GetCurLevel();
			
			transform.rotation = Quaternion.Euler (0,90,0);

			m_Mover = GetComponent<Mover> ();
			//if (!m_Mover)
			//	Debug.Log (this.name + " : CarにMoverがついてないよ");

			m_Player = GameObject.Find ("Player");
			//if (!m_Player)
			//	Debug.Log (this.name + " : CarにPlayerが見つけられないよ");

			m_fInitHeight = transform.position.y;
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 距離算出
			float Dist = Vector3.Distance (m_Player.transform.position, this.transform.position);

			// 警告
			Caution(Dist);

			// 攻撃判定
			if (!m_bAttack && Dist <= m_fAttackDist) {
				m_bAttack = true;
				gameObject.GetComponent<DeleteObstacle> ().enabled = true;
			}

			// 攻撃確認
			if ( !m_bAttack )	return;

			// X移動
			m_Mover.LocalMove (m_fMoveSpeed[m_nCurLevel] * Time.deltaTime);

			// ジャンプ
			m_fTime += Time.deltaTime;
			Vector3 pos = transform.position;
			pos.y = m_fV0[m_nCurLevel] * m_fTime - (1.0f / 2.0f * m_fGravity * (m_fTime * m_fTime));
			pos.y += m_fInitHeight;
			transform.position = pos;
		}

		/// <summary>
		/// 警告判定
		/// </summary>
		/// <param name="Dist">自身とプレイヤーとの距離</param>
		void Caution( float Dist ) {
			if (m_bCaution ||
				Dist >= m_fCautionDist [m_nCurLevel])
				return;

			m_bCaution = true;

			// 警告を表示
			GameObject.Find("CautionImage").GetComponent<CautionImage>().Display();
		}

		/// <summary>
		/// 当たり判定（ヒット時
		/// </summary>
		/// <param name="col">相手オブジェクト</param>
		void OnTriggerEnter(Collider col) {
			if (col.gameObject.tag != "Player")
				return;

			if (m_bSE)
				return;

			SoundManager.Instance.PlaySE (6);
			m_bSE = true;
		}
	}

}
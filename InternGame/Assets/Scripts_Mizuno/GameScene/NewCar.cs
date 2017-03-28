using UnityEngine;
using System.Collections;

using Asada;

namespace Mizuno {

	/// <summary>
	/// 車用クラス改
	/// </summary>
	public class NewCar : MonoBehaviour {

		#region variable

		int		m_nCurLevel = 0;

		Mover		m_Mover;
		[SerializeField]private float[]	m_fMoveSpeed;			// 通常速度
		[SerializeField]private float	m_fMoveDist = 10.0f;	// 動き出す範囲
		[SerializeField]private float[]	m_fRushSpeed;			// 突進速度
		[SerializeField]private float	m_fRushDist = 5.0f;		// 突進すだす範囲

		GameObject			m_Player;

		bool		m_bMove = false;
		bool		m_bRush = false;

		//----- ジャンプ
		float								m_fInitHeight;
		[SerializeField]private float[]		m_fV0;
		float								m_fJumpTime = 0.0f;
		[SerializeField]private float		m_fGravity = 9.8f;

		bool	m_bCaution = false;
		[SerializeField]private float[]		m_fCautionDist;

		bool	m_bSE = false;

		#endregion variable


		#region method

		/// <summary>
		/// 移動判定
		/// </summary>
		/// <param name="Dist">自身とプレイヤーとの距離</param>
		void MoveCheck(float Dist) {
			if (m_bMove)
				return;

			if (Dist > m_fMoveDist)
				return;

			m_bMove = true;
			gameObject.GetComponent<DeleteObstacle>().enabled = true;
			//			Debug.Log ("移動開始します");
		}

		/// <summary>
		/// 突進判定
		/// </summary>
		/// <param name="Dist">自身とプレイヤーとの距離</param>
		void RushCheck(float Dist) {
			if (!m_bMove)
				return;

			if (m_bRush)
				return;

			if (Dist > m_fRushDist)
				return;

			m_bRush = true;
			//			Debug.Log ("突進開始します");
		}

		/// <summary>
		/// 前方へ移動
		/// </summary>
		void ForwardMove() {
			// 移動中
			if (m_bMove && !m_bRush) {
				m_Mover.LocalMove(m_fMoveSpeed[m_nCurLevel] * Time.deltaTime);
			} else
			// 突進中
			if (m_bMove && m_bRush) {
				m_Mover.LocalMove(m_fRushSpeed[m_nCurLevel] * Time.deltaTime);
				JumpMove();
			}
		}

		/// <summary>
		/// ジャンプ移動
		/// </summary>
		void JumpMove() {
			m_fJumpTime += Time.deltaTime;
			Vector3 pos = transform.position;
			pos.y = m_fV0[m_nCurLevel] * m_fJumpTime - (1.0f / 2.0f * m_fGravity * (m_fJumpTime * m_fJumpTime));
			pos.y += m_fInitHeight;
			transform.position = pos;
		}

		/// <summary>
		/// 警告判定
		/// </summary>
		/// <param name="Dist">自身とプレイヤーとの距離</param>
		void CautionCheck(float Dist) {
			if (m_bCaution ||
				Dist >= m_fCautionDist[m_nCurLevel])
				return;

			m_bCaution = true;

			GameObject.Find("CautionImage").GetComponent<CautionImage>().Display();
		}

		#endregion method


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_nCurLevel = (int)LevelController.Instance.GetCurLevel ();

			transform.rotation = Quaternion.Euler (0, 90, 0);

			m_Mover = GetComponent<Mover> ();
			m_Player = GameObject.Find ("Player");
			m_fInitHeight = transform.position.y;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			float Dist = Vector3.Distance (m_Player.transform.position, this.transform.position);

			CautionCheck (Dist);

			// 移動判定
			MoveCheck( Dist );

			// 突進判定
			RushCheck(  Dist );

			// 前方移動
			ForwardMove();
		}

		/// <summary>
		/// 当たり判定
		/// </summary>
		/// <param name="col">相手オブジェクト</param>
		void OnTriggerEnter(Collider col) {
			if (col.gameObject.tag != "Player")
				return;

			if (m_bSE)
				return;

			SoundManager.Instance.PlaySE(6);
			m_bSE = true;

			col.gameObject.GetComponent<Player>().enabled = false;
			col.gameObject.GetComponent<PlayerImpactCamera>().enabled = true;
			col.gameObject.GetComponent<BoxCollider>().enabled = false;
		}

		#endregion unity method
	}
}
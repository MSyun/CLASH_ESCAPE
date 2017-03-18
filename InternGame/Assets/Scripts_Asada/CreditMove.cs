using UnityEngine;
using System.Collections;
using Mizuno;

namespace Asada
{
	/// <summary>
	/// クレジットシーンでの移動処理
	/// </summary>
	public class CreditMove : MonoBehaviour {

		// 地面の高さ
		[SerializeField, Range(0.0f, 3.0f)] private float	m_fGroundHeight = 0.0f;
		[SerializeField]private float		m_fSpeedDash = 1.0f;	// 走る速度
		Mover		m_Move = null;

		[SerializeField]private float		m_fSpeedMax = 2.0f;
		[SerializeField]private float		m_fSpeedMin = 1.0f;
		[SerializeField]private float		m_fSpeedAdd = 0.3f;
		static bool	m_bDash = false;




		// Use this for initialization
		void Start () {
			m_Move = GetComponent<Mover> ();
			transform.position = new Vector3 (transform.position.x, m_fGroundHeight * 1.5f, transform.position.z);
		}
		
		// Update is called once per frame
		void Update () {
			DashMove ();
			m_Move.LocalMove (m_fSpeedDash * Time.deltaTime);
		}

		/// <summary>
		/// 速度の設定
		/// </summary>
		void DashMove() {
			if (m_bDash)	ChangeSpeed (m_fSpeedAdd);
			else			ChangeSpeed (-m_fSpeedAdd);
		}

		/// <summary>
		/// 速度の変更
		/// </summary>
		/// <param name="speed">加速度</param>
		void ChangeSpeed(float speed) {
			m_fSpeedDash += speed;

			// 値制限
			m_fSpeedDash = Mathf.Clamp( m_fSpeedDash, m_fSpeedMin, m_fSpeedMax );
		}

		/// <summary>
		/// 速度アップ
		/// </summary>
		public void SpeedUp() {
			m_bDash = true;
		}

		/// <summary>
		/// 速度ダウン
		/// </summary>
		public void SpeedDown() {
			m_bDash = false;
		}
	}
}
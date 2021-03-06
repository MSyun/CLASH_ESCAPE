﻿using UnityEngine;
using System.Collections;

namespace Mizuno {

	/// <summary>
	/// 障害物用あたり判定クラス
	/// </summary>
	public class ObstCollision : MonoBehaviour {

		#region variable

		Player m_Player;

		bool	m_bHitPlayer = false;

		#endregion variable


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Player = GameObject.Find ("Player").GetComponent<Player> ();
		}

		/// <summary>
		/// ヒット時あたり判定
		/// </summary>
		/// <param name="col">当たった相手</param>
		void OnTriggerEnter(Collider col) {
			if (col.gameObject.tag == "Truck") {
				if (m_bHitPlayer)
					m_Player.DeleteObst();
			} else
			if (col.gameObject.tag == "Player") {
				m_bHitPlayer = true;
			}
		}

		/// <summary>
		/// 離れたときあたり判定
		/// </summary>
		/// <param name="col">当たった相手</param>
		void OnTriggerExit(Collider col ) {
			if (col.gameObject.tag == "Player") {
				m_bHitPlayer = false;
			}
		}

		#endregion unity method
	}
}
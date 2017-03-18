﻿using UnityEngine;
using System.Collections;


namespace Mizuno {

	public class ClearPlayer : MonoBehaviour {

		Mover		m_Mover;
		Player		m_Player;

		float		m_fTime = 0.0f;

		[SerializeField]private float	m_fMoveTime = 1.0f;

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Mover = GetComponent<Mover> ();
			m_Player = GetComponent<Player> ();
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			m_fTime += Time.deltaTime;

			if (m_fTime >= m_fMoveTime) {
				Destroy (this);
			}

			m_Mover.LocalMove (m_Player.DashSpeed * Time.deltaTime);
		}
	}

}
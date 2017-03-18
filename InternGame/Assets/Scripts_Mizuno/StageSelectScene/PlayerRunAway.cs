using UnityEngine;
using System.Collections;


namespace Mizuno {

	public class PlayerRunAway : MonoBehaviour {

		// 移動
		Mover			m_Mover;

		// アニメータ
		Animator		m_Anim = null;

		// 移動速度
		[SerializeField]private float		m_fRunSpeed = 10.0f;

		//----- 回転
		float					m_fRotInit;		// 初期角度
		[SerializeField]float	m_fRotAim;		// 角度
		[SerializeField]float	m_fRotTime;		// 目的までの回転時間
		float					m_fTime = 0.0f;	// 現在の時間

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			//----- 移動
			m_Mover = GetComponent<Mover> ();
			if (!m_Mover)
				Debug.Log (this.name + " : PlayerRunAwayのMoverがないよ!");

			//----- アニメーション
			m_Anim = GetComponent<Animator>();
			if (!m_Anim)
				Debug.Log (this.name + " : PlayerRunAwayのAnimatorがないよ!");
			m_Anim.SetTrigger ("tRun");		// 走るアニメーションへ

			//----- 初期角度
			m_fRotInit = transform.eulerAngles.y;
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			//----- 回転
			m_fTime += Time.deltaTime;
			float time = m_fTime / m_fRotTime;		// 割合計算(0.0 ~ 1.0
			Vector3 angle = transform.eulerAngles;
			// 角度の線形保管
			angle.y = Mathf.LerpAngle (m_fRotInit, m_fRotAim, time);
			transform.eulerAngles = angle;

			//----- 移動
			m_Mover.LocalMove (m_fRunSpeed * Time.deltaTime);
		}
	}

}
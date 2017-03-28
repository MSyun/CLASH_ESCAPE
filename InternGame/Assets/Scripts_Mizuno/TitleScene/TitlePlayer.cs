using UnityEngine;
using System.Collections;

using Mizuno;

namespace Mizuno {

	/// <summary>
	/// タイトルシーン用プレイヤー
	/// </summary>
	public class TitlePlayer : MonoBehaviour {

		#region variable

		//----- 移動
		Mover m_Mover;
		// 速度
		[SerializeField]private float		m_fSpeed = 10.0f;

		// アニメータ
		Animator		m_Anim;

		bool			m_bTrig = false;

		[SerializeField]private Transform[]		m_Neck;
		[SerializeField]private float[] 		m_fNeckAngle;

		//----- 回転
		float					m_fRotInit;		// 初期角度
		[SerializeField]float	m_fRotAim;		// 角度
		[SerializeField]float	m_fRotTime;		// 目的までの回転時間
		float					m_fTime = 0.0f; // 現在の時間

		#endregion variable


		#region method

		/// <summary>
		/// 行動の変更
		/// </summary>
		public void Change() {
			m_bTrig = true;
			// アニメーション
			m_Anim.SetTrigger("tWalk");
		}

		#endregion method


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Mover = GetComponent<Mover> ();
			if (!m_Mover)
				Debug.Log (this.name + " : TitlePlayerのMoverがないよ!");

			m_Anim = GetComponent<Animator> ();
			if (!m_Anim)
				Debug.Log (this.name + " : TitlePlayerのAnimatorがないよ!");

			//----- 初期角度
			m_fRotInit = transform.eulerAngles.y;
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 例外処理
			if (!m_bTrig)	return;

			//----- 回転
			m_fTime += Time.deltaTime;
			float time = m_fTime / m_fRotTime;
			Vector3 angle = transform.eulerAngles;
			angle.y = Mathf.LerpAngle (m_fRotInit, m_fRotAim, time);
			transform.eulerAngles = angle;

			// 移動
			m_Mover.LocalMove (m_fSpeed * Time.deltaTime);
		}

		/// <summary>
		/// 遅い更新
		/// </summary>
		void LateUpdate() {
			// 例外処理
			if (m_bTrig)	return;

			//----- 上を向く
			for (int i = 0; i < m_Neck.Length; i++) {
				Quaternion quaternionNeck = Quaternion.AngleAxis (m_fNeckAngle[i], Vector3.forward);
				m_Neck[i].localRotation = quaternionNeck;
			}
		}

		#endregion unity method
	}

}
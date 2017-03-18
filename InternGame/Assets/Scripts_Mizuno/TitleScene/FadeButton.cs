using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Mizuno {

	public class FadeButton : MonoBehaviour {

		// テクスチャ
		Image		m_Image = null;

		//
		bool		m_bSwitch = true;							// On,Offフラグ
		float		m_fAlpha = 255.0f;							// 合計時間
		[SerializeField]private float		m_fCycle = 60.0f;	// フェード時間


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Image = GetComponent<Image> ();
			if (!m_Image)
				Debug.Log (this.name + " : FadeButtonのImageがないよ");
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// アルファ値変動量算出
			float Alpha = 255.0f / m_fCycle;

			// フェードIn
			if (m_bSwitch) {
				m_fAlpha -= (Alpha * Time.deltaTime);
				if (m_fAlpha <= 0.0f) {
					m_fAlpha = 0.0f;
					m_bSwitch ^= true;
				}
			} else {
			// フェードOut
				m_fAlpha += (Alpha * Time.deltaTime);
				if (m_fAlpha >= 255.0f) {
					m_fAlpha = 255.0f;
					m_bSwitch ^= true;
				}
			}
			
			// 色の反映
			Color old = m_Image.color;
			old.a = m_fAlpha / 255.0f;
			m_Image.color = old;
		}
	}

}
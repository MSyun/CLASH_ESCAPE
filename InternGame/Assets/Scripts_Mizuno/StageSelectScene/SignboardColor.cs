using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Mizuno {

	public class SignboardColor : MonoBehaviour {

		Image	m_Image = null;

		// 色
		[SerializeField]private Color	m_OnColor;
		[SerializeField]private Color	m_OffColor;

		// サイクル時間
		float	m_fCycle = 3.0f;
		// 現在の時間
		float	m_fTime = 0.0f;
		// 間隔
		float	m_fInterval = 1.0f;
		// スイッチ
		bool	m_bSwitch = true;



		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Image = GetComponent<Image> ();
			if (!m_Image)
				Debug.Log (this.name + " : SignboardColorのImageがないよ!");
			ChangeRandom ();
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 時間更新
			m_fTime += Time.deltaTime;

			// スイッチの確認
			if (m_bSwitch) {
				if (m_fCycle <= m_fTime) {
					m_bSwitch ^= true;
					m_fTime = 0.0f;
				}
			} else {
				// 発光まで休憩
				if (m_fInterval <= m_fTime) {
					m_bSwitch ^= true;
					m_fTime = 0.0f;
					ChangeRandom ();
				}
				return;
			}

			// 色の変更
			m_Image.color = ChangeColor (m_fTime);
		}


		/// <summary>
		/// 色の変更（保管
		/// </summary>
		/// <returns>新しい色</returns>
		/// <param name="time">0.0~1.0までの時間</param>
		Color ChangeColor(float time) {
			float r = Mathf.Lerp (m_OffColor.r, m_OnColor.r, time);
			float g = Mathf.Lerp (m_OffColor.g, m_OnColor.g, time);
			float b = Mathf.Lerp (m_OffColor.b, m_OnColor.b, time);
			float a = Mathf.Lerp (m_OffColor.a, m_OnColor.a, time);
			return new Color (r, g, b, a);
		}


		/// <summary>
		/// ランダムの初期化
		/// </summary>
		void ChangeRandom() {
			// 2 ~ 6
			m_fCycle = Random.Range (200, 600) / 100;
			// 1 ~ 2
			m_fInterval = Random.Range(100, 200) / 100;
		}
	}

}
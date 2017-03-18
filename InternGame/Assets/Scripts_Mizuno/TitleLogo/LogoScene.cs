using UnityEngine;
using System.Collections;
using Asada;

namespace Mizuno {

	public class LogoScene : MonoBehaviour {

		// 計測時間
		float	m_fTime = 0.0f;

		// 表示時間
		[SerializeField]private float	m_fAppearTime = 2.0f;

		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			m_fTime += Time.deltaTime;

			// タッチ入力確認
			if (!SceneChanger.GetFadeFlg() &&
				TouchUtil.GetTouch() == TouchInfo.Began) {
				m_fTime = m_fAppearTime;
			}
		
			// 表示時間を越える
			if (m_fTime >= m_fAppearTime) {
				SceneChanger.SetScene("Title");

				this.enabled = false;
			}
		}
	}

}
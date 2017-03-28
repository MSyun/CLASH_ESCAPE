using UnityEngine;
using System.Collections;

namespace Mizuno {

	/// <summary>
	/// ゲームの調整
	/// </summary>
	public class GameController : MonoBehaviour {

		#region variable

		[SerializeField]private int m_nFPS = 60;
		[SerializeField]private int	m_nVSync = 1;

		#endregion variable


		#region unity method

		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake () {
			// FPSの設定
			Application.targetFrameRate = m_nFPS;

			// 垂直同期
			QualitySettings.vSyncCount = m_nVSync;

			if (Application.isEditor) {
				Debug.Log ("フレームレートの設定は" + Application.targetFrameRate + "です");
				Debug.Log ("垂直同期の設定は" + QualitySettings.vSyncCount + "です");
			}

			this.enabled = false;
		}

		#endregion unity method
	}

}
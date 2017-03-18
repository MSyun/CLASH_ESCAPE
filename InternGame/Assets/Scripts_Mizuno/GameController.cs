using UnityEngine;
using System.Collections;

namespace Mizuno {

	public class GameController : MonoBehaviour {

		[SerializeField]private int m_nFPS = 60;
		[SerializeField]private int	m_nVSync = 1;


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
	}

}
using UnityEngine;
using System.Collections;


namespace Mizuno {

	/// <summary>
	/// クリア用生成機
	/// </summary>
	public class ClearGenerator : MonoBehaviour {

		#region variable

		bool	m_bClear = false;	// クリア情報

		[SerializeField]private GameObject	m_GameClear;
		[SerializeField]private	GameObject	m_GameOver;

		#endregion variable


		#region method

		/// <summary>
		/// リザルトを開く
		/// </summary>
		public void Open() {
			if (!this.enabled)
				return;

			if (m_bClear) {
				m_GameClear.SetActive (true);
				SoundManager.Instance.PlayBGM (2);
			} else {
				m_GameOver.SetActive (true);
				SoundManager.Instance.PlayBGM (3);
			}

			this.enabled = false;
		}


		/// <summary>
		/// ゲームクリア
		/// </summary>
		public void Clear() {
			if (!this.enabled)
				return;

			m_bClear = true;
			Open ();
		}

		#endregion method
	}

}
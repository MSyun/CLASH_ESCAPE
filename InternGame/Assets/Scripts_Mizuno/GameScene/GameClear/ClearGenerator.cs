using UnityEngine;
using System.Collections;


namespace Mizuno {

	public class ClearGenerator : MonoBehaviour {

		bool	m_bClear = false;	// クリア情報

		[SerializeField]private GameObject	m_GameClear;
		[SerializeField]private	GameObject	m_GameOver;


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
	}

}
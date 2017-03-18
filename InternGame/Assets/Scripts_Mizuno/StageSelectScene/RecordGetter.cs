using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Mizuno {

	public class RecordGetter : MonoBehaviour {

		// 難易度
		[SerializeField]private LevelController._StageLevel m_Level;

		// 記録保持者
		LevelController		m_LevelCont;

		// テキスト
		Text	m_text = null;


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_LevelCont = GameObject.Find ("LevelController").GetComponent<LevelController> ();
			m_text = gameObject.GetComponent<Text> ();
			if (!m_text)
				Debug.Log (this.name + " : RecordGetterのTextがないよ!");

			ChangeTime ();
		}


		/// <summary>
		/// データの再取得
		/// </summary>
		public void ChangeTime() {
			// 最高記録取得
			m_text.text = TimerConverter.Convert (m_LevelCont.GetRecord ((int)m_Level));
		}
	}

}
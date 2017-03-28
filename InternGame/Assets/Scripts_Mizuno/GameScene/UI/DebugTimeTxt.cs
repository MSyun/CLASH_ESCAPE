using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Asada;

namespace Mizuno {

	/// <summary>
	/// タイムアタック用テキスト
	/// </summary>
	public class DebugTimeTxt : MonoBehaviour {

		#region variable

		// 画面上に出すテキスト
		Text m_Text = null;

		// タイマー
		[SerializeField]private GameTimer	m_Timer;


		//Timeの文字
		public Text m_TimeText;

		#endregion variable


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Text = GetComponent<Text> ();

			if (!m_Text) {
				Debug.Log (this.name + "DebugTimeTxtのTextがないよ");
			}

			//タイムアタックじゃなかったら
			if (GameModeController.Instance.GetGameMode () != (int)GameModeController._GameMode.TIME_ATTACK) {
				enabled = false;			//このスクリプトを切る
				m_Text.enabled = false;		//テキスト非表示
				m_TimeText.enabled = false;
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 現在の時間
			m_Text.text = TimerConverter.Convert (m_Timer.GameTime);
		}

		#endregion unity method
	}

}
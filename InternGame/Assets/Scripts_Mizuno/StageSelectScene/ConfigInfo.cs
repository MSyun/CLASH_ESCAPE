using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Mizuno {

	/// <summary>
	/// コンフィグ関連
	/// </summary>
	public class ConfigInfo : MonoBehaviour {

		#region variable

		// キャンバス
		Canvas	m_Canvas;

		// テキスト
		Text	m_SwitchTxt;

		#endregion variable


		#region method

		/// <summary>
		/// キャンバスを開く
		/// </summary>
		public void Open() {
			m_Canvas.enabled = true;
			SoundManager.Instance.PlaySE(3);
		}


		/// <summary>
		/// キャンバスを閉じる
		/// </summary>
		public void Close() {
			m_Canvas.enabled = false;
			SoundManager.Instance.PlaySE(2);
		}


		/// <summary>
		/// デバッグ表示の変更
		/// </summary>
		public void ChangeDebug() {
			FPSDrawer.Instance.ChangeDraw();

			ChangeText(FPSDrawer.Instance.Draw);

			SoundManager.Instance.PlaySE(5);
		}


		/// <summary>
		/// テキスト表示の変更
		/// </summary>
		/// <param name="flg">テキスト表示フラグ</param>
		void ChangeText(bool flg) {
			if (flg) {
				m_SwitchTxt.text = "ON";
				m_SwitchTxt.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			} else {
				m_SwitchTxt.text = "OFF";
				m_SwitchTxt.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
			}
		}

		#endregion method


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Canvas = GetComponent<Canvas> ();
			if (!m_Canvas)
				Debug.Log (this.name + " : ConfigInfoにCanvasがないよ");

			m_SwitchTxt = GameObject.Find ("DebugChangeTxt").GetComponent<Text> ();
			if (!m_SwitchTxt)
				Debug.Log (this.name + " : ConfigInfoにTextがないよ");

			ChangeText (FPSDrawer.Instance.Draw);

			m_Canvas.enabled = false;
		}

		#endregion unity method
	}

}
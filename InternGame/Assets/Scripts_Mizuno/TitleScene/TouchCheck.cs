using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Asada;

namespace Mizuno {

	/// <summary>
	/// 画面入力確認
	/// </summary>
	public class TouchCheck : MonoBehaviour {

		#region unity method

		/// <summary>
		/// アウェイク
		/// </summary>
		void Start() {
			// タイトル
			SoundManager.Instance.PlayBGM(0);

			// 振動停止
			GameObject.Find ("Mini_Truck").GetComponent<ShakeTruck> ().enabled = false;
		}

		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 入力開始
			if (!SceneChanger.GetFadeFlg() && SceneChanger.GetStartFlg() &&
				TouchUtil.GetTouch () == TouchInfo.Began) {
				SceneChanger.SetScene("StageSelect");

				GameObject.Find("Player").GetComponent<TitlePlayer>().Change();
				GameObject.Find("Mini_Truck").GetComponent<ShakeTruck>().enabled = true;
				// エンジン
				SoundManager.Instance.PlaySE(1);

				//this.enabled = false;
			}
		}

		#endregion unity method
	}
}
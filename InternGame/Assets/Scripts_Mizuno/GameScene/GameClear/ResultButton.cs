using UnityEngine;
using System.Collections;
using Asada;


namespace Mizuno {

	public class ResultButton : MonoBehaviour {

		/// <summary>
		/// 再スタートを押された
		/// </summary>
		public void ReStart() {
			SceneChanger.SetScene ("Game");
			SoundManager.Instance.PlaySE (5);
		}

		/// <summary>
		/// 終了を押された
		/// </summary>
		public void End() {
			SceneChanger.SetScene ("StageSelect");
			SoundManager.Instance.PlaySE (5);
		}
	}

}
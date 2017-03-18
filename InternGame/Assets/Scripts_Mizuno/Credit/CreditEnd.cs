using UnityEngine;
using System.Collections;
using Asada;

using Mizuno;


namespace Mizuno {
	
	public class CreditEnd : MonoBehaviour {
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 入力確認
			if (SceneChanger.GetFadeFlg() ||
				TouchUtil.GetTouch () != TouchInfo.Began)
				return;

			SceneChanger.SetScene ("StageSelect");
			SoundManager.Instance.PlaySE (5);
			this.enabled = false;
		}
	}

}
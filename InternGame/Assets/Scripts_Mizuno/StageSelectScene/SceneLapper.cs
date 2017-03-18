﻿using UnityEngine;
using System.Collections;
using Asada;


namespace Mizuno {

	public class SceneLapper : MonoBehaviour {

		/// <summary>
		/// シーンの遷移
		/// </summary>
		/// <param name="SceneName">遷移先のシーン名</param>
		public void Change( string SceneName ) {
			SceneChanger.SetScene (SceneName);
		}

		/// <summary>
		/// クレジットシーンへ遷移
		/// </summary>
		public void Credit() {
			SceneChanger.SetScene ("Credit");
			SoundManager.Instance.PlaySE (5);
		}

		/// <summary>
		/// クレジットシーンへ遷移
		/// </summary>
		public void TruckCredit() {
			SceneChanger.SetScene ("TruckCredit");
			SoundManager.Instance.PlaySE (5);
		}
	}

}
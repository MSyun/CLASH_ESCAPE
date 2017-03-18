using UnityEngine;
using System.Collections;

using Mizuno;

namespace Asada
{
	/// <summary>
	/// クレジットScene管理クラス
	/// </summary>
	public class CreditController : MonoBehaviour {

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			SoundManager.Instance.PlayBGM (1);
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update () {
		
		}

		/// <summary>
		/// クラクションボタンが押されたら
		/// </summary>
		public void OnCarHornButton()
		{
			SoundManager.Instance.RePlaySE (0);
		}

		/// <summary>
		/// クラクションボタンが離されたら
		/// </summary>
		public void OnCarHornReleaseButton() {
			SoundManager.Instance.StopSE (0);
		}

		/// <summary>
		/// スキップボタンが押されたら
		/// </summary>
		public void OnSkipButton()	{
			SceneChanger.SetScene ("StageSelect");
		}

	}
}
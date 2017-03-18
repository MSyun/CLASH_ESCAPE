using UnityEngine;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// ゲームモード管理用クラス
	/// </summary>
	public class GameModeController : MonoBehaviour {

		//----- Singleton
		private static GameModeController mInstance = null;

		public static GameModeController Instance {
			get {
				return mInstance;
			}
		}

		/// <summary>
		/// ゲームモード
		/// </summary>
		public enum _GameMode
		{
			TIME_ATTACK = 0,
			Normal,

			MODE_MAX,
		};


		//現在のゲームモード
		private int m_nNowMode = (int)_GameMode.Normal;

		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake () {
			//----- Singleton
			if (mInstance == null) {
				mInstance = this;
			} else {
				Destroy (this);
				return;
			}

			DontDestroyOnLoad (this);
		}

		/// <summary>
		/// ゲームモード設定
		/// </summary>
		/// <param name="nMode">N mode.</param>
		public void SetGameMode(int nMode)
		{
			//範囲内の場合でのみ受け付ける
			if ( (int)_GameMode.TIME_ATTACK <= nMode && nMode < (int)_GameMode.MODE_MAX) {
				m_nNowMode = nMode;
			}
		}


		/// <summary>
		/// 現在のゲームモード取得
		/// </summary>
		/// <returns>The game mode.</returns>
		public int GetGameMode()
		{
			return m_nNowMode;
		}

	}
}
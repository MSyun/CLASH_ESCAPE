using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mizuno;



namespace Asada
{
	/// <summary>
	/// モード切替クラス
	/// </summary>
	public class ModeChange : MonoBehaviour {

		public Image m_ModeDrawImage;	//描画先イメージ
		public Sprite m_TimeAttackImage;	//タイムアッタイメージ
		public Sprite m_NormalImage;		//ノーマルイメージ
		public Image m_ModeNoneImage;	//モードなし時のイメージ
		public Text[] m_RecordText;		//レコードテキスト

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			//初期設定
			ChangeImage ((int)GameModeController.Instance.GetGameMode ());
		}

		/// <summary>
		/// モード切り替えボタンが押されたら
		/// </summary>
		public void OnModeChangeButton()
		{
			//現在のモード設定
			int nMode = GameModeController.Instance.GetGameMode ();

			//切り替え
			nMode = ((int)GameModeController._GameMode.MODE_MAX - 1) - nMode;

			//モード設定
			GameModeController.Instance.SetGameMode (nMode);

			//表示切り替え
			ChangeImage(nMode);

			SoundManager.Instance.PlaySE (5);
		}


		/// <summary>
		/// 表示切り替え処理
		/// </summary>
		void ChangeImage(int nNowMode)
		{
			int nCnt;

			switch (nNowMode) {

			//ノーマル時
			case (int)GameModeController._GameMode.Normal:
				m_ModeNoneImage.enabled = true;
				for (nCnt = 0; nCnt < m_RecordText.Length; nCnt++) {
					m_RecordText [nCnt].enabled = false;
				}

				//イメージ変更
				m_ModeDrawImage.sprite = m_NormalImage;
				break;

			//タイムアタック時
			case (int)GameModeController._GameMode.TIME_ATTACK:
				m_ModeNoneImage.enabled = false;
				for (nCnt = 0; nCnt < m_RecordText.Length; nCnt++) {
					m_RecordText [nCnt].enabled = true;
				}

				//イメージ変更
				m_ModeDrawImage.sprite = m_TimeAttackImage;
				break;
			}
		}
	}
}
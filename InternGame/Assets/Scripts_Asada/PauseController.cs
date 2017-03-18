using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mizuno;
using Asada;

namespace Asada
{
	/// <summary>
	/// ポーズ画面操作クラス
	/// </summary>
	public class PauseController : MonoBehaviour {

		public Canvas m_PauseScreen;		//ポーズ画面キャンバス
		bool m_bPause;						//ポーズ判別用フラグ
		LevelController LevelCon;			//最高記録取得用
		public Text RecordTime;				//最高記録表示テキスト
		public Text LevelText;				//難易度表示用テキスト
		public Scaling PauseText;			//拡大縮小テキスト

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			LevelCon = GameObject.Find ("LevelController").GetComponent<LevelController> ();	//レベルコントローラ取得
			InitPause();
		}


		/// <summary>
		/// 初期化関数
		/// </summary>
		void InitPause()
		{
			//フラグ初期化
			m_bPause = false;

			//キャンバス非表示に
			m_PauseScreen.enabled = false;

			//拡大縮小OFF
			PauseText.enabled = false;

			//ポーズ設定

			//難易度表示
			LevelText.text = "Level：";
			switch (LevelCon.GetCurLevel ()) {
			case 0:
				LevelText.text += "Easy";
				break;
				
			case 1:
				LevelText.text += "Normal";
				break;

			case 2:
				LevelText.text += "Hard";
				break;
			
			case 3:
				LevelText.text += "Nightmare";
				break;
			}

			//最高記録セット
			if (GameModeController.Instance.GetGameMode () == (int)GameModeController._GameMode.TIME_ATTACK)
				RecordTime.text = "最高記録：" + TimerConverter.Convert (LevelCon.GetRecord (LevelCon.GetCurLevel ()));
			else
				RecordTime.enabled = false;
		}


		/// <summary>
		/// ポーズボタンが押されたら
		/// </summary>
		public void OnPause()
		{
			//ポーズ中かどうか
			if (m_bPause == false) {
				//ポーズ開始
				Time.timeScale = 0.0f;	//タイムスケールを0に

				//拡大縮小ON
				PauseText.enabled = true;

				//ポーズ画面表示
				m_PauseScreen.enabled = true;

				// サウンド再生
				SoundManager.Instance.PlaySE(3);

			} else 
			{//ポーズ中ならポーズ終了
				//ポーズ画面終了
				OFFPause();
			}
		}


		/// <summary>
		/// リトライボタンが押されらたら
		/// </summary>
		public void OnRetry()
		{
			Time.timeScale = 1.0f;				//タイムスケールを元に戻す
			m_PauseScreen.enabled = false;		//ポーズ画面を非表示に
			SceneChanger.SetScene ("Game");		//シーン切り替え

			// サウンド再生
			SoundManager.Instance.PlaySE(5);
		}


		/// <summary>
		/// ポーズ終了処理
		/// </summary>
		public void OFFPause()
		{
			Time.timeScale = 1.0f;				//タイムスケールを元に戻す
			m_PauseScreen.enabled = false;		//ポーズ画面を非表示に
			//拡大縮小OFF
			PauseText.enabled = false;

			// サウンド再生
			SoundManager.Instance.PlaySE(2);
		}

		/// <summary>
		/// 戻るボタンが押されたら
		/// </summary>
		public void OnQuit()
		{
			Time.timeScale = 1.0f;					//タイムスケールを元に戻す
			m_PauseScreen.enabled = false;			//ポーズ画面を非表示に
			SceneChanger.SetScene ("StageSelect");	//シーン切り替え

			// サウンド再生
			SoundManager.Instance.PlaySE(5);
		}
}
}
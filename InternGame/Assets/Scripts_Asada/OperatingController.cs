using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Mizuno;

namespace Asada
{
	/// <summary>
	/// 操作説明画面操作クラス
	/// </summary>
	public class OperatingController : MonoBehaviour {

		public Canvas  m_OperatingScreen;	//操作説明のキャンバス
		public Image[] m_OperatingImage;	//操作説明の画像
		public Text    m_CountText;			//現在の枚数表示用
		public GameObject  m_BackButton;	//前Button表示切り替え
		int m_nNowImageCnt;					//現在の画像
		int m_nMaxImageCnt;					//最大枚数

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			m_nMaxImageCnt = m_OperatingImage.Length;	//最大枚数設定
			m_nNowImageCnt = 0;							//開始位置設定
			m_BackButton.SetActive(false);				//前へButton設定
			m_OperatingScreen.enabled = false;			//Canvasを非表示
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {
		
		}


		/// <summary>
		/// イメージの表示切り替え関数
		/// </summary>
		/// <param name="nImageCnt">N image count.</param>
		void SetDrawImage(int nImageCnt)
		{
			for (int nCnt = 0; nCnt < m_nMaxImageCnt; nCnt++) 
			{
				//表示の切り替え
				if (nCnt == nImageCnt) {
					m_OperatingImage [nCnt].enabled = true;
				} 
				else {
					m_OperatingImage [nCnt].enabled = false;
				}
			}

			//テキストの表示設定
			m_CountText.text = (m_nNowImageCnt + 1) + " / " + m_nMaxImageCnt;
		}


		/// <summary>
		/// 操作説明ボタンが押されたら
		/// </summary>
		public void OnOperatingButton()
		{
			//初期化
			m_nNowImageCnt = 0;
			SetDrawImage (m_nNowImageCnt);
			m_OperatingScreen.enabled = true;
			m_BackButton.SetActive(false);				//前へButton設定

			SoundManager.Instance.PlaySE (3);
		}


		/// <summary>
		/// 操作説明終了ボタンが押されたら
		/// </summary>
		public void OffOperatingButton()
		{
			m_OperatingScreen.enabled = false;
			SoundManager.Instance.PlaySE (2);
		}


		/// <summary>
		/// 次へのボタンが押されたら
		/// </summary>
		public void OnNextButton()
		{
			//次の画像へ
			m_nNowImageCnt++;
			m_BackButton.SetActive(true);				//前へButton設定

			//次の画像が無かったら
			if (m_nNowImageCnt >= m_nMaxImageCnt) {
				m_OperatingScreen.enabled = false;	//終了
			} 
			else {
				SetDrawImage (m_nNowImageCnt);		//表示切り替え
			}

			SoundManager.Instance.PlaySE (5);
		}


		/// <summary>
		/// 前へのボタンが押されたら
		/// </summary>
		public void OnBackButton()
		{
			//前の画像へ
			m_nNowImageCnt--;

			//先頭の画像だったら
			if (m_nNowImageCnt == 0) {
				m_BackButton.SetActive(false);				//前へButton設定
			}

			SetDrawImage (m_nNowImageCnt);		//表示切り替え

			SoundManager.Instance.PlaySE (5);
		}
	}
}
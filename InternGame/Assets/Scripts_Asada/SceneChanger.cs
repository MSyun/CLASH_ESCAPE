using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Asada
{
	/// <summary>
	/// シーン切り替えクラス
	/// </summary>
	public class SceneChanger : MonoBehaviour {

		public bool m_bFadeIn;				//フェードインするかどうか
		public bool m_bFadeOut;				//フェードアウトするかどうか

		public int 	m_FadeTimeIn;			//フェードインにかける時間
		public int 	m_FadeTimeOut;			//フェードアウトにかける時間
		public Image m_FadeImage;			//フェード用イメージ


		static string m_NextScene;			//次Scene名
		static bool  m_bFade;				//フェードをしているかどうか
		static bool  m_bStart = false;		//スタート関数を通ったかどうか(タイトルSceneのtouch用)		
		float m_fAddAlpha;					//加算用アルファ値



		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			m_bStart = true;

			//次シーン名初期化
			m_NextScene = null;	

			//フェードアウトを行うか
			if (m_bFadeOut == true) {
				//フェードアウトを行う
				m_fAddAlpha = -1.0f / m_FadeTimeIn;		//加算値設定
				m_FadeImage.enabled = true;				//フェード用画像表示
				m_FadeImage.color = new Color(m_FadeImage.color.r, m_FadeImage.color.g, m_FadeImage.color.b, 1.0f);		//アルファ値を0初期化
				m_bFade = true;							//フェード中状態に
			} 
			else {
				//フェードインを行わない
				m_FadeImage.raycastTarget = false;		//キャンバスが反応しないように
				m_FadeImage.enabled = false;			//フェード用画像を消す
			}
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {

			//シーン変更判定
			if (m_NextScene != null && m_bFade == false) {
				SceneChange();	//シーン変更処理
			}

			//フェード中じゃない場合更新しない
			if(m_bFade == false) return;

			//現在の色取得
			Color NowColor = m_FadeImage.color;

			//アルファ値加算
			NowColor.a += m_fAddAlpha * Time.deltaTime * 60.0f;

			//フェードアウトが終わっていたら
			if (NowColor.a < 0.0f) {
				//Debug.Log("フェードアウト終了");
				NowColor.a = 0.0f;	//0初期化
				m_bFade = false;	//フェード終了
				m_FadeImage.enabled = false;
			}
			else if(NowColor.a > 1.0f)
			{//フェードインが終了していたら
				//Debug.Log("フェードイン終了");
				NowColor.a = 1.0f;						//1.0fで初期化
				SceneManager.LoadScene(m_NextScene);	//シーンの切り替え
				m_bFade = false;						//フェード終了
				m_bStart = false;
			}

			//アルファ値反映
			m_FadeImage.color = NowColor;
		}


		/// <summary>
		/// シーン切り替え
		/// </summary>
		void SceneChange()
		{
			//フェードイン行うか
			if (m_bFadeIn == true) {
				//Debug.Log("Scene切り替えセット");
				m_fAddAlpha = 1.0f / m_FadeTimeOut;		//加算値設定
				m_FadeImage.color = new Color(m_FadeImage.color.r, m_FadeImage.color.g, m_FadeImage.color.b, 0.0f);		//アルファ値を初期化
				m_bFade = true;							//フェード中状態に
				m_FadeImage.enabled = true;				//フェード用画像を表示
			}
			else {
				//フェードインを行わない場合
				m_FadeImage.enabled = false;			//フェード用画像を非表示
				SceneManager.LoadScene(m_NextScene);	//シーンの切り替え
				m_bStart = false;
			}
		}


		/// <summary>
		/// 次シーン設定
		/// </summary>
		/// <param name="nextscene">Nextscene.</param>
		static public void SetScene(string nextscene)
		{
			//割り込みは受け付けない
			if (m_NextScene == null) {
				// Debug.Log("Scene名設定");
				m_NextScene = nextscene;				//次Scene名セット
			}
		}


		/// <summary>
		/// フェードフラグ取得
		/// </summary>
		/// <returns><c>true</c>, if fade flg was gotten, <c>false</c> otherwise.</returns>
		static public bool GetFadeFlg()
		{
			return m_bFade;
		}

		/// <summary>
		/// スタート関数を通ったか検知用フラグ取得
		/// </summary>
		/// <returns><c>true</c>, if start flg was gotten, <c>false</c> otherwise.</returns>
		static public bool GetStartFlg()
		{
			return m_bStart;
		}
	}
}
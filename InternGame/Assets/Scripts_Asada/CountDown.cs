using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mizuno;


namespace Asada
{
	/// <summary>
	/// カウントダウンクラス
	/// </summary>
	public class CountDown : MonoBehaviour {

		public int m_nCountTime;		//何秒カウントするか
		bool   m_bCountDown;			//カウントダウン判別用
		float  m_CurrentTime;			//現在の時間
		int    m_nCount;				//何秒たったかカウント
		public Player m_Player;			//プレイヤーのスクリプトを止めるよう
		public CountDownMove m_Move;	// 開始前の移動
		Text   m_CountText;				//カウントダウン用テキスト

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			m_nCount = 0;									//カウント初期化
			m_bCountDown = false;							//カウントダウンOFF
			m_Player.enabled = false;						//プレイヤースクリプトOFF
			m_Move.enabled = true;							// プレイヤー開始前行動 On
			m_CountText = gameObject.GetComponent<Text>();	//テキスト取得
			m_CountText.text = m_nCountTime.ToString ();	//テキストに現在の秒数を設定
		}

		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {

			//カウントダウン開始判定
			if (SceneChanger.GetFadeFlg () == false && m_bCountDown == false) {

				//現在の時間取得
				m_CurrentTime = Time.time; 

				//フラグON
				m_bCountDown = true;
			}

			//カウントダウン中しか処理をしない
			if (m_bCountDown == false)
				return;

			//1秒経過したら
			if (Time.time - m_CurrentTime > 1.0f) {
				m_nCount++;					//カウントする
				m_CurrentTime = Time.time;	//現在時間更新

				//カウントダウンが終了したら
				if ((m_nCountTime - m_nCount) == 0) {
					m_Player.enabled = true;		//プレイヤーを動かす
					m_Move.enabled = false;			//カウントダウン用の移動をOFFにする
					gameObject.SetActive(false);	//スクリプトをOFFにする

					// 時間計測開始
					GameObject.Find("Timer").GetComponent<GameTimer>().Play();

					// サウンド再生
					SoundManager.Instance.PlaySE(4);
				} 
				else 
				{//終了していなかったら
					m_CountText.text = (m_nCountTime - m_nCount).ToString ();		//テキスト更新
				}
			}

		}
	}
}
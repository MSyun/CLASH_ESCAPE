using UnityEngine;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// 設定した時間が過ぎたら削除するクラス
	/// </summary>
	public class DeleteObstacle : MonoBehaviour {

		public float m_fDeleteSecond;	//削除までの秒数
		float m_OnTime;					//有効にされた時間

		/// <summary>
		/// Enableがtrueになったら
		/// </summary>
		void OnEnable()
		{//現在の時間を取得する
			m_OnTime = Time.time;
		}


		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			//スクリプトを無効にする
			enabled = false;
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {

			//削除判定
			if (Time.time - m_OnTime >= m_fDeleteSecond) {
				Destroy (gameObject);
			}
		}
	}
}

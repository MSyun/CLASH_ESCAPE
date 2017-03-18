using UnityEngine;
using System.Collections;

using Mizuno;

namespace Asada
{
	/// <summary>
	/// 演出用星操作
	/// </summary>
	public class Star : MonoBehaviour {

		enum _StarState
		{
			START,
			ROTATE,
			END,
			MAX
		};

		public float m_fDeleteTime;	//削除するまでの時間
		public float m_fAddAngle;	//1回の回転量

		public float m_fMaxScale;	//最大拡大率
		public float m_fMinScale;	//最小拡大率
		public float m_fAddScale;	//加算拡大率


		float m_fRate;
		float m_fAddRate;
		float m_StartTime;
		_StarState m_NowState;


		// Use this for initialization
		void Start () {
			m_fAddRate = 1 / ((m_fMaxScale - m_fMinScale) / m_fAddScale);
//			Debug.Log (m_fAddRate);
		}

		/// <summary>
		/// スクリプトがONになった時
		/// </summary>
		void OnEnable()
		{
			//座標を設定
			transform.position = GameObject.Find("Player").transform.position;
			transform.LookAt (Camera.main.transform);

			//初期拡大率設定
			transform.localScale = new Vector3(m_fMinScale,m_fMinScale,m_fMinScale);

			//初期ステート設定
			m_NowState = _StarState.START;

			//レート初期化
			m_fRate = 0;

			//playerの表示を消す
			GameObject.Find("Player").SetActive(false);
		}

		// Update is called once per frame
		void Update () {

			//各状態ごとに更新
			switch (m_NowState) 
			{
			//開始
			case _StarState.START:

				//レート加算
				m_fRate += m_fAddRate;

				//だんだん大きくする
				transform.localScale = Vector3.Lerp (new Vector3(m_fMinScale,m_fMinScale,m_fMinScale), new Vector3(m_fMaxScale,m_fMaxScale,m_fMaxScale), m_fRate);

				//指定サイズになったら
				if (transform.localScale.x == m_fMaxScale) {
					//ステート変更
					m_NowState = _StarState.ROTATE;

					//現在の時間取得
					m_StartTime = Time.time;

					//レート初期化
					m_fRate = 0;

					SoundManager.Instance.PlaySE (12);
				}
				break;
			
			//回転
			case _StarState.ROTATE:
				//ステート変更判定
				if (Time.time - m_StartTime >= m_fDeleteTime) {
					m_NowState = _StarState.END;
				}
				break;
			
			//終了
			case _StarState.END:
				//レート加算
				m_fRate += m_fAddRate;

				//だんだん小さくする
				transform.localScale = Vector3.Lerp (new Vector3(m_fMaxScale,m_fMaxScale,m_fMaxScale), new Vector3(m_fMinScale,m_fMinScale,m_fMinScale), m_fRate);

				//指定サイズになったら
				if (transform.localScale.x == m_fMinScale) {
					//削除する
					Destroy (gameObject);
				}
				break;
			}


			//回転させる
			Vector3 Rot = transform.eulerAngles;
			Rot.z += m_fAddAngle;
			transform.eulerAngles = Rot;
		}
	}
}
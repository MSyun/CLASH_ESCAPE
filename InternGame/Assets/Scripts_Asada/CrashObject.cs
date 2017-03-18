using UnityEngine;
using System.Collections;
using Mizuno;

namespace Asada
{ 
	/// <summary>
	/// ぶつかったオブジェクトを飛ばすクラス
	/// </summary>
	public class CrashObject : MonoBehaviour {

		public float m_CrashPower;		//飛ばす威力
		public float m_fMinYRot;		//Y軸の最小角度
		public float m_fMaxYRot;		//Y軸の最大角度
		public float m_fMinXRot;		//X軸の最小角度
		public float m_fMaxXRot;		//X軸の最大角度

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
		}


		/// <summary>
		/// 当たり判定
		/// </summary>
		/// <param name="col">Col.</param>
		void OnTriggerEnter(Collider col)
		{
			if (col.gameObject.tag != "Player" && col.gameObject.tag != "Obstacle")
				return;

			//現在の角度を保存
			Vector3 rot = transform.rotation.eulerAngles;

			//回転角度決定
			float fRotX = Random.Range(m_fMinXRot,m_fMaxXRot);
			float fRotY = Random.Range (m_fMinYRot, m_fMaxYRot);


			//回転実行
			transform.Rotate (transform.up,fRotY);
			transform.Rotate (transform.right,fRotX);


			//Debug.Log ("fRotX" + fRotX);
			//Debug.Log ("fRotY" + fRotY);
			if (col.gameObject.tag == "Obstacle") {
				//Rigidbodyを取得して力を加える
				col.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * m_CrashPower,ForceMode.Force);
				col.gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * m_CrashPower,ForceMode.Force);

				//削除用のスクリプトをONにする
				col.gameObject.GetComponent<DeleteObstacle>().enabled = true;
				//col.gameObject.GetComponent<BoxCollider>().enabled = false;
				col.gameObject.tag = "HitObstacle";
			}

			if (col.gameObject.tag == "Player") {
				//Debug.Log ("ひっと");
				//Rigidbodyを取得して力を加える
				col.gameObject.transform.GetComponent<Rigidbody>().AddForce(transform.forward * m_CrashPower * 1.5f,ForceMode.Force);
				col.gameObject.transform.GetComponent<Rigidbody>().AddTorque(transform.right * m_CrashPower * 1.5f,ForceMode.Force);

				//プレイヤースクリプトをOFFにする
				col.gameObject.GetComponent<Player>().enabled = false;

				//アニメーションをOFFにする
//				col.gameObject.GetComponent<Animator>().enabled = false;

				//残りの距離表示を消す
				GameObject.Find("DistanceScreen").GetComponent<Canvas>().enabled = false;
				// プレイヤー
				GameObject.Find("Player").GetComponent<Player>().enabled = false;
				// タイマーをとめる
				GameObject.Find("Timer").GetComponent<GameTimer>().End();
				// ゲームUI
				GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
				// カメラ
				Camera.main.GetComponent<ChaseCamera>().enabled = false;
				//トラックを止める
				GameObject.Find("Mini_Truck").GetComponent<Truck>().enabled = false;

				this.enabled = false;

				//フェード中はゲームオーバー画面を表示しない
				if(SceneChanger.GetFadeFlg() == false)
					GameObject.Find("Result").GetComponent<ClearGenerator>().Open();
			}


			//Debug.Log ("m_CrashPower" + m_CrashPower);

			//角度を戻す
			transform.rotation = Quaternion.Euler(rot);
		}
}
}
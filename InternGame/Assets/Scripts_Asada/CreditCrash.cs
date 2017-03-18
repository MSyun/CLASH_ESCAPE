using UnityEngine;
using System.Collections;

using Mizuno;

namespace Asada
{
	public class CreditCrash : MonoBehaviour {

		public float m_CrashPower;		//飛ばす威力
		public float m_fMinYRot;		//Y軸の最小角度
		public float m_fMaxYRot;		//Y軸の最大角度
		public float m_fMinXRot;		//X軸の最小角度
		public float m_fMaxXRot;		//X軸の最大角度

		/// <summary>
		/// 当たり判定
		/// </summary>
		/// <param name="col">Col.</param>
		void OnTriggerEnter(Collider col)
		{
			if (col.gameObject.tag != "CreditObj" && col.gameObject.tag != "CreditGoal"&& col.gameObject.tag != "Player")
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
			if (col.gameObject.tag == "CreditObj") {
				//Rigidbodyを取得して力を加える
				col.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * m_CrashPower,ForceMode.Force);
				col.gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * m_CrashPower,ForceMode.Force);
				SoundManager.Instance.PlaySE (6);

			}

			if (col.gameObject.tag == "Player") {
				//Rigidbodyを取得して力を加える
				col.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * m_CrashPower,ForceMode.Force);
				col.gameObject.GetComponent<Rigidbody>().AddTorque(transform.right * m_CrashPower,ForceMode.Force);
				SoundManager.Instance.PlaySE (6);
				col.gameObject.GetComponent<CreditMove> ().enabled = false;
				col.gameObject.GetComponent<Animator> ().enabled = false;
			}

			if (col.gameObject.tag == "CreditGoal") {
				//カメラを揺らす
				Camera.main.GetComponent<CreditShakeCamera>().enabled = true;
				//トラックを止める
				GetComponent<CreditMove>().enabled = false;
				//シーンを変える
				SceneChanger.SetScene("StageSelect");
			}

			//角度を戻す
			transform.rotation = Quaternion.Euler(rot);
		}

}
}
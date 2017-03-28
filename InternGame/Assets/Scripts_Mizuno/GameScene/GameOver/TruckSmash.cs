using UnityEngine;
using System.Collections;

using Asada;

namespace Mizuno {

	/// <summary>
	/// トラックがオブジェクトを潰す時の動き
	/// </summary>
	public class TruckSmash : MonoBehaviour {

		#region variable

		// つぶれるオブジェクト
		[SerializeField]private GameObject	m_BringObj;
		[SerializeField]private Vector3		m_vObjScale;
		[SerializeField]private Vector3		m_vObjInitRot;
		bool		m_bObjComponent = false;

		// ゴールの近さ
		bool		m_bNearGoal = false;
		public bool NearGoal { set{ m_bNearGoal = value; } }

		float		m_fSpeed;
		Mover		m_Mover;
		float		m_fStopTime = 7.0f;

		// 倒れる角度
		[SerializeField]private float		m_fSmashAngle;
		[SerializeField, Range(0.0f, 5.0f)]private float		m_fSmashTimeScale = 2.0f;

		float	m_fTime = 0.0f;

		float	m_fObjMoveTime = 3.0f;

		[SerializeField]private GameObject[]	m_StopperObj;

		#endregion variable


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			// Rigidbodyストップ
			RigidbodyStopper( m_BringObj.GetComponent<Rigidbody>());

			m_Mover = GetComponent<Mover> ();
			m_fSpeed = GetComponent<Truck> ().Speed;

			m_BringObj.transform.Translate (new Vector3 (0.0f, 1.0f, 0.0f));
			m_BringObj.transform.eulerAngles = m_vObjInitRot;
			m_BringObj.transform.localScale = m_vObjScale;
			m_BringObj.GetComponent<Animator> ().SetTrigger ("tBallUp");
			m_BringObj.GetComponent<BoxCollider> ().enabled = false;

			Camera.main.GetComponent<ChaseCamera> ().enabled = false;

			GameObject.Find ("StageController").GetComponent<StageController> ().enabled = false;

			// いらないオブジェクト群
			for (int i = 0; i < m_StopperObj.Length; ++i) {
				m_StopperObj [i].SetActive (false);
			}
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			m_fTime += Time.deltaTime;

			if (m_fTime <= m_fStopTime)
				m_Mover.LocalMove (m_fSpeed * Time.deltaTime);

			if (!m_bObjComponent && m_fTime >= m_fObjMoveTime) {
				if (m_bNearGoal) {
					Vector3 pos = Camera.main.transform.position;
					pos.z = m_BringObj.transform.position.z;
					Camera.main.transform.position = pos;
					Camera.main.transform.LookAt (m_BringObj.transform);
				} else {
					m_BringObj.GetComponent<SmashObj>().enabled = true;
				}
				m_bObjComponent = true;
			}

			// プレイヤーを倒す
			if (m_fTime < 1.0f) {
				float t = Mathf.Clamp (m_fTime * m_fSmashTimeScale, 0.0f, 1.0f);
				float Angle = Mathf.Lerp (0.0f, m_fSmashAngle, t);
				Vector3 ObjAngle = m_vObjInitRot;
				ObjAngle.x += Angle;
				m_BringObj.transform.eulerAngles = ObjAngle;
			}
		}

		#endregion unity mehod


		#region method

		/// <summary>
		/// Rigidbodyの処理を止める
		/// </summary>
		/// <param name="rigid">Rigidbody</param>
		void RigidbodyStopper( Rigidbody rigid ) {
			if (!rigid)
				return;

			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
			rigid.isKinematic = true;
		}

		/// <summary>
		/// あたった直後の判定
		/// </summary>
		/// <param name="col">当たった相手</param>
		void OnTriggerEnter(Collider col ) {
			if (col.gameObject.tag != "Goal")
				return;

			// パーティ来るを出す
			GetComponent<Truck>().OnParticle();

			m_fSpeed = 0.0f;
		}

		#endregion method
	}
}
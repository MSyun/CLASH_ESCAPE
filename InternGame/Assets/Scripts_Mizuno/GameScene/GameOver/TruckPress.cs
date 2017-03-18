using UnityEngine;
using System.Collections;

using Asada;

namespace Mizuno {
	/// <summary>
	/// トラックがオブジェクトをゴールまで運ぶ動き
	/// </summary>
	public class TruckPress : MonoBehaviour {

		// つれてくオブジェ
		[SerializeField]private GameObject	m_BringObj;
		[SerializeField]private Vector3		m_vObjInitRot;	// 角度
		[SerializeField]private Vector3		m_vObjGapPos;	// 座標のずれ

		Mover	m_Mover;
		[SerializeField]private float		m_fSpeed = 1.0f;
		[SerializeField]private Vector3		m_vLookGap;		// 注視点のずれ

		// フェードが開始するまでの時間
		float	m_fBeginFadeTime = 2.0f;
		float	m_fTimeFade = 0.0f;
		bool	m_bFade = false;


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			// Rigidbodyストップ
			RigidbodyStopper (m_BringObj.GetComponent<Rigidbody> ());

			m_Mover = GetComponent<Mover> ();

			m_BringObj.transform.parent = transform;

			m_BringObj.transform.eulerAngles = m_vObjInitRot;
			m_BringObj.GetComponent<Animator> ().SetTrigger ("tBallUp");
			m_BringObj.transform.localPosition = m_vObjGapPos;
			m_BringObj.GetComponent<BoxCollider> ().enabled = false;

			Camera.main.GetComponent<ChaseCamera> ().enabled = false;

			GameObject.Find ("StageController").GetComponent<StageController> ().enabled = false;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			m_Mover.LocalMove (m_fSpeed * Time.deltaTime * 60.0f);
			Camera.main.transform.LookAt(m_BringObj.transform.position + m_vLookGap);

			m_fTimeFade += Time.deltaTime;
			if (!m_bFade &&
				m_fTimeFade >= m_fBeginFadeTime) {
				// フェード開始
				m_bFade = true;
				Camera.main.GetComponent<ShakeCamera> ().MoveGoal ();
				GetComponent<Truck> ().GoalMove ();
			}
		}

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

			if (m_BringObj.transform.parent != gameObject.transform)
				return;
			
			// カメラを揺らす
			Camera.main.GetComponent<ShakeCamera> ().enabled = true;

			//揺れを止める
			gameObject.GetComponent<ShakeTruck> ().enabled = false;

			// パーティ来るを出す
			GetComponent<Truck>().OnParticle();

			// オブジェクトの親子を消す
			m_BringObj.transform.parent = null;

			PressObj Press = m_BringObj.AddComponent<PressObj> ();
			Press.m_vRot = new Vector3 (8.0f, 0.0f, 0.0f);
			Press.m_vVec = new Vector3 (0.0f, 0.0f, 2.0f);
			Press.m_fMaxTime = 3.0f;

			// 自身を止める
			this.enabled = false;
		}
	}
}
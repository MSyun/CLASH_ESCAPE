using UnityEngine;
using System.Collections;

namespace Mizuno {

	/// <summary>
	/// プレイヤーが車に吹き飛ばされた時の動き
	/// </summary>
	public class PlayerImpactCamera : MonoBehaviour {

		#region variable

		// ベジエ関係
		Vector3 m_vStartPosition;
		[SerializeField]private Vector3		m_vStartVec;
		Vector3		m_vGoalPosition;
		[SerializeField]private Vector3		m_vGoalVec;

		[SerializeField]private Vector3		m_vGoalGap;	// 誤差

		[SerializeField]private float		m_fGoalTime = 1.0f;
		float								m_fTime = 0.0f;

		// 回転
		Vector3			m_vInitRot = Vector3.zero;
		[SerializeField]private Vector3	m_vGoalRot;

		// 止めたいオブジェ
		[SerializeField]GameObject[]		m_StopObj;

		// ずり落ち
		bool	m_bSlideFall = false;
		[SerializeField]private float		m_fSlideSpeed = 0.5f;
		[SerializeField]private float		m_fSlideStopTime = 1.0f;
		[SerializeField]private Vector3		m_fSlideRot;

		Mover	m_Mover;

		// ボーン
		[SerializeField]private Transform[]	m_Bone;
		[SerializeField]private float[]		m_fAngle;
		[SerializeField]private int[]		m_nAxis;

		#endregion variable


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_vStartPosition = transform.position;
			m_vGoalPosition = Camera.main.transform.position;
			m_vInitRot = transform.eulerAngles;

			transform.LookAt (Camera.main.transform);

			// Rigidbodyストップ
			Rigidbody rigidbody = GetComponent<Rigidbody> ();
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.isKinematic = true;

			// Animator
			Animator anim = GetComponent<Animator>();
			anim.speed = 1.0f;
			anim.SetTrigger("tImpact");
			anim.updateMode = AnimatorUpdateMode.UnscaledTime;

			// Obj
			for (int i = 0; i < m_StopObj.Length; i++) {
				m_StopObj [i].SetActive (false);
			}

			// Mover
			m_Mover = GetComponent<Mover>();
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			if (!m_bSlideFall)
				FlyingMove ();
			else
				SlideFallMove ();
		}

		/// <summary>
		/// 遅めの更新
		/// </summary>
		void LateUpdate() {
			for (int i = 0; i < m_Bone.Length; i++) {
				Quaternion qua;
				Vector3 axis = Vector3.zero;
				if (m_nAxis [i] == 0)	axis = Vector3.right;
				if (m_nAxis [i] == 1)	axis = Vector3.up;
				if (m_nAxis [i] == 2)	axis = Vector3.forward;

				qua = Quaternion.AngleAxis (m_fAngle [i], axis);
				m_Bone [i].localRotation = qua;
			}
		}

		#endregion unity method


		#region method

		/// <summary>
		/// 飛行
		/// </summary>
		void FlyingMove() {
			if (m_fTime >= 1.0f) {
				Camera.main.GetComponent<FaceCamera> ().enabled = true;
			}

			m_fTime += (Time.deltaTime / m_fGoalTime);

			m_fTime = Mathf.Clamp (m_fTime, 0.0f, 1.0f);

			// ベジエ曲線算出
			Vector3 vGoal = m_vGoalPosition + m_vGoalGap;
			transform.position = MathUtil.Bezier (
				m_vStartPosition,
				m_vStartPosition + m_vStartVec,
				vGoal + m_vGoalVec,
				vGoal,
				m_fTime);

			// 
			transform.eulerAngles = MathUtil.LerpAngle (m_vInitRot, m_vGoalRot, m_fTime);
		}

		/// <summary>
		/// ずり落ち
		/// </summary>
		void SlideFallMove() {
			m_fTime += Time.deltaTime;

			if (m_fTime <= m_fSlideStopTime)
				return;

			m_Mover.WorldMoveY (Time.deltaTime * -m_fSlideSpeed);

			transform.Rotate (m_fSlideRot * m_fTime);
		}

		/// <summary>
		/// ずり落ち開始
		/// </summary>
		public void SlideFall() {
			m_bSlideFall = true;
			m_fTime = 0.0f;
		}

		#endregion method
	}
}
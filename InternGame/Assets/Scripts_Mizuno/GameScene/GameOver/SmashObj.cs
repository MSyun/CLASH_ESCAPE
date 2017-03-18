using UnityEngine;
using System.Collections;


namespace Mizuno {
	/// <summary>
	/// 潰されたオブジェの動き
	/// </summary>
	public class SmashObj : MonoBehaviour {

		// 初期高さ
		[SerializeField, Range(3.0f, 10.0f)]private float	m_InitHeight;

		// 移動量
		Mover		m_Mover;
		[SerializeField]private float		m_fMoveY;
		[SerializeField]private float		m_fMoveZ;

		float	m_fTime = 0.0f;

		[SerializeField]private float		m_fRotateAngle = 10.0f;

		// sinカーブ
		[SerializeField]private float	m_fCycleTime = 1.0f;
		[SerializeField]private float	m_fCountMax = 30.0f;

		float	m_fInitAngle = 0.0f;

		// カメラの誤差
		[SerializeField]private Vector3	m_vCameraGapPos;
		[SerializeField]private Vector3	m_vCameraGapLook;

		// SE
		int		m_nSENum = 0;

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			Vector3 pos = transform.position;
			pos.y = m_InitHeight;
			transform.position = pos;

			m_Mover = GetComponent<Mover> ();

			m_fInitAngle = transform.eulerAngles.x;

			// カメラ
			Camera.main.transform.position = transform.position + m_vCameraGapPos;
			Camera.main.transform.LookAt (transform.position + m_vCameraGapLook);
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			m_fTime += Time.deltaTime;

			// 座標
			m_Mover.LocalMoveY( m_fMoveY / 2.5f + ( Mathf.Cos(Mathf.PI * 2.0f / m_fCycleTime * m_fTime * 2.0f) * m_fMoveY));
			m_Mover.LocalMove (Mathf.Sin(Mathf.PI * 2.0f / m_fCycleTime * m_fTime) * m_fMoveZ);

			// 角度
			Vector3 vAngle = transform.eulerAngles;
			vAngle.x = m_fInitAngle + (Mathf.Sin(Mathf.PI * 2.0f / m_fCycleTime * m_fTime) * m_fRotateAngle);
			transform.eulerAngles = vAngle;

			if (m_fTime >= m_fCountMax) {
				this.enabled = false;
			}

			SEPlay ();
		}

		void SEPlay() {
			if (m_fTime >= m_nSENum * m_fCountMax / 2) {
				++m_nSENum;
				SoundManager.Instance.PlaySE (14);
			}
		}
	}
}
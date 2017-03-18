using UnityEngine;
using System.Collections;

namespace Mizuno {
	/// <summary>
	/// プレイヤーが画面にぶつかった時のカメラ
	/// </summary>
	public class FaceCamera : MonoBehaviour {

		// sinカーブ
		[SerializeField]private float	m_fCycleTime = 1.0f;
		[SerializeField]private float	m_fCount = 0.0f;
		[SerializeField]private float	m_fStroke = 20.0f;
		[SerializeField]private float	m_fCountMax = 30.0f;
		[SerializeField, Range(-3.0f, 3.0f)]
		private float	m_fTimeSpeed = 1.0f;

		Vector3		m_vInitPos;

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_vInitPos = transform.position;

			SoundManager.Instance.PlaySE (13);
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			Vector3 Pos = transform.position;

			if (m_fCount >= m_fCountMax) {
				enabled = false;
				GameObject.Find ("Player").GetComponent<PlayerImpactCamera> ().SlideFall ();
			}
				
			Pos = m_vInitPos + (transform.forward * Mathf.Sin(Mathf.PI * 2 / m_fCycleTime * m_fCount) * m_fStroke);

			m_fCount += (Time.deltaTime * m_fTimeSpeed);

			transform.position = Pos;
		}
	}
}
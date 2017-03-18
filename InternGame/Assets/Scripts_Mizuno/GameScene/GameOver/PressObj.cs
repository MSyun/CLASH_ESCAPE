using UnityEngine;
using System.Collections;


namespace Mizuno {
	/// <summary>
	/// 車にゴールまで運ばれた後の動き
	/// </summary>
	public class PressObj : MonoBehaviour {

		public Vector3	m_vRot { get; set; }
		public Vector3	m_vVec { get; set; }

		float	m_fTime;
		public float	m_fMaxTime { get; set; }

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_fTime = 0.0f;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			transform.position += (m_vVec * Time.deltaTime * 60.0f);
			transform.Rotate( m_vRot * Time.deltaTime * 60.0f );

			m_fTime += Time.deltaTime;
			if (m_fTime >= m_fMaxTime)
				this.enabled = false;
		}
	}
}
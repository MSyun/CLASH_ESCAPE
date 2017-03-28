using UnityEngine;
using System.Collections;


namespace Mizuno {

	/// <summary>
	/// ボード回転用クラス
	/// </summary>
	public class RotateBoard : MonoBehaviour {

		#region variable

		// 回転速度
		[SerializeField]private Vector3		m_vRotSpeed;

		// 移動速度
		[SerializeField]private Vector3		m_vMoveSpeed;

		// 移動
		Mover		m_Mover;

		#endregion variable


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Mover = GetComponent<Mover> ();
			if (!m_Mover)
				Debug.Log (this.name + " : RotateBoardにMoverがないよ");
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 回転
			transform.Rotate (m_vRotSpeed * Time.deltaTime * 60.0f);
			// 移動
			Vector3 pos = transform.position;
			pos += (m_vMoveSpeed * Time.deltaTime * 60.0f);
			transform.position = pos;
		}

		#endregion unity method
	}
}
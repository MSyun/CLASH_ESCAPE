using UnityEngine;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// リジットボディの重力値変更クラス
	/// </summary>
	public class SetGravity : MonoBehaviour {

		public float m_fGravity;

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			Physics.gravity = new Vector3 (0, m_fGravity, 0);
		}
	}
}

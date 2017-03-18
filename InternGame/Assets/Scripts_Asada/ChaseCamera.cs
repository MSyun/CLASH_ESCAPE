using UnityEngine;
using System.Collections;


namespace Asada
{
	/// <summary>
	/// ターゲット追跡クラス
	/// </summary>
	public class ChaseCamera : MonoBehaviour {

		public Transform m_Target;	//ターゲットの座標
		Vector3 m_Distance;			//ターゲットまでの距離

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			//距離を求める
			m_Distance = transform.position - m_Target.position;	
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {
			Vector3 NewPos;

			//移動先の座標を求める
			NewPos.x = m_Target.position.x + m_Distance.x;
			NewPos.y = transform.position.y;
			NewPos.z = m_Target.position.z + m_Distance.z;

			//徐々に進める
			transform.position = Vector3.Lerp(transform.position,NewPos, 10.0f * Time.deltaTime);
		}
	}
}
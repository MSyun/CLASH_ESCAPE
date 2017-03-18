using UnityEngine;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// トラックのタイヤ管理クラス
	/// </summary>
	public class WheelController : MonoBehaviour {

		public Transform[] m_Wheel;		//タイヤ情報
		float  m_Circumference;			//円周
		Vector3 m_OldPos;				//過去座標
		float m_Angle;

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			
			//サイズ計算
			Vector3 Size;
			Size = new Vector3 (transform.parent.localScale.x * transform.localScale.x * m_Wheel [0].localScale.x,
								transform.parent.localScale.y * transform.localScale.y * m_Wheel [0].localScale.y,
								transform.parent.localScale.z * transform.localScale.z * m_Wheel [0].localScale.z);

			//円周を計算
			m_Circumference = 2 * Mathf.PI * (Size.y * 0.5f);

			//過去座標設定
			m_OldPos = transform.position;
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {

			Vector3 vMove;



			//移動量計算
			vMove = (transform.position + transform.parent.position) - m_OldPos;
			//Debug.Log (vMove);

			//回転量計算
			float fRot = (vMove.z / m_Circumference) * 360 *(Time.deltaTime*60);

			m_Angle += fRot;// / 180 * Mathf.PI;

			for(int i = 0; i < m_Wheel.Length;i++)
				m_Wheel[i].eulerAngles = new Vector3(m_Angle,0,0);// (Vector3.left, m_Angle);

			m_OldPos = transform.position + transform.parent.position;
		}
	}
}

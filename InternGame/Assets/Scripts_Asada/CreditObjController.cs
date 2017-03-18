using UnityEngine;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// クレジット用オブジェクト配置
	/// </summary>
	public class CreditObjController : MonoBehaviour {

		public float m_fTipSize;		//ステージチップのサイズ200
		public float m_fStartPos;		//最初の配置位置 100
		public float m_fIntervalCnt;	//配置間隔     3

		public GameObject[] m_CreditObject;	//配置するオブジェクト

		// Use this for initialization
		void Start () {
			//配置する
			for (int i = 0; i < m_CreditObject.Length; i++) {
				m_CreditObject [i].SetActive (true);
				m_CreditObject [i].transform.position = new Vector3 (0.0f, 26.0f, m_fStartPos + (i * (m_fTipSize * m_fIntervalCnt)));
			}
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
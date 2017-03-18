using UnityEngine;
using System.Collections;


namespace Asada
{
	/// <summary>
	/// 指定したオブジェクトに向ける
	/// </summary>
	public class LookCamera : MonoBehaviour {

		public Transform m_TargetObject;		//向ける対象オブジェクト
		public GameObject m_Star;				//演出用星
		public float m_fFarValue;				//描画範囲外の補正値

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			//対象オブジェクトに向ける
			transform.LookAt (m_TargetObject);

			//対象オブジェクトが描画範囲から出たら
			Vector3 Distance = m_TargetObject.position - transform.position;

			if ((Camera.main.farClipPlane - m_fFarValue) <= Distance.magnitude) {
				//描画範囲外に出たら
//				Debug.Log("出たー");
				m_Star.SetActive(true);	//星を表示する
				enabled = false;		//スクリプトを切る
			}
		}
	}
}

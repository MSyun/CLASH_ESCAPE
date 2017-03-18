using UnityEngine;
using System.Collections;


namespace Asada
{
	/// <summary>
	/// トラックの揺れ実装クラス
	/// </summary>
	public class ShakeTruck : MonoBehaviour {

		public float Period;			//揺れの周期
		public float DeflectionWidth;	//振れ幅
		float Count;
		Vector3 ShakeCenter;			//揺れの中心


		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			Count = 0f;
			ShakeCenter = transform.position;
		}

		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {

			//サイン波で揺れを計算
			Vector3 Diff = transform.up * (Period + Mathf.Sin (Mathf.PI * 2 / Period * Count) * DeflectionWidth);
			Count += Time.deltaTime * 60.0f;


			//揺れを反映
			transform.position =  new Vector3(transform.position.x,
				Diff.y + ShakeCenter.y ,
				transform.position.z);
		}
	}
}
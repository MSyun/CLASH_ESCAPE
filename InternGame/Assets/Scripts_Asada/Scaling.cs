using UnityEngine;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// オブジェクトを拡大縮小するクラス
	/// </summary>
	public class Scaling : MonoBehaviour {

		public float fMaxScale;		//最大拡大率
		public float fMinScale;		//最小拡大率
		public float fAddScale;		//加算値

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {

			//拡大率取得
			Vector3 Scale = transform.localScale;

			//拡大率加算
			Scale = new Vector3(Scale.x + fAddScale, Scale.y + fAddScale, Scale.z + fAddScale);

			//拡大率判定
			if (Scale.x >= fMaxScale || Scale.x <= fMinScale)
				fAddScale = -fAddScale;

			//代入
			transform.localScale = Scale;
		}
	}
}
using UnityEngine;
using System.Collections;
using Asada;

namespace Asada
{
	/// <summary>
	/// ライトオブジェクトクラス
	/// </summary>
	public class LightObject : MonoBehaviour {
		
		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake () {
			LightController.AddLightObj (gameObject);	//リストに追加する
		}

		/// <summary>
		/// 削除されたら
		/// </summary>
		void OnDestroy()
		{
			LightController.DeleteLightObj (gameObject);	//リストから削除する
		}

	}
}
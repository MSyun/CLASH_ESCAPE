using UnityEngine;
using System.Collections;
using Asada;



namespace Asada
{
	/// <summary>
	/// オブジェクト出現位置設定用クラス
	/// </summary>
	public class ObstaclePoint : MonoBehaviour {

		public GameObject m_ObstacleType;				//配置するオブジェクトの種類
		ObstacleController m_ObstacleController = null;	//障害物の親に設定するTransform


		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			//親Transformが設定されていなかったら
			if( m_ObstacleController == null){
				m_ObstacleController = GameObject.Find ("ObstacleController").GetComponent<ObstacleController>();
			}

			//オブジェクト生成
			GameObject Obj = (GameObject)Instantiate (m_ObstacleType, transform.position, Quaternion.identity);

			//親Transformの子に設定
			m_ObstacleController.AddObstacle(Obj);

			//削除
			Destroy(gameObject);
		}

		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {
		}

		/// <summary>
		/// ギズモ表示
		/// </summary>
		void OnDrawGizmos()
		{
			//色設定
			Gizmos.color = new Color (1, 0, 0, 0.5f);  
			Vector3 Size;

			//BoxColliderを取得
			BoxCollider Box = m_ObstacleType.GetComponent<BoxCollider> ();

			//あたり判定サイズ計算
			Size = new Vector3(m_ObstacleType.transform.localScale.x * Box.size.x,
				m_ObstacleType.transform.localScale.y * Box.size.y,
				m_ObstacleType.transform.localScale.z * Box.size.z);

			//ギズモ描画
			Gizmos.DrawCube (transform.position, Size);
		}
}
}

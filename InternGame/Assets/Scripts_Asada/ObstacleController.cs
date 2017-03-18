using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Asada
{
	/// <summary>
	/// 障害物を管理するクラス
	/// </summary>
	public class ObstacleController : MonoBehaviour {

		//オブジェクト管理用List
		List<GameObject> m_ObstacleList = new List<GameObject>();


		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			
		}

		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {
		
		}


		/// <summary>
		/// リスト内全削除
		/// </summary>
		void AllDeleteObstacle()
		{
			for (int i = 0; i < m_ObstacleList.Count; i++) {
				GameObject OldObj = m_ObstacleList [i];		//リストの一番古いものを取得
				m_ObstacleList.Remove(OldObj);				//リストから削除
				Destroy(OldObj);							//削除	
			}
		}


		/// <summary>
		/// リストに追加処理
		/// </summary>
		/// <param name="Obstacle">Obstacle.</param>
		public void AddObstacle(GameObject Obstacle)
		{
			//子に設定
			Obstacle.transform.parent = transform;

			//Listに追加
			m_ObstacleList.Add(Obstacle);
		}
	}
}
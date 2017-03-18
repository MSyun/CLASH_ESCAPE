using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;



namespace Asada
{
	/// <summary>
	/// クレジットシーンでのステージ管理クラス
	/// </summary>
	public class CreditStageController : MonoBehaviour {

		const int m_nStageTipSize = 180; 	//ステージチップのサイズ
		public Transform m_Player;			//生成基準のプレイヤーの座標 
		public Transform m_Track;			//削除基準のトラックの座標
		public int m_nPreInstantiate;		//先読み込みチップ数
		public int m_nDeleteValue;			//トラックのいくつ後ろのチップを削除するのか
		public GameObject m_StageTip;		//生成するステージチップ
		private List<GameObject> m_StageList = new List<GameObject> ();		//ステージチップ管理用のリスト

		int m_nCurrentTipIndex;				//現在のチップインデックス 



		// Use this for initialization
		void Start () {
			m_nCurrentTipIndex = -1;	//カウント設定

			//ステージチップ更新
			UpdateStage(m_nPreInstantiate);
		}




		// Update is called once per frame
		void Update () {
			int PlayerIndex = (int)(m_Player.position.z / m_nStageTipSize);	//現在のプレイヤーインデックスを計算
			if (PlayerIndex + m_nPreInstantiate > m_nCurrentTipIndex)		//プレイヤーのインデックスが現在のインデックスより大きかった
				UpdateStage (PlayerIndex + m_nPreInstantiate);				//ステージ更新

			//古いものを削除する
			int nTrackIndex = (int)(m_Track.position.z / m_nStageTipSize); //トラックのインデックスを計算

			while (m_StageList.Count > (m_nCurrentTipIndex - (nTrackIndex - m_nDeleteValue))) 
				OldDelete();
		}





		/// <summary>
		/// ステージチップ更新処理
		/// </summary>
		/// <param name="Index">Index.</param>
		void UpdateStage(int Index)
		{

			//現在のステージチップの数より少なかったら生成しない
			if (Index <= m_nCurrentTipIndex)
				return;

			//生成処理
			int nCnt;
			for (nCnt = m_nCurrentTipIndex + 1; nCnt <= Index; nCnt++) 
			{
				GameObject StageTipObj;
				StageTipObj = (GameObject)Instantiate(m_StageTip, new Vector3 (0, 0, nCnt * m_nStageTipSize), Quaternion.identity);		//生成
				//生成したものを子に設定する
				StageTipObj.transform.parent = transform;
				m_StageList.Add (StageTipObj);			//リストに追加
			}
			//現在のチップ数を更新
			m_nCurrentTipIndex = nCnt - 1;
		}




		/// <summary>
		/// リストの古いものから削除
		/// </summary>
		void OldDelete()
		{
			GameObject OldObj = m_StageList [0];	//リストの一番古いものを取得
			m_StageList.Remove(OldObj);				//リストから削除
			Destroy(OldObj);						//削除			
		}
	}
}

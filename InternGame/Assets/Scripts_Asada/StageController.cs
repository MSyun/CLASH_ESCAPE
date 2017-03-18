using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mizuno;

namespace Asada
{
	/// <summary>
	/// ステージチップ管理クラス
	/// </summary>
	public class StageController : MonoBehaviour {

		//生成方法種類
		enum GenerateType
		{
			RANDOM,	//ランダム生成
			ORDER,	//順番通りに生成
			TITLE,	//タイトルチップ生成
			SELECT,	//セレクトチップ生成
			GOAL	//ゴールチップ生成
		};

		const int m_nStageTipSize = 180; 	//ステージチップのサイズ 

		//ゴールまでのステージチップ数
		public int[] m_LevelGoalCount;	
		int		   m_GoalCount;			//現在のゴールまでのカウント


		public int StageTipSize {get { return m_nStageTipSize; } }
		public int GoalTipIndex {get { return m_nGoalTipIndex; } }


		float m_fStartPos;		//スタート座標
		float m_fGoalPos;		//ゴール座標
		public float StartPos {get {return m_fStartPos;}}
		public float GoalPos  {get {return m_fGoalPos;}}

		//読み込み先ファイル名
		public string[] m_StageFileName;
		string 			m_FolderName;

		public Transform m_Player;			//生成基準のプレイヤーの座標 
		public Transform m_Track;			//削除基準のトラックの座標
		public int m_nPreInstantiate;		//先読み込みチップ数
		public int m_nDeleteValue;			//トラックのいくつ後ろのチップを削除するのか
		public GameObject m_TitleTip;		//タイトルのステージチップ
		public GameObject m_SelectTip;		//セレクト画面のステージチップ
		public GameObject[] m_LevelGoalTip;	//各レベルのゴールチップ
		GameObject m_GoalTip;				//ゴールのステージチップ


		private List<GameObject> m_StageList = new List<GameObject> ();		//ステージチップ管理用のリスト
		List<GameObject> m_StageTipData = new List<GameObject>();			//ステージチップのプレファブデータ 

		public int m_nCountDownTipCount;			//カウントダウン用の先読みする数
		public GameObject m_FlatTip;				//何も乗っていないチップ


		int m_nCurrentTipIndex;				//現在のチップインデックス 
		int m_nGoalTipIndex;				//ゴールチップのインデックス保存用
		int m_nNormalModeTipCount;			//ノーマルモード時のチップCount
		int m_nNowMode;						//現在のモード格納用

		public GameObject m_StageObject;		//建物などのステージ上のオブジェクト




		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			InitStage ();												//ステージ初期化
			SetLevel ((int)LevelController.Instance.GetCurLevel());		//レベル設定
		}





		/// <summary>
		/// 初期化処理
		/// </summary>
		void InitStage()
		{
			//ゴールのインデックス初期化
			m_nGoalTipIndex = 0;

			//ステージチップ管理リストの中身を削除
			ALLDelete(m_StageList);


			//奥が見えないように何もないものを並べる
			GameObject StageObj,obj,CObj,SObj;
			for (int j= -10; j < -1; j++) {
				SObj = (GameObject)Instantiate (m_FlatTip, new Vector3 (0, 0, (j + m_nCurrentTipIndex) * m_nStageTipSize), Quaternion.identity);
				SObj.transform.parent = transform;	//生成した物を子に設定
				m_StageList.Add (SObj);				//リストに追加する
			}


			m_nCurrentTipIndex = -1;	//カウント設定

			//タイトルチップ配置
			StageObj = (GameObject)Instantiate (m_TitleTip, new Vector3 (0, 0, m_nCurrentTipIndex * m_nStageTipSize), Quaternion.identity);
			StageObj.transform.parent = transform;				//生成した物を子に設定	
			m_StageList.Add (StageObj);							//リストに追加する
			m_fStartPos = m_nCurrentTipIndex * m_nStageTipSize;	//スタート座標保存
			m_nCurrentTipIndex++;

			//セレクトチップ配置
			obj = (GameObject)Instantiate (m_SelectTip, new Vector3 (0, 0, m_nCurrentTipIndex * m_nStageTipSize), Quaternion.identity);
			obj.transform.parent = transform;	//生成した物を子に設定	
			m_StageList.Add (obj);				//リストに追加する

			int i;
			//カウントダウン用の先読みチップ配置
			for (i = 1; i <= m_nCountDownTipCount; i++) {
				CObj = (GameObject)Instantiate (m_FlatTip, new Vector3 (0, 0, (i + m_nCurrentTipIndex) * m_nStageTipSize), Quaternion.identity);
				CObj.transform.parent = transform;	//生成した物を子に設定
				m_StageList.Add (CObj);				//リストに追加する
			}

			m_nCurrentTipIndex += i - 1;

			//モード取得
			m_nNowMode = GameModeController.Instance.GetGameMode();
			m_nNormalModeTipCount = 0;

			//モードごとの設定
			if (m_nNowMode == (int)GameModeController._GameMode.TIME_ATTACK) {
				//タイムアタックモード
				m_FolderName = "TimeAttackMode/";	//読み込み先フォルダー設定
			} 
			else {
				//ノーマルモード
				m_FolderName = "NormalMode/";		//読み込み先フォルダー設定
			}
		}





		/// <summary>
		/// 各難易度ごとに設定
		/// </summary>
		/// <param name="nLevel">N level.</param>
		void SetLevel(int nLevel)
		{
			//ステージチップのプレファブリスト初期化
			ALLDelete(m_StageTipData);

			var Obj = Resources.LoadAll (m_FolderName + m_StageFileName[nLevel]);    //プレファブ読み込み
			SetList (Obj, m_StageTipData);                      	  //配列からListに入れ替える

			m_GoalTip = m_LevelGoalTip [nLevel];					  //ゴールのステージチップ設定


			//モードごとに設定
			if (m_nNowMode == (int)GameModeController._GameMode.TIME_ATTACK) {
				//タイムアタックモード時
				m_GoalCount = Obj.Length;		//ゴールまでのカウント設定
			} else {
				//ノーマルモード時
				m_GoalCount = m_LevelGoalCount [nLevel];              //ゴールまでのカウント設定
			}

			//Goal座標計算
			m_fGoalPos = m_nStageTipSize * (m_GoalCount + m_nCountDownTipCount + 1);

			//ステージチップ更新
			UpdateStage(m_nCurrentTipIndex);
		}




		/// <summary>
		/// アップデート関数
		/// </summary>
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

			//現在のステージチップの数より少ないか、Goalを生成していたら生成しない
			if (Index <= m_nCurrentTipIndex || m_GoalCount == -1)
				return;

			//生成処理
			int nCnt;
			for (nCnt = m_nCurrentTipIndex + 1; nCnt <= Index; nCnt++) 
			{
				GameObject StageTipObj;

				//Goal生成タイミングがきたら
				if (m_GoalCount == 0) {
					//ゴールを生成
					StageTipObj = GenerateStage (nCnt,(int)GenerateType.GOAL);	//生成
					m_StageList.Add (StageTipObj);
					m_nGoalTipIndex = nCnt;		//インデックス保存
					m_GoalCount = -1;
					break;
				}

				if (m_nNowMode == (int)GameModeController._GameMode.TIME_ATTACK) {
					StageTipObj = GenerateStage (nCnt,(int)GenerateType.ORDER);		//生成
				} else {
					StageTipObj = GenerateStage (nCnt);		//生成
				}
				m_StageList.Add (StageTipObj);			//リストに追加

				m_GoalCount--;
			}


			//Debug.Log("現在のリストの中の数:"+m_StageList.Count);
			//Debug.Log("最大生成数:"+(m_nPreInstantiate + 2));
			//Debug.Log("現在のプレイヤーのインデックス:"+Index);
			//Debug.Log("現在のTrackのインデックス:"+nTrackIndex);

			//現在のチップ数を更新
			m_nCurrentTipIndex = nCnt;
		}


		/// <summary>
		/// ステージチップ生成関数
		/// </summary>
		/// <returns>The stage.</returns>
		/// <param name="nIndex">N index.</param>
		/// <param name="Type">Type.</param>
		GameObject GenerateStage(int nIndex,int Type = (int)GenerateType.RANDOM)
		{
			GameObject StageTipObj = null;

			//指定されたものを生成
			switch ((GenerateType)Type) {
			//ランダム生成
			case GenerateType.RANDOM:
				int nNext = Random.Range (0, m_StageTipData.Count);
				StageTipObj = (GameObject)Instantiate (m_StageTipData[nNext], new Vector3 (0, 0, nIndex * m_nStageTipSize), Quaternion.identity);
				break;
			
			//ゴールを生成
			case GenerateType.GOAL:
				StageTipObj = (GameObject)Instantiate (m_GoalTip, new Vector3 (0, 0, nIndex * m_nStageTipSize), Quaternion.identity);
				break;

			//順番通り生成
			case GenerateType.ORDER:
				StageTipObj = (GameObject)Instantiate (m_StageTipData [m_nNormalModeTipCount], new Vector3 (0, 0, nIndex * m_nStageTipSize), Quaternion.identity);
				m_nNormalModeTipCount++;
				break;
			}



			//生成したものを子に設定する
			if( StageTipObj != null )
				StageTipObj.transform.parent = transform;


			GameObject LightObj;
			//生成した物にステージオブジェクトをつける
			if(Random.Range(1,5) == 1)
			{
				LightObj = GameObject.Instantiate(m_StageObject) as GameObject;	//コピー
				LightObj.transform.parent = StageTipObj.transform;				//親を設定
				LightObj.transform.localPosition = new Vector3 (0, 0, 0);		//座標を設定
				//LightObj.SetActive(true);										//Onにする
			}

			return StageTipObj;
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


		/// <summary>
		/// リスト内全削除
		/// </summary>
		/// <param name="List">List.</param>
		void ALLDelete(List<GameObject> List)
		{
			for (int i = 0; i < List.Count; i++) {
				GameObject OldObj = m_StageList [i];	//リストの一番古いものを取得
				m_StageList.Remove(OldObj);				//リストから削除
				Destroy(OldObj);						//削除	
			}
		}


		/// <summary>
		/// 配列の中身をリストに移す
		/// </summary>
		/// <param name="Array">Array.</param>
		/// <param name="List">List.</param>
		void SetList(Object[] Array, List<GameObject> List)
		{
			
			for (int i = 0; i < Array.Length; i++) {
				List.Add ((GameObject)Array [i]);
				//Debug.Log (Array [i]);
			}
		}
	}
}
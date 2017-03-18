using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Asada;


namespace Asada
{
	/// <summary>
	/// 残りの距離表示管理クラス
	/// </summary>
	public class DistanceScreenController : MonoBehaviour {

		public RectTransform DistanceImage;		//距離表示のRectTransform
		public RectTransform PlayerIconImage;	//プレイヤーのRectTransform
		public RectTransform TruckIconImage;	//トラックのRectTransform
		float MaxDistance;						//距離の長さ格納用
		float ImageStartPos;					//開始位置X格納用

		public Transform m_PlayerPos;			//プレイヤーの座標位置
		public Transform m_TruckPos;			//トラックの座標位置

		float m_fStartStagePos;					//ステージのスタート座標格納用
		float m_fGoalStagePos;					//ステージのゴール座標格納用
		float m_fStageDistance;					//ステージの長さ

		float m_fRatio;							//比率

		bool m_bInit;

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {

			//初期化フラグOFF
			m_bInit = false;

			//アイコンをスタート位置に変更する

			//スタート位置計算
			Vector2 StartPos = new Vector2(DistanceImage.anchoredPosition.x - (DistanceImage.rect.width * 0.5f),
				(DistanceImage.anchoredPosition.y - DistanceImage.rect.height * 0.5f));

			//スタート位置格納
			ImageStartPos = StartPos.x;

			//スタート位置に移動
			PlayerIconImage.anchoredPosition = new Vector2(StartPos.x , StartPos.y + (PlayerIconImage.rect.height * 0.5f));
			TruckIconImage.anchoredPosition = new Vector2(StartPos.x , StartPos.y + (TruckIconImage.rect.height * 0.5f));

			//長さ設定
			MaxDistance = DistanceImage.rect.width;
		}

		/// <summary>
		/// 初期化処理
		/// </summary>
		void Init()
		{
			m_bInit = true;
			
			//ステージの長さ取得
			StageController SteCon = GameObject.Find("StageController").GetComponent<StageController>();
			m_fStartStagePos = SteCon.StartPos;
			m_fGoalStagePos = SteCon.GoalPos;
			m_fStageDistance = m_fGoalStagePos - m_fStartStagePos;

			//比率の計算
			m_fRatio = MaxDistance / m_fStageDistance;
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {

			if (m_bInit == false)
				Init();

			//イメージの座標更新
			float fPlayerDistance = m_PlayerPos.position.z - m_fStartStagePos;	//進んだ距離を計算
			float fTruckDistance = m_TruckPos.position.z - m_fStartStagePos;	

			//Imageの位置計算
			PlayerIconImage.anchoredPosition = new Vector2(ImageStartPos + (m_fRatio * fPlayerDistance) ,PlayerIconImage.anchoredPosition.y);
			TruckIconImage.anchoredPosition = new Vector2(ImageStartPos + (m_fRatio * fTruckDistance),TruckIconImage.anchoredPosition.y);

		}
	}
}
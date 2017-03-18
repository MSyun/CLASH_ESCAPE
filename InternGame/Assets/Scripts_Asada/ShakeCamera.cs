﻿using UnityEngine;
using System.Collections;


namespace Asada
{
	/// <summary>
	/// カメラの揺れ実装クラス
	/// </summary>
	public class ShakeCamera : MonoBehaviour {

		Vector3 ShakeCenter;			//揺れの中心

		float angle = 0;
		public float Range;				//振れ幅
		public float SubRange;			//減少振れ幅
		float yspeed = 0.5f;


		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			ShakeCenter = transform.position;	//揺れの中心設定
		}

		/// <summary>
		/// Enableがtrueになった時
		/// </summary>
		void OnEnable()
		{
			ShakeCenter = transform.position;	//現在地を中心に

			MoveGoal ();
		}


		/// <summary>
		/// アップデート関数
		/// </summary>
		void Update () {
			if (Range <= 0.0f)
				return;

			Vector3 v = ShakeCenter;
			v.x = Mathf.Sin (angle) * Range;
			angle += yspeed;
			transform.position = v;
			Range -= SubRange;
		}


		/// <summary>
		/// ゴールに移動
		/// </summary>
		public void MoveGoal()
		{

			//カメラの座標を変える
			//ステージコントローラ取得
			StageController StaCol =  GameObject.Find("StageController").GetComponent<StageController>();

			transform.position = new Vector3 (0.0f,
				transform.position.y,
				StaCol.StageTipSize * StaCol.GoalTipIndex + StaCol.StageTipSize * 0.5f);

			transform.rotation = Quaternion.Euler (0, -180, 0);
		}
}
}
using UnityEngine;
using System.Collections;
using Asada;


namespace Mizuno {

	/// <summary>
	/// ステージセレクト用トラック
	/// </summary>
	public class TruckVerStageSelect : MonoBehaviour {

		#region variable

		Mover			m_Mover = null;

		// 移動速度
		[SerializeField]private float	m_fSpeed = 10.0f;

		#endregion variable


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Mover = GetComponent<Mover>();
			if (!m_Mover)
				Debug.Log (this.name + " : TruckVerStageSelectのMoverがないよ");

			// 振動開始
			GetComponent<ShakeTruck> ().enabled = true;
			// クラクションを鳴らす
			SoundManager.Instance.PlaySE(0);
			// エンジン音を鳴らす
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// トラックの移動
			m_Mover.WorldMove(m_fSpeed * Time.deltaTime);
		}

		#endregion unity method
	}

}
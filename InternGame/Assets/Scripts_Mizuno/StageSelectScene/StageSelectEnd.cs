using UnityEngine;
using System.Collections;


namespace Mizuno {

	public class StageSelectEnd : MonoBehaviour {

		/// <summary>
		/// ステージ選択シーン終了
		/// </summary>
		public void End() {
			if (!enabled)
				return;

			// トラックが突っ込んでくる
			GameObject.Find("StageSelect_Truck").GetComponent<TruckVerStageSelect>().enabled = true;
			// キャラクターがトラックを見る
			GameObject.Find("Player").GetComponent<PlayerMotion>().Change();
			// 看板が飛ぶ
			GameObject.Find("RotateBoard").GetComponent<RotateBoard>().enabled = true;
			// ボタン
			GameObject.Find("ConfigButton").SetActive(false);
			GameObject.Find ("CreditButton").SetActive (false);
			GameObject.Find ("OperatingButton").SetActive (false);


			this.enabled = false;
		}
	}
}
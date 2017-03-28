using UnityEngine;
using System.Collections;
using Asada;


namespace Mizuno {

	/// <summary>
	/// ゴール用あたり判定クラス
	/// </summary>
	public class GoalCheck : MonoBehaviour {

		/// <summary>
		/// 当たり判定であたっている間
		/// </summary>
		/// <param name="col">当たった相手</param>
		void OnTriggerEnter(Collider col) {
			// プレイヤーがゴールした
			if (col.gameObject.tag == "Player") {
				//----- ゴール！！
				// タイマーをとめる
				GameTimer time = GameObject.Find("Timer").GetComponent<GameTimer>();
				time.End();
				// クリア
				GameObject.Find("Result").GetComponent<ClearGenerator>().Clear();
				// ゲームUI
				GameObject.Find("Canvas").SetActive(false);

				// タイマーを記録
				if(GameModeController.Instance.GetGameMode() == (int)GameModeController._GameMode.TIME_ATTACK)
					GameObject.Find("LevelController").GetComponent<LevelController>().SetRecord(time.GameTime);
				// プレイヤー移動
				GameObject.Find("Player").GetComponent<Player>().Clear();
				GameObject.Find ("Player").GetComponent<BoxCollider> ().enabled = false;
				// カメラの移動を止める
				Camera.main.GetComponent<ChaseCamera>().enabled = false;
				// 
				Camera.main.GetComponent<ShakeCamera>().MoveGoal();

				
				//残り距離の表示を消す
				GameObject.Find("DistanceScreen").GetComponent<Canvas>().enabled = false;

				//トラック
				GameObject.Find("Mini_Truck").GetComponent<Truck>().GoalMove();

				// 自身はもう必要ない
				this.enabled = false;
			}
		}
	}

}
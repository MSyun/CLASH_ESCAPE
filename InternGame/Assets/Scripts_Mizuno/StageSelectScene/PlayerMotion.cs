using UnityEngine;
using System.Collections;
using Asada;


namespace Mizuno {

	/// <summary>
	/// プレイヤーの動き
	/// </summary>
	public class PlayerMotion : MonoBehaviour {

		#region variable

		// 顔の回転角度
		[SerializeField]private float[]	m_fBoardAngle;	// 板を見ている
		[SerializeField]private float	m_fTruckAngle = 90.0f;	// トラックを見ている

		// 状態フラグ
		bool	m_bTrig = false;

		// 計測時間
		float	m_fTime = 0.0f;

		// トラックを見ている時間
		[SerializeField]private float	m_fLookTruckTime = 1.0f;

		// 首のボーン
		[SerializeField]private Transform[]		m_Neck;

		#endregion variable


		#region method

		/// <summary>
		/// 行動変更
		/// </summary>
		public void Change() {
			m_bTrig = true;
			GameObject.Find("Player").GetComponent<Animator>().SetTrigger("tLook");
		}

		#endregion method


		#region unity method

		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake() {
			SoundManager.Instance.PlayBGM (0);
		}


		/// <summary>
		/// 更新
		/// </summary>
		void LateUpdate () {
			Quaternion quaternionNeck;
			if (!m_bTrig) {
				// 看板を向く
				for (int i = 0; i < m_fBoardAngle.Length; i++) {
					quaternionNeck = Quaternion.AngleAxis (m_fBoardAngle[i], Vector3.forward);
					m_Neck[i].localRotation = quaternionNeck;
				}
			} else {
				// トラックを向く
				quaternionNeck = Quaternion.AngleAxis (m_fTruckAngle, Vector3.right);
				m_Neck[0].localRotation = quaternionNeck;

				// 時間計測
				m_fTime += Time.deltaTime;

				// 逃げてシーン遷移
				if (m_fTime >= m_fLookTruckTime) {
					// 逃げる
					GetComponent<PlayerRunAway> ().enabled = true;
					this.enabled = false;
					// シーン遷移
					SceneChanger.SetScene("Game");
				}
			}
		}

		#endregion unity method
	}
}
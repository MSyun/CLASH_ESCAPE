using UnityEngine;
using System.Collections;


namespace Mizuno {

	public class CountDownMove : MonoBehaviour {

		// トラック
		[SerializeField]private Truck		m_Truck;

		// 移動
		Mover		m_Mover;


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Mover = GetComponent<Mover> ();
			if (!m_Mover)
				Debug.Log (this.name + " : CountDownMoveのMoverがないよ!");

			SoundManager.Instance.PlayBGM (1);
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			// 移動
			m_Mover.LocalMove( m_Truck.Speed * Time.deltaTime);
		}
	}

}
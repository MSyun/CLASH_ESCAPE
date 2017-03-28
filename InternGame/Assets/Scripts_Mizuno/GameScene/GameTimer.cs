using UnityEngine;
using System.Collections;


namespace Mizuno {

	/// <summary>
	/// ゲーム時間計測
	/// </summary>
	public class GameTimer : MonoBehaviour {

		#region variable

		// 経過時間
		float	m_fGameTime;
		public float GameTime { get{ return m_fGameTime; } }
		// 計測中確認
		bool	m_bPlay = false;
		// 最大時間
		[SerializeField]private float	m_fMaxTime = 600.0f;

		#endregion variable


		#region method

		/// <summary>
		/// リセット
		/// </summary>
		void Reset() {
			m_fGameTime = 0.0f;
		}


		/// <summary>
		/// 開始
		/// </summary>
		public void Play() {
			m_bPlay = true;
		}


		/// <summary>
		/// 終了
		/// </summary>
		public void End() {
			m_bPlay = false;
		}

		#endregion method


		#region unity method

		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake() {
			m_fGameTime = 0.0f;
			m_bPlay = false;
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update() {
			// 計測確認
			if (!m_bPlay)	return;

			// 時間加算
			m_fGameTime += Time.deltaTime;

			// 時間の補正
			m_fGameTime = Mathf.Clamp( m_fGameTime, 0.0f, m_fMaxTime );
		}

		#endregion unity method
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Mizuno {

	public class FPSDrawer : MonoBehaviour {

		//----- シングルトン
		protected static FPSDrawer instance;

		public static FPSDrawer Instance {
			get{
				if (!instance) {
					Debug.Log ("FPSDrawer Instance Not Found");
					return null;
				}
				return instance;
			}
		}


		int		m_FrameCount;	// フレームカウンタ
		float	m_NextTime;		// 次の時間
		float	m_fFPS;			// 現在のFPS

		bool	m_bDraw = false;		// 描画フラグ
		public bool Draw{ get{ return m_bDraw; } }

		// 描画用テキスト
		[SerializeField]private Text	m_text;


		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake() {
			// 作成済み確認
			if (instance) {
				Destroy (gameObject);
				return;
			}
				
			instance = this;

			DontDestroyOnLoad (this);
		}


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_NextTime = Time.time + 1;
			m_fFPS = 0.0f;
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			m_FrameCount++;

			if (Time.time >= m_NextTime) {
				// 1秒たったらFPSを表示
				m_fFPS = m_FrameCount;
				m_FrameCount = 0;
				m_NextTime += 1;
			}
			if (m_bDraw) {
				m_text.text = "FPS : " + m_fFPS;
			} else {
				m_text.text = "";
			}
		}


		/// <summary>
		/// 描画の変更
		/// </summary>
		public void ChangeDraw() {
			m_bDraw ^= true;
		}
	}

}
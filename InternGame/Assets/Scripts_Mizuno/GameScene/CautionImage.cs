using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Mizuno {

	public class CautionImage : MonoBehaviour {

		Image		m_Image;

		// 計測
		float		m_fTime = 0.0f;

		// 表示
		bool		m_bDraw = false;

		// 回数
		int			m_nDispNum;

		// 
		[SerializeField]private float	m_fDrawTime = 1.0f;


		/// <summary>
		/// 初期化
		/// </summary>
		void Start() {
			m_Image = GetComponent<Image> ();
			if (!m_Image)
				Debug.Log (this.name + " : CautionImageにImageがないよ");

			Color color = m_Image.color;
			color.a = 0.0f;
			m_Image.color = color;
		}

		
		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			if (m_bDraw) {
				m_fTime += Time.deltaTime;

				// 
				Switch();
			} else {

			}
		}

		/// <summary>
		/// スイッチ
		/// </summary>
		void Switch() {
			if (m_fTime >= m_fDrawTime) {
				Color color = m_Image.color;
				color.a = 0.0f;
				m_Image.color = color;

				m_fTime = 0.0f;
				m_bDraw = false;
			}
		}

		/// <summary>
		/// 出現
		/// </summary>
		public void Display() {
			m_bDraw = true;

			Color color = m_Image.color;
			color.a = 1.0f;
			m_Image.color = color;

			SoundManager.Instance.PlaySE (8);
		}
	}
}
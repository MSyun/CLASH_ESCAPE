using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Mizuno {

	public class Frame : MonoBehaviour {

		// 画像
		Image	m_Image;

		float							m_fAlpha = 0.0f;	// 現在のアルファ値
		[SerializeField]private float	m_fCycle = 1.0f;	// 出現時間

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Image = GetComponent<Image> ();
			if (!m_Image)
				Debug.Log (this.name + " : Frameクラスのイメージがないよ");
		}


		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			float Alpha = 1.0f / m_fCycle;

			// アルファ値加算
			m_fAlpha += (Alpha * Time.deltaTime);

			// 色の変更
			Color old = m_Image.color;
			old.a = m_fAlpha;
			m_Image.color = old;
		}
	}

}
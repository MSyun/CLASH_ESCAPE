using UnityEngine;
using System.Collections;

using UnityEngine.UI;


namespace Mizuno {
	public class ExplosionPlayer : MonoBehaviour {

		Animator		m_Anim;

		Image			m_Image;

		GameObject		m_BombButton;

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Anim = GetComponent<Animator> ();
			if (!m_Anim)
				Debug.Log (this.name + " : ExplosionAnimにAnimatorがないよ");

			m_Image = GetComponent<Image> ();
			if (!m_Image)
				Debug.Log (this.name + " : ExplosionAnimにImageがないよ");
			ChangeAlpha (0.0f);

			m_BombButton = GameObject.Find ("DeleteButton");
		}

		/// <summary>
		/// 再生
		/// </summary>
		public void Play() {
			m_Anim.SetTrigger ("Explosion");
			ChangeAlpha (1.0f);
			m_BombButton.SetActive (false);
			SoundManager.Instance.PlaySE (11);
		}

		/// <summary>
		/// 終了
		/// </summary>
		public void End() {
			ChangeAlpha (0.0f);
			m_BombButton.SetActive (true);
		}

		/// <summary>
		/// アルファ値の変更
		/// </summary>
		/// <param name="alpha">新しいアルファ値</param>
		void ChangeAlpha( float alpha ) {
			Color color = m_Image.color;
			color.a = alpha;
			m_Image.color = color;
		}
	}
}
using UnityEngine;
using System.Collections;

namespace Mizuno {

	/// <summary>
	/// サウンドマネージャー
	/// </summary>
	public class SoundManager : MonoBehaviour {

		#region Singleton

		//----- シングルトン
		protected static SoundManager instance;

		public static SoundManager Instance {
			get{
				if (!instance) {
					Debug.Log ("SoundManager Instance Not Found");
					return null;
				}
				return instance;
			}
		}

		#endregion Singleton


		#region variable

		//----- 最大同時再生数
		public static int	m_nMaxPlay = 10;

		//----- AudioSource
		AudioSource		m_BGMSource;	// BGM
		AudioSource[]	m_SESources = new AudioSource[m_nMaxPlay];	// SE
		AudioSource[]	m_VoiceSources = new AudioSource[m_nMaxPlay];	// Voice

		//----- AudioClip
		public AudioClip[]	m_BGM;
		public AudioClip[]	m_SE;
		public AudioClip[]	m_Voice;

		#endregion variable


		#region method

		/// <summary>
		/// BGM再生
		/// </summary>
		/// <param name="index">BGM番号</param>
		public void PlayBGM(int index) {
			// 例外処理
			if (index < 0 || m_BGM.Length <= index) return;

			// 現在再生しているか確認
			if (m_BGMSource.clip == m_BGM[index]) return;

			m_BGMSource.Stop();
			m_BGMSource.clip = m_BGM[index];
			m_BGMSource.Play();
		}

		/// <summary>
		/// 全BGM停止
		/// </summary>
		public void StopBGM() {
			m_BGMSource.Stop();
			m_BGMSource.clip = null;
		}

		/// <summary>
		/// SE再生
		/// </summary>
		/// <param name="index">SE番号</param>
		public void PlaySE(int index) {
			// 例外処理
			if (index < 0 || m_SE.Length <= index) return;

			// 未使用のAudioSourceで鳴らす
			foreach (AudioSource source in m_SESources) {
				if (!source.isPlaying) {
					source.clip = m_SE[index];
					source.Play();
					return;
				}
			}
		}

		public void RePlaySE(int index) {
			// 例外処理
			if (index < 0 || m_SE.Length <= index) return;

			// 使用中のAudioSourceで鳴らす
			AudioSource NotPlaySource = null;
			foreach (AudioSource source in m_SESources) {
				if (source.isPlaying) {
					if (source.clip == m_SE[index]) {
						source.Play();
						return;
					}
				} else {
					NotPlaySource = source;
				}
			}

			if (!NotPlaySource)
				return;

			NotPlaySource.clip = m_SE[index];
			NotPlaySource.Play();
		}

		/// <summary>
		/// SE停止
		/// </summary>
		/// <param name="index">SE番号</param>
		public void StopSE(int index) {
			// 例外処理
			if (index < 0 || m_SE.Length <= index) return;

			foreach (AudioSource source in m_SESources) {
				if (source.clip == m_SE[index]) {
					source.Stop();
					source.clip = null;
					return;
				}
			}
		}

		/// <summary>
		/// 全SE停止
		/// </summary>
		public void StopSE() {
			foreach (AudioSource source in m_SESources) {
				source.Stop();
				source.clip = null;
			}
		}

		/// <summary>
		/// Voice再生
		/// </summary>
		/// <param name="index">Voice番号</param>
		public void PlayVoice(int index) {
			// 例外処理
			if (index < 0 || m_Voice.Length <= index) return;

			// 未使用のAudioSourceで鳴らす
			foreach (AudioSource source in m_VoiceSources) {
				if (!source.isPlaying) {
					source.clip = m_Voice[index];
					source.Play();
					return;
				}
			}
		}

		/// <summary>
		/// Voice停止
		/// </summary>
		/// <param name="index">Voice番号</param>
		public void StopVoice(int index) {
			// 例外処理
			if (index < 0 || m_Voice.Length <= index) return;

			foreach (AudioSource source in m_VoiceSources) {
				if (source.clip == m_Voice[index]) {
					source.Stop();
					source.clip = null;
					return;
				}
			}
		}

		/// <summary>
		/// 全Voice停止
		/// </summary>
		public void StopVoice() {
			foreach (AudioSource source in m_VoiceSources) {
				source.Stop();
				source.clip = null;
			}
		}

		#endregion method


		#region unity method

		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake() {
			if (instance) {
				Destroy (gameObject);
				return;
			}

			// 登録
			instance = this;

			DontDestroyOnLoad(gameObject);

			//----- AudioSourceを追加
			// BGM
			m_BGMSource = gameObject.AddComponent<AudioSource>();
			m_BGMSource.loop = true;	// ループ
			// SE
			for (int i = 0; i < m_SESources.Length; ++i) {
				m_SESources [i] = gameObject.AddComponent<AudioSource> ();
			}
			// Voice
			for (int i = 0; i < m_VoiceSources.Length; ++i) {
				m_VoiceSources [i] = gameObject.AddComponent<AudioSource> ();
			}
		}

		#endregion unity method
	}

}
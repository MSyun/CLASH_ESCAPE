using UnityEngine;
using System.Collections;


namespace Mizuno {

	public class LevelController : MonoBehaviour {

		//----- Singleton
		private static LevelController mInstance = null;

		public static LevelController Instance {
			get {
				return mInstance;
			}
		}

		/// <summary>
		/// ゲーム難易度
		/// </summary>
		public enum _StageLevel {
			LEVEL_EASY = 0,
			LEVEL_NORMAL,
			LEVEL_HARD,
			LEVEL_NIGHTMARE,
			LEVEL_MAX,
		};

		// 現在のレベル
		int		m_nCurrentLevel = (int)_StageLevel.LEVEL_EASY;

		// 記録保存
		[SerializeField]private float[]	m_fRecordTime = new float[(int)_StageLevel.LEVEL_MAX];	// 最速記録

		[SerializeField]private float		m_fDefaultTime = 300.0f;


		/// <summary>
		/// アウェイク
		/// </summary>
		void Awake () {
			//----- Singleton
			if (mInstance == null) {
				mInstance = this;
			} else {
				Destroy (this);
				return;
			}

			DontDestroyOnLoad (this);

			// データ
			for (int i = 0; i < (int)_StageLevel.LEVEL_MAX; i++) {
				// 取り出し
				m_fRecordTime[i] = PlayerPrefs.GetFloat (""+i, m_fDefaultTime);
			}
		}

		/// <summary>
		/// 記録設定
		/// </summary>
		/// <returns><c>true</c>記録更新, <c>false</c>記録以下.</returns>
		/// <param name="time">Time.</param>
		public bool SetRecord( float time ) {
			// 記録更新ならず
			if (time >= m_fRecordTime[m_nCurrentLevel]) {
				return false;
			}

			// 記録更新
			PlayerPrefs.SetFloat ("" + m_nCurrentLevel, time);
			m_fRecordTime[m_nCurrentLevel] = time;
			return true;
		}

		// 記録の取得
		/// <summary>
		/// 記録の取得
		/// </summary>
		/// <returns>現在の最速時間</returns>
		/// <param name="Level">ゲーム難易度</param>
		public float GetRecord( int Level ) {
			// そんな難易度はありません
			if (!CheckLevel(Level)) {
				return -1.0f;
			}

			return m_fRecordTime[Level];
		}
			
		/// <summary>
		/// 全データ削除
		/// </summary>
		public void DeleteRecord() {
			for (int i = 0; i < (int)_StageLevel.LEVEL_MAX; i++) {
				// データ取り出し
				PlayerPrefs.DeleteKey("" + i);
				m_fRecordTime[i]	= PlayerPrefs.GetFloat (""+i, m_fDefaultTime);
//				if( Application.isEditor )
//					Debug.Log (i + "番目の最高新記録を削除しました");
			}
		}


		/// <summary>
		/// 現在の難易度を返す
		/// </summary>
		/// <returns>現在の難易度</returns>
		public int GetCurLevel() {
			return m_nCurrentLevel;
		}

		/// <summary>
		/// 難易度の設定
		/// </summary>
		/// <param name="level">新しい難易度</param>
		public void SetCurLevel(int level) {
			if (!CheckLevel (level))
				return;

			m_nCurrentLevel = level;
		}

		/// <summary>
		/// 難易度の存在を確認
		/// </summary>
		/// <returns><c>true</c>難易度がある, <c>false</c> 難易度がない</returns>
		/// <param name="level">難易度</param>
		bool CheckLevel( int level ) {
			// ない
			if (level < 0 || (int)_StageLevel.LEVEL_MAX <= level)
				return false;

			// ある
			return true;
		}
	}

}
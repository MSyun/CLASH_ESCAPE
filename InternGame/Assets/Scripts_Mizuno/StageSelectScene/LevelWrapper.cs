using UnityEngine;


namespace Mizuno {

	public class LevelWrapper : MonoBehaviour {

		LevelController		m_Controller;

		[SerializeField]private RecordGetter[]		m_Record;


		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_Controller = GameObject.Find ("LevelController").GetComponent<LevelController> ();
		}


		/// <summary>
		/// 難易度の設定
		/// </summary>
		/// <param name="Level">難易度</param>
		public void SetLevel( int Level ) {
			m_Controller.SetCurLevel (Level);
		}


		/// <summary>
		/// データの削除
		/// </summary>
		public void Delete() {
			// 爆発
			GameObject.Find("ExplosionImage").GetComponent<ExplosionPlayer>().Play();

			// データの消去
			m_Controller.DeleteRecord ();

			// テキストを変更
			for (int i = 0; i < m_Record.Length; i++) {
				m_Record [i].ChangeTime ();
			}
		}
	}

}
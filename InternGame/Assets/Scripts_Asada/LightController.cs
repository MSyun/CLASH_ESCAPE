using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mizuno;

namespace Asada
{
	/// <summary>
	/// ライト管理クラス
	/// </summary>
	public class LightController : MonoBehaviour {

		//ライトオブジェクトのリスト
		static List<GameObject> m_LightObjList = new List<GameObject> ();

		public Material m_NightSky;	//夜のスカイボックスマテリアル

		//夜の色設定用
		public Color m_NightSkyColor;
		public Color m_NightEquatorColor;
		public Color m_NightGroundColor;

		public float m_fLightIntensity;		//ライトの明るさ


		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			//難易度を取得してライトを設定するか判別
			if (LevelController.Instance.GetCurLevel () == (int)LevelController._StageLevel.LEVEL_NIGHTMARE) {
				//難易度がナイトメアの場合

				//スカイボックス変更
				RenderSettings.skybox = m_NightSky;

				//明るさ設定
				GameObject.Find ("Directional Light").GetComponent<Light> ().intensity = m_fLightIntensity;
				RenderSettings.ambientSkyColor = m_NightSkyColor;
				RenderSettings.ambientEquatorColor = m_NightEquatorColor;
				RenderSettings.ambientGroundColor = m_NightGroundColor;

				//ライトオブジェクトON
				OnLight ();
			} else {
				//それ以外の場合

				//ライトオブジェクトOFF
				OffLight ();
			}
		}



		/// <summary>
		/// 更新処理
		/// </summary>
		void Update () {
		}



		/// <summary>
		/// Listにオブジェクトを追加
		/// </summary>
		/// <param name="obj">Object.</param>
		static public void AddLightObj(GameObject obj)
		{
			m_LightObjList.Add (obj);

			if (LevelController.Instance.GetCurLevel () == (int)LevelController._StageLevel.LEVEL_NIGHTMARE) {
				//難易度がナイトメアの場合
				obj.SetActive(true);
			} else {
				obj.SetActive(false);
			}
		}


		/// <summary>
		/// リストからオブジェクトを削除
		/// </summary>
		/// <param name="obj">Object.</param>
		static public void DeleteLightObj(GameObject obj)
		{
			m_LightObjList.Remove (obj);
		}


		/// <summary>
		/// 全ライトオブジェクトをOnにする
		/// </summary>
		static public void OnLight()
		{
			for (int nCnt = 0; nCnt < m_LightObjList.Count; nCnt++) {
				m_LightObjList [nCnt].SetActive (true);
			}
		}



		/// <summary>
		/// 全ライトオブジェクトをOffにする
		/// </summary>
		static public void OffLight()
		{
			for (int nCnt = 0; nCnt < m_LightObjList.Count; nCnt++) {
				m_LightObjList [nCnt].SetActive (false);
			}
		}
	}
}
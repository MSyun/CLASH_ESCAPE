using UnityEngine;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// マテリアル読み込みクラス
	/// </summary>
	public class LoadMaterial : MonoBehaviour {

		public string m_FolderName;			//読み込み先フォルダ名
		public string[] m_FileName;			//読み込むファイル名

		[SerializeField]private bool	m_bSkin = false;

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			//Materialを設定
			Material[] mal = new Material[m_FileName.Length];

			for (int i = 0; i < m_FileName.Length; i++) {
				mal[i] = Resources.Load (m_FolderName + m_FileName[i]) as Material;
			}

			if (!m_bSkin)
				gameObject.GetComponent<Renderer> ().materials = mal;
			else
				gameObject.GetComponent<SkinnedMeshRenderer> ().materials = mal;

			enabled = false;
		}
	}
}
using UnityEngine;
using System.Collections;


namespace Mizuno {

	/// <summary>
	/// 丸影
	/// </summary>
	public class FootShadow : MonoBehaviour {

		#region variable

		// 座標誤差
		[SerializeField]private Vector3		m_PosGap;

		// 持ち主
		[SerializeField]private GameObject	m_OwnerObj;

		// 影の出る高さ
		[SerializeField]private float	m_fAppearHeight = 0.0f;

		// 影の消える高さ
		[SerializeField]private float	m_fGhostHeight = 5.0f;

		// 影の画像
		Material		m_ShadowMat = null;

		#endregion variable


		#region method

		/// <summary>
		/// 影のアルファ値を変更
		/// </summary>
		void ShadowAlpha() {
			float Height = m_OwnerObj.transform.position.y;

			Height = Mathf.Clamp(Height, m_fAppearHeight, m_fGhostHeight);

			float Alpha = 1.0f - Height / (m_fGhostHeight - m_fAppearHeight);

			Color color = new Color(1.0f, 1.0f, 1.0f, Alpha);
			color.a = Alpha;
			m_ShadowMat.color = color;
		}

		#endregion method


		#region unity method

		/// <summary>
		/// 初期化
		/// </summary>
		void Start () {
			m_ShadowMat = GetComponent<MeshRenderer> ().material;
			if (!m_ShadowMat)
				Debug.Log (this.name + " : FootShadowにMaterialがないよ");
		}
		

		/// <summary>
		/// 更新
		/// </summary>
		void Update () {
			ShadowAlpha ();

			// 座標変更
			Vector3 pos = m_OwnerObj.transform.position;
			gameObject.transform.position = new Vector3 (pos.x + m_PosGap.x, m_fAppearHeight, pos.z + m_PosGap.z);
		}

		#endregion unity method
	}

}
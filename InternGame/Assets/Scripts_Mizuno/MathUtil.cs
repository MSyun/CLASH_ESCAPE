using UnityEngine;
using System.Collections;

namespace Mizuno {

	/// <summary>
	/// 計算便利クラス
	/// </summary>
	public static class MathUtil {

		/// <summary>
		/// 線形保管角度3次元ベクトル型
		/// </summary>
		/// <returns>線形保管角度3次元ベクトル型</returns>
		/// <param name="a">始点の3次元ベクトル</param>
		/// <param name="b">終点の3次元ベクトル</param>
		/// <param name="t">0.0 ~ 1.0 の時間</param>
		public static Vector3 LerpAngle( Vector3 a, Vector3 b, float t ) {
			return new Vector3 (
				Mathf.LerpAngle (a.x, b.x, t),
				Mathf.LerpAngle (a.y, b.y, t),
				Mathf.LerpAngle (a.z, b.z, t));
		}

		/// <summary>
		/// 3次ベジエ曲線
		/// </summary>
		/// <param name="p1">点1</param>
		/// <param name="p2">点2</param>
		/// <param name="p3">点3</param>
		/// <param name="p4">点4</param>
		/// <param name="t">中間点</param>
		public static float Bezier( float p1, float p2, float p3, float p4, float t ) {
			float buf = 1 - t;
			float buf2 = buf * buf;
			float t2 = t * t;

			return 	buf2 * buf * p1 +
				3 * buf2 * t * p2 +
				3 * buf *t2 * p3 +
				t2 * t * p4;
		}

		/// <summary>
		/// ベクトル型3次ベジエ曲線
		/// </summary>
		/// <param name="p1">点1</param>
		/// <param name="p2">点2</param>
		/// <param name="p3">点3</param>
		/// <param name="p4">点4</param>
		/// <param name="t">中間点</param>
		public static Vector3 Bezier( Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t) {
			return new Vector3 (
				Bezier (p1.x, p2.x, p3.x, p4.x, t),
				Bezier (p1.y, p2.y, p3.y, p4.y, t),
				Bezier (p1.z, p2.z, p3.z, p4.z, t));
		}
	}
}
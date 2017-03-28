using UnityEngine;


namespace Mizuno {

	/// <summary>
	/// 移動系まとめ
	/// </summary>
	public class Mover : MonoBehaviour {

		/// <summary>
		/// X軸ローカル移動
		/// </summary>
		/// <param name="speed">速度</param>
		public void LocalMoveX( float speed ) {
			Vector3 Vec = transform.right * speed;
			Vector3 Pos = transform.position;
			transform.position = Pos + Vec;
		}

		/// <summary>
		/// Y軸ローカル移動
		/// </summary>
		/// <param name="speed">速度</param>
		public void LocalMoveY( float speed ) {
			Vector3 Vec = transform.up * speed;
			Vector3 Pos = transform.position;
			transform.position = Pos + Vec;
		}

		/// <summary>
		/// Z軸ローカル移動
		/// </summary>
		/// <param name="speed">速度</param>
		public void LocalMove( float speed ) {
			Vector3 Vec = transform.forward * speed;
			Vector3 Pos = transform.position;
			transform.position = Pos + Vec;
		}

		/// <summary>
		/// X軸ワールド移動
		/// </summary>
		/// <param name="speed">速度</param>
		public void WorldMoveX( float speed ) {
			Vector3 pos = transform.position;
			pos.x += speed;
			transform.position = pos;
		}

		/// <summary>
		/// Y軸ワールド移動
		/// </summary>
		/// <param name="speed">速度</param>
		public void WorldMoveY( float speed ) {
			Vector3 pos = transform.position;
			pos.y += speed;
			transform.position = pos;
		}

		/// <summary>
		/// Z軸ワールド移動
		/// </summary>
		/// <param name="speed">速度</param>
		public void WorldMove( float speed ) {
			Vector3 pos = transform.position;
			pos.z += speed;
			transform.position = pos;
		}
	}

}
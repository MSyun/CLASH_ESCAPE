﻿using UnityEngine;

namespace Mizuno {

	/// <summary>
	/// 入力便利クラス
	/// </summary>
	public static class TouchUtil {

		#region variable

		// タッチ座標保存
		private static Vector3 TouchPosition = Vector3.zero;

		#endregion variable


		#region method

		/// <summary>
		/// タッチ判定をするか確認
		/// </summary>
		/// <returns>true. タッチ : false. マウス</returns>
		static bool CheckTouch() {
			if (Application.isEditor ||
				Application.platform == RuntimePlatform.WindowsPlayer ||
				Application.platform == RuntimePlatform.OSXPlayer) {
				return false;
			}

			return true;
		}

		/// <summary>
		/// 入力情報を取得
		/// </summary>
		/// <returns>入力の種類</returns>
		public static TouchInfo GetTouch() {
			// エディター確認
			if (!CheckTouch()) {
				if (Input.GetMouseButtonDown (0))	return TouchInfo.Began;
				if (Input.GetMouseButton (0))		return TouchInfo.Moved;
				if (Input.GetMouseButtonUp (0))		return TouchInfo.Ended;
			} else {
				if (Input.touchCount > 0) {
					return (TouchInfo)((int)Input.GetTouch (0).phase);
				}
			}

			return TouchInfo.None;
		}


		/// <summary>
		/// 入力座標を取得
		/// </summary>
		/// <returns>入力された座標</returns>
		public static Vector3 GetTouchPosition() {
			// エディター確認
			if (!CheckTouch()) {
				TouchInfo touch = TouchUtil.GetTouch ();
				if (touch != TouchInfo.None) {
					return Input.mousePosition;
				}
			} else {
				if (Input.touchCount > 0) {
					Touch touch = Input.GetTouch (0);
					TouchPosition.x = touch.position.x;
					TouchPosition.y = touch.position.y;
					return TouchPosition;
				}
			}

			// 入力なし
			return Vector3.zero;
		}

		/// <summary>
		/// 入力のワールド座標
		/// </summary>
		/// <returns>ワールド座標</returns>
		/// <param name="camera">基準のカメラ</param>
		public static Vector3 GetTouchWorldPosition( Camera camera ) {
			return camera.ScreenToWorldPoint (GetTouchPosition ());
		}

		#endregion method
	}

	#region enum

	/// <summary>
	/// 入力情報の拡張
	/// </summary>
	public enum TouchInfo
	{
		// タッチなし
		None = 99,

		// タッチ開始
		Began = 0,

		// タッチ移動
		Moved = 1,

		// タッチ静止
		Stationary = 2,

		// タッチ終了
		Ended = 3,

		// タッチキャンセル
		Canceled = 4,
	}

	#endregion enum
}
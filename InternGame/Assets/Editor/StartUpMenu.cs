using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

namespace Asada
{
	/// <summary>
	/// スタートアップメニュー用クラス
	/// </summary>
	public class StartUpMenu : EditorWindow {
		//メニューに項目追加
		[MenuItem("Window/StartUpMenu")]

		/// <summary>
		/// ウインドウ表示
		/// </summary>
		static void Open()
		{
			//ウインドウ表示
			EditorWindow.GetWindow<StartUpMenu>("StartUpMenu");
		}


		/// <summary>
		/// ウインドウにUI表示
		/// </summary>
		void OnGUI()
		{
			GUILayout.Label ("StartScene");
			if (GUILayout.Button ("Title")) {
				EditorSceneManager.OpenScene("Assets/Scenes/Title.unity");
			}

			GUILayout.Label ("GamePlay");
			if (GUILayout.Button ("Play")) {
				EditorSceneManager.OpenScene("Assets/Scenes/Title.unity");
				EditorApplication.isPlaying = true;
			}

			GUILayout.Label ("SceneMenu");

			if (GUILayout.Button ("Title")) {
				EditorSceneManager.OpenScene("Assets/Scenes/Title.unity");
			}

			if (GUILayout.Button ("StageSelect")) {
				EditorSceneManager.OpenScene("Assets/Scenes/StageSelect.unity");
			}

			if (GUILayout.Button ("Game")) {
				EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
			}

			if (GUILayout.Button ("Credit")) {
				EditorSceneManager.OpenScene("Assets/Scenes/Credit.unity");
			}

		}

	}
}
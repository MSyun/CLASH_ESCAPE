using UnityEngine;
using System.Collections;
using UnityEditor;

public class AutoBuild {



	[UnityEditor.MenuItem("Build/Auto Build")]
	static void BuildProjectAllScene() {
		Building( BuildTarget.Android, BuildOptions.None, "hoge.apk");
	}

	/// <summary>
	/// アクティブ中のすべてのシーンを取得
	/// </summary>
	/// <returns>アクティブ中のシーンのパス配列</returns>
	static string[] GetActiveAllScenePaths() {
		ArrayList Scenes = new ArrayList ();
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled)
				continue;

			Scenes.Add (scene.path);
		}
		return (string[])Scenes.ToArray (typeof(string));
	}

	/// <summary>
	/// ビルド開始
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="option">Option.</param>
	/// <param name="buildPath">Build path.</param>
	static void Building( BuildTarget target, BuildOptions option, string buildPath ) {
		string targetName = System.Enum.GetName (target.GetType (), target);
		Debug.Log ("Begin Build Player : " + targetName);
		// プラットフォームの確認
		if (EditorUserBuildSettings.activeBuildTarget != target) {
			EditorUserBuildSettings.SwitchActiveBuildTarget (target);
		}

		string message = BuildPipeline.BuildPlayer (GetActiveAllScenePaths(), buildPath, target, option);
		Debug.Log ("End Build Player : " + targetName);
		if (message != "") {
			Debug.LogWarning (message);
		}
	}
}

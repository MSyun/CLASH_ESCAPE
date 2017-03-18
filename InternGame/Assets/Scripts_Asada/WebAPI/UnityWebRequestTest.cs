using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Asada
{
	/// <summary>
	/// WebAPIテストクラス
	/// </summary>
	public class UnityWebRequestTest : MonoBehaviour {

		//サーバにリクエストするデータ
		string myName = "asada";

		//通信先url
		string url = "http://localhost/phptest/Test.php";	

		/// <summary>
		/// スタート関数
		/// </summary>
		void Start () {
			//StartCoroutine (GetText ());
			StartCoroutine (Upload ());		//送信

			StartCoroutine (GetText ());	//受信？

		}


		/// <summary>
		/// アップロード関数
		/// </summary>
		IEnumerator Upload()
		{
			//送信データ作成
			WWWForm form = new WWWForm ();

			//送信データ設定
			form.AddField ("myName", myName);

			//送信先を設定
			UnityWebRequest www = UnityWebRequest.Post (url, form);

			//リクエスト送信
			yield return www.Send ();


			//エラーチェック
			if (www.isError) {
				Debug.Log (www.error);
			} else {
				Debug.Log ("Form upload complete!");

				//レスポンスを表示
				if (www.isDone) {
					Debug.Log ("HttpPost OK:" +www.downloadHandler.text);
				}
			}
		}


		/// <summary>
		/// 受信関数
		/// </summary>
		/// <returns>The text.</returns>
		IEnumerator GetText()
		{
			//受信先設定
			UnityWebRequest request = UnityWebRequest.Get (url);

			//リクエスト送信
			yield return request.Send();


			//通信エラーチェック
			if (request.isError) {
				Debug.Log (request.error);	//エラー表示
			} else {
				Debug.Log (request.downloadHandler.text);	//受信結果を表示する

				//レスポンスを表示
				//if (request.isDone) {
				//	Debug.Log ("HttpGet OK:" +request.downloadHandler.text);
				//}
			}
		}


		// Update is called once per frame
		void Update () {
		
		}
}
}
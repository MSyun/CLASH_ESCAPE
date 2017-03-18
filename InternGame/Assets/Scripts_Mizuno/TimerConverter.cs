namespace Mizuno {

	public class TimerConverter {

		/// <summary>
		/// 時間変換機（float → string
		/// </summary>
		/// <param name="time">時間</param>
		static public string Convert( float time ) {
			float CurrentTime = time;

			//----- 各時間の算出
			// ミリ
			string Miri = "" + ((int)(CurrentTime * 100) % 100);
			// 秒
			string Sec = "" + (int)(CurrentTime % 60);
			// 分
			string Min = "" + (int)(CurrentTime / 60);

			// 00:00:00の形へ変更
			return Min.PadLeft(2, '0') + ":" + Sec.PadLeft(2, '0') + ":" + Miri.PadLeft(2, '0');
		}
	}

}
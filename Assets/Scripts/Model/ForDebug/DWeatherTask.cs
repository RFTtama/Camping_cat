using System;

namespace Assets.Scripts.Model.ForDebug
{
	internal class DWeatherTask : IWeatherTask
	{
		public static int WeatherIdForDebug { get; set; } = 800;
		public static DateTimeOffset SunsetTimeForDebug { get; set; } = DateTimeOffset.Now.AddHours(6);

		private static Lazy<DWeatherTask> _lazy = new Lazy<DWeatherTask>(() => new DWeatherTask(), isThreadSafe: true);
		public static DWeatherTask Instance => _lazy.Value;

		public void SetInitialData(string apiKey, string cityName)
		{
			// デバッグ用モック実装
		}

		public int GetWeatherId()
		{
			// デバッグ用モック実装
			return 800; // 晴天を示すID
		}

		public DateTimeOffset GetSunsetTime()
		{
			// デバッグ用モック実装
			return DateTimeOffset.Now.AddHours(6); // 現在時刻から6時間後を夕焼け時刻とする
		}

		public void Dispose()
		{
			// デバッグ用モック実装
		}
	}
}

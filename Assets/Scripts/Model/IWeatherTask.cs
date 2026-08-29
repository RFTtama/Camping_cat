using System;

namespace Assets.Scripts.Model
{
	internal interface IWeatherTask : IDisposable
	{
		public void SetInitialData(string apiKey, string cityName);

		public int GetWeatherId();

		public DateTimeOffset GetSunsetTime();
	}
}

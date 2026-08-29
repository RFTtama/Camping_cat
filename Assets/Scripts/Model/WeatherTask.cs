using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
	public class WeatherTask : IWeatherTask
	{
		private static Lazy<WeatherTask> _lazy = new Lazy<WeatherTask>(() => new WeatherTask(), isThreadSafe: true);
		public static WeatherTask Instance => _lazy.Value;

		private System.Threading.Timer _timer;

		//定数
		private const int Interval = 10 * 60 * 1000;
		//private const int Interval = 1000;
		private const string ApiUrl = "http://api.openweathermap.org/geo/1.0/direct?q={city name}&appid={API key}";
		private const string WeatherApiUrl = "https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API key}";

		//シリアライズ用
		[Serializable]
		private class LocationData
		{
			public double lat;
			public double lon;
		}

		[Serializable]
		private class WeatherData
		{
			public int id;
			public string main;
			public string description;
			public string icon;
		}

		[Serializable]
		private class WeatherResponse
		{
			public WeatherData[] weather;
		}

		[Serializable]
		private class SysData
		{
			public string country;
			public long sunrise;
			public long sunset;
		}

		[Serializable]
		private class SysResponse
		{
			public SysData sys;
		}

		//ローカル変数
		private string _apiKey;
		private string _city;

		private LocationData _location;
		private int _weatherId;
		private DateTimeOffset _sunsetTime;

		//ゲッター

		/// <summary>
		/// WeatherIdを取得する
		/// </summary>
		/// <returns></returns>
		public int GetWeatherId()
		{
			return _weatherId;
		}

		/// <summary>
		/// SunsetTimeを取得する
		/// </summary>
		/// <returns></returns>
		public DateTimeOffset GetSunsetTime()
		{
			return _sunsetTime;
		}


		private WeatherTask()
		{
			_apiKey = string.Empty;
			_city = string.Empty;
			_weatherId = 800;
			_sunsetTime = SystemInfo.Instance.GetSystemDate().Date.AddHours(19.0);
			_location = new LocationData();
		}


		public void SetInitialData(string apiKey, string cityName)
		{
			_apiKey = apiKey;
			_city = cityName;

			// すでにタイマーが存在する場合は破棄
			if (null != _timer)
			{
				_timer.Dispose();
			}

			// 10分毎に実行
			_timer = new Timer(TimerFunc, null, 0, Interval);
			UnityEngine.Debug.Log("Set Initial Data");
		}

		public void Dispose()
		{
			_apiKey = string.Empty;
			_city = string.Empty;
			_timer?.Dispose();
		}

		/// <summary>
		/// 定期実行される関数
		/// </summary>
		/// <param name="state"></param>
		private async void TimerFunc(object state)
		{
			UnityEngine.Debug.Log("Weather Timer Ticked");
			// 緯度経度が未取得の場合、APIを叩く
			if (0.0 == _location.lat || 0.0 == _location.lon)
			{
				await GetLocation();
			}

			await GetWeather();
		}

		/// <summary>
		/// 都市の緯度経度を取得する
		/// </summary>
		/// <returns></returns>
		private async Task GetLocation()
		{
			using (var client = new HttpClient())
			{
				string req = ApiUrl.Replace("{city name}", _city).Replace("{API key}", _apiKey);
				UnityEngine.Debug.Log($"Request URL: {req}");
				var result = await client.GetAsync(req);
				if (result.IsSuccessStatusCode)
				{
					string json = await result.Content.ReadAsStringAsync();
					json = json.TrimStart('[').TrimEnd(']');

					try
					{
						_location = JsonUtility.FromJson<LocationData>(json);
					}
					catch (Exception ex)
					{
						UnityEngine.Debug.LogError(ex.Message);
					}
					UnityEngine.Debug.Log($"lat: {_location.lat}, lon: {_location.lon}");
				}
				else
				{
					UnityEngine.Debug.LogError($"Failed to get location data: {result.StatusCode}");
				}
			}
		}

		/// <summary>
		/// 現在の天気を取得する
		/// </summary>
		/// <returns></returns>
		private async Task GetWeather()
		{
			using (var client = new HttpClient())
			{
				string req = WeatherApiUrl.Replace("{API key}", _apiKey).Replace("{lat}", _location.lat.ToString()).Replace("{lon}", _location.lon.ToString());
				UnityEngine.Debug.Log($"Request URL: {req}");
				var result = await client.GetAsync(req);
				if (result.IsSuccessStatusCode)
				{
					string json = await result.Content.ReadAsStringAsync();

					WeatherResponse response = JsonUtility.FromJson<WeatherResponse>(json);
					_weatherId = response.weather[0].id;
					SysResponse sysres = JsonUtility.FromJson<SysResponse>(json);
					_sunsetTime = DateTimeOffset.FromUnixTimeSeconds(sysres.sys.sunset).ToLocalTime();

					UnityEngine.Debug.Log($"Weather: {_weatherId}, Sunset Time: {_sunsetTime}");
				}
				else
				{
					UnityEngine.Debug.LogError($"Failed to get weather data: {result.StatusCode}");
				}
			}
		}
	}
}
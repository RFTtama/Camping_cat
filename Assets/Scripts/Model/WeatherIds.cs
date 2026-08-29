namespace Assets.Scripts.Model
{
	public enum WeatherIds
	{
		Thunderstorm = 200,
		Drizzle = 300,
		Rain = 500,
		Snow = 600,
		Mist = 700,
		Clear = 800,
		Clouds = 801,
	}

	//天候条件
	public enum WeatherCondition : ulong
	{
		All = ~(ulong)0x00, //全て
		Thunderstorm = 1,   //雷雨
		Drizzle = 1 << 1,   //霧雨
		Rain = 1 << 2,      //雨
		Snow = 1 << 3,      //雪
		Mist = 1 << 4,      //霧
		Clear = 1 << 5,     //晴天
		Clouds = 1 << 6,    //曇り
	}

	public class WeatherIdsUtil
	{
		private int _apiId;
		private WeatherIds _weatherId;
		private WeatherCondition _weatherCondition;

		public WeatherIds WeatherId => _weatherId;
		public WeatherCondition WeatherCondition => _weatherCondition;

		public WeatherIdsUtil(int apiId)
		{
			_apiId = apiId;
			CalcWeatherId();
			CalcWeatherCondition();
		}

		private void CalcWeatherId()
		{
			if (801 <= _apiId)
			{
				_weatherId = WeatherIds.Clouds;
			}
			else if (800 == _apiId)
			{
				_weatherId = WeatherIds.Clear;
			}
			else if (700 <= _apiId)
			{
				_weatherId = WeatherIds.Mist;
			}
			else if (600 <= _apiId)
			{
				_weatherId = WeatherIds.Snow;
			}
			else if (500 <= _apiId)
			{
				_weatherId = WeatherIds.Rain;
			}
			else if (300 <= _apiId)
			{
				_weatherId = WeatherIds.Drizzle;
			}
			else if (200 <= _apiId)
			{
				_weatherId = WeatherIds.Thunderstorm;
			}
			else
			{
				UnityEngine.Debug.LogError("Unknown weather API ID: " + _apiId);
			}
		}

		private void CalcWeatherCondition()
		{
			switch (_weatherId)
			{
				case WeatherIds.Thunderstorm:
					_weatherCondition = WeatherCondition.Thunderstorm;
					break;
				case WeatherIds.Drizzle:
					_weatherCondition = WeatherCondition.Drizzle;
					break;
				case WeatherIds.Rain:
					_weatherCondition = WeatherCondition.Rain;
					break;
				case WeatherIds.Snow:
					_weatherCondition = WeatherCondition.Snow;
					break;
				case WeatherIds.Mist:
					_weatherCondition = WeatherCondition.Mist;
					break;
				case WeatherIds.Clear:
					_weatherCondition = WeatherCondition.Clear;
					break;
				case WeatherIds.Clouds:
					_weatherCondition = WeatherCondition.Clouds;
					break;
				default:
					UnityEngine.Debug.LogError("Unknown weather ID: " + _weatherId);
					break;
			}
		}
	}
}

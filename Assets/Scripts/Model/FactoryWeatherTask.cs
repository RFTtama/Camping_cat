using Assets.Scripts.Model.ForDebug;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Model
{
	internal static class FactoryWeatherTask
	{
		public static IWeatherTask Create()
		{
			if (DebugSettings.WEATHER_TASK_STUB)
			{
				return DWeatherTask.Instance;
			}
            return WeatherTask.Instance;

        }
    }
}

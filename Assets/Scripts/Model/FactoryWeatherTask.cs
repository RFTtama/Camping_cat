using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Model
{
    internal static class FactoryWeatherTask
    {
        public static IWeatherTask Create()
        {
#if false
            return DWeatherTask.Instance;
#else
            return WeatherTask.Instance;
#endif
        }
    }
}

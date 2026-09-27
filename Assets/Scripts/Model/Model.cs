using System;
using Assets.Scripts.BehaviorId;

namespace Assets.Scripts.Model
{
	public class Model : IDisposable, IModel
	{
		private static Lazy<Model> _lazy = new Lazy<Model>(() => new Model(), isThreadSafe: true);
		public static Model Instance => _lazy.Value;

		private BehaviorTask _behaviorTask;

		private Model()
		{
			_behaviorTask = BehaviorTask.Instance;
		}

		/// <summary>
		/// 今のキャラの行動を取得する
		/// </summary>
		/// <returns>行動名</returns>
		public string GetNowBehavior()
		{
			return _behaviorTask.GetNowBehavior();
		}

		/// <summary>
		/// 今のキャラの行動IDを取得する
		/// </summary>
		/// <returns>行動ID</returns>
		public BehaviorId.BehaviorId GetNowBehaviorId()
		{
			return _behaviorTask.GetNowBehaviorId();
		}

        /// <summary>
        /// 日の出の時間を取得する
        /// </summary>
        /// <returns>日の出時刻</returns>
        public DateTime GetSunRiseTime()
		{
			return _behaviorTask.GetSunRiseTime();
		}

        /// <summary>
        /// 日の入の時間を取得する
        /// </summary>
        /// <returns>日の入時刻</returns>
        public DateTime GetSunSetTime()
		{
			return _behaviorTask.GetSunSetTime();
		}

		/// <summary>
		/// 天気を取得する
		/// </summary>
		/// <returns>天気名(英語文字列)</returns>
		public string GetNowWeather()
		{
			return _behaviorTask.GetNowWeather();
		}

		public void Dispose()
		{
			_behaviorTask?.Dispose();
		}
	}
}
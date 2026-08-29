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

		public void Dispose()
		{
			_behaviorTask?.Dispose();
		}
	}
}
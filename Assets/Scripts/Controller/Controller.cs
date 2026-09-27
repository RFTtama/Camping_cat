using UnityEngine;
using System;
using Assets.Scripts.View;
using Assets.Scripts.Model;
using System.ComponentModel;

namespace Assets.Scripts.Controller
{
	public class Controller : IDisposable, IController
	{
		private static Lazy<Controller> _lazy = new Lazy<Controller>(() => new Controller(), isThreadSafe: true);
		public static Controller Instance => _lazy.Value;

		System.Threading.Timer _timer;

#nullable enable
		private IView? _view;
		private IModel? _model;
		private bool _initialized;

		private const int TIMER_INTERVAL = 1000;

		private Controller()
		{
		}

		public void Dispose()
		{
			_timer.Dispose();

			_view = null;
			_model = null;
		}

		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="view">viewのインスタンス</param>
		/// <param name="model">modelのインスタンス</param>
		public void Initial(IView view, IModel model)
		{
			// 各インスタンス設定
			_view = view;
			_model = model;

			if (null == _view || null == _model)
			{
				UnityEngine.Debug.Log("View or Model are null");
				return;
			}

			// タイマ始動
			_timer = new System.Threading.Timer(TimerFunc, null, TIMER_INTERVAL, TIMER_INTERVAL);
		}

		/// <summary>
		/// 周期処理
		/// </summary>
		/// <param name="state"></param>
		private void TimerFunc(object state)
		{
			if (null == _model) return;
			if (null == _view) return;

			try
			{
				ViewUpdateData newData = new();

				// viewに渡すデータをmodelから取得
				newData.BehaviorId = _model.GetNowBehaviorId();
				newData.BehaviorName = _model.GetNowBehavior();
				newData.SunRiseTime = _model.GetSunRiseTime();
				newData.SunSetTime = _model.GetSunSetTime();
				newData.NowWeather = _model.GetNowWeather();

				_view?.Update(newData);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log(ex.Message);
			}
		}
	}
}
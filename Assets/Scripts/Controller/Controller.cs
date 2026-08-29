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
			_initialized = false;
		}

		public void Dispose()
		{
			_timer.Dispose();

			_view = null;
			_model = null;

			_initialized = false;

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

			// タイマ始動
			_timer = new System.Threading.Timer(TimerFunc, null, TIMER_INTERVAL, TIMER_INTERVAL);

			// 初期化OK
			_initialized = true;
		}

		/// <summary>
		/// 周期処理
		/// </summary>
		/// <param name="state"></param>
		private void TimerFunc(object state)
		{
			if (null == _model) return;
			if (false == _initialized) return;

			ViewUpdateData newData = new();

			// viewに渡すデータをmodelから取得
			newData.BehaviorId = _model.GetNowBehaviorId();
			newData.BehaviorName = _model.GetNowBehavior();

			_view?.Update(newData);
		}
	}
}
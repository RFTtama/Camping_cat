using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Scripts.BehaviorId;

namespace Assets.Scripts.Model
{
	public class BehaviorTask : IDisposable
	{
		private static Lazy<BehaviorTask> _lazy = new Lazy<BehaviorTask>(() => new BehaviorTask(), isThreadSafe: true);
		public static BehaviorTask Instance => _lazy.Value;
		private IWeatherTask _weatherTask;
		private System.Threading.Timer _timer;

		//変数
		private int _currentWeather = -1;
		private DateTime _finalCalcTime = new DateTime();

		//定数
		private const string CONFIG_FILE = "config.json";
		private const int INTERVAL = 5000;

		//デバッグ用：現在日時取得プロパティ
#if false
		private DateTime DateTimeNow => SystemInfo.Instance.GetSystemDate().Date;
#else
		private DateTime DateTimeNow => SystemInfo.Instance.GetSystemDate();
#endif

		//シリアライズ用設定クラス
		[Serializable]
		private class Config
		{
			public string apiKey;
			public string city;
		}

		private BehaviorTask()
		{
			try
			{
				//設定ファイル読み込み
				string json = string.Empty;
				using (StreamReader sr = new StreamReader(CONFIG_FILE))
				{
					json = sr.ReadToEnd();
				}
				Config cfg = JsonUtility.FromJson<Config>(json);
				_weatherTask = WeatherTask.Instance;
				//                _weatherTask = ForDebug.DWeatherTask.Instance;
				_weatherTask.SetInitialData(cfg.apiKey, cfg.city);

				//今日の行動リスト初期化
				_todayBehavior = new BehaviorId.BehaviorId[144];
				Array.Fill(_todayBehavior, BehaviorId.BehaviorId.Idle);

				//行動決定関数群初期化
				BehaviorDecideFuncs = new Action[]
				{
				SleepLayer,
				MealLayer,
				FuelFireLayer,
				RandomBehaviorLayer,
				};

				_timer = new System.Threading.Timer(TimerFunc, null, INTERVAL, INTERVAL);
			}
			catch (FileNotFoundException)
			{
				//設定ファイルが無い場合はデフォルトで作成
				using (StreamWriter sw = new StreamWriter(CONFIG_FILE, false))
				{
					sw.Write(JsonUtility.ToJson(new Config()));
				}
				UnityEngine.Debug.LogError($"Config file not found");
			}
		}

		//行動決定処理系

		//行動構造体
		private struct Bhavior
		{
			public BehaviorId.BehaviorId Id;
			public int Weight;
			public ulong Weather;
			public int MinTime;
			public int MaxTime;
		}

		//ランダム行動リスト
		private readonly Bhavior[] Behaviors =
		{
		new Bhavior{ Id = BehaviorId.BehaviorId.FetchFirewood,     Weight = 5,
			Weather = (ulong)(WeatherCondition.Mist | WeatherCondition.Clear | WeatherCondition.Clouds),
			MinTime = 6, MaxTime = 12 },

		new Bhavior{ Id = BehaviorId.BehaviorId.Stroll,            Weight = 15,
			Weather = (ulong)(WeatherCondition.Mist | WeatherCondition.Clear | WeatherCondition.Clouds),
			MinTime = 3, MaxTime = 18 },

		new Bhavior{ Id = BehaviorId.BehaviorId.StrollInRain,      Weight = 15,
			Weather = (ulong)(WeatherCondition.Drizzle | WeatherCondition.Rain | WeatherCondition.Snow),
			MinTime = 3, MaxTime = 18 },

		new Bhavior{ Id = BehaviorId.BehaviorId.Sunbathing,        Weight = 10,
			Weather = (ulong)(WeatherCondition.Clear),
			MinTime = 3, MaxTime = 6 },

		new Bhavior{ Id = BehaviorId.BehaviorId.ReadBook,          Weight = 10,
			Weather = (ulong)(WeatherCondition.Clear | WeatherCondition.Clouds),
			MinTime = 1, MaxTime = 18 },

		new Bhavior{ Id = BehaviorId.BehaviorId.Grooming,         Weight = 5,
			Weather = (ulong)(WeatherCondition.All),
			MinTime = 1, MaxTime = 1 },

		new Bhavior{ Id = BehaviorId.BehaviorId.WatchingOutside,   Weight = 5,
			Weather = (ulong)(WeatherCondition.Drizzle | WeatherCondition.Rain | WeatherCondition.Snow),
			MinTime = 1, MaxTime = 1 },
	};

		//今日の行動リスト
		private readonly BehaviorId.BehaviorId[] _todayBehavior;

		//行動決定関数
		private readonly Action[] BehaviorDecideFuncs;

		//各行動レイヤー関数群

		/// <summary>
		/// 睡眠レイヤー
		/// </summary>
		private void SleepLayer()
		{
			//朝の睡眠時間を設定
			for (int i = 0; i < 42; i++)
			{
				if (BehaviorId.BehaviorId.Idle == _todayBehavior[i])
				{
					_todayBehavior[i] = BehaviorId.BehaviorId.Sleep;
				}
			}

			//夜の睡眠時間を設定
			for (int i = 138; i < _todayBehavior.Length; i++)
			{
				if (BehaviorId.BehaviorId.Idle == _todayBehavior[i])
				{
					_todayBehavior[i] = BehaviorId.BehaviorId.Sleep;
				}
			}
		}

		/// <summary>
		/// 食事レイヤー
		/// </summary>
		private void MealLayer()
		{
			//朝食時間を設定
			for (int i = 45; i < 48; i++)
			{
				if (BehaviorId.BehaviorId.Idle == _todayBehavior[i])
				{
					_todayBehavior[i] = BehaviorId.BehaviorId.Breakfast;
				}
			}

			//昼食時間を設定
			for (int i = 72; i < 75; i++)
			{
				if (BehaviorId.BehaviorId.Idle == _todayBehavior[i])
				{
					_todayBehavior[i] = BehaviorId.BehaviorId.Launch;
				}
			}

			//夕食時間を設定
			for (int i = 114; i < 117; i++)
			{
				if (BehaviorId.BehaviorId.Idle == _todayBehavior[i])
				{
					_todayBehavior[i] = BehaviorId.BehaviorId.Dinner;
				}
			}
		}

		/// <summary>
		/// 薪くべレイヤー
		/// </summary>
		private void FuelFireLayer()
		{
			//日の入り時間を薪くべ時間に設定
			DateTime sunset = _weatherTask.GetSunsetTime().DateTime;
			TimeSpan ts = sunset - sunset.Date;
			int sunsetIndex = (int)(ts.TotalMinutes / 10);

			//算出した日の入り時間が睡眠時間と被らないように調整
			while (_todayBehavior[sunsetIndex] != BehaviorId.BehaviorId.Idle && sunsetIndex > 0)
			{
				sunsetIndex--;
			}

			_todayBehavior[sunsetIndex] = BehaviorId.BehaviorId.FuelFire;
		}

		/// <summary>
		/// ランダム行動レイヤー
		/// </summary>
		private void RandomBehaviorLayer()
		{
			int ind = (int)((DateTimeNow - DateTimeNow.Date).TotalMinutes / 10);
			BehaviorId.BehaviorId befBehavior = BehaviorId.BehaviorId.Idle;

			while (true)
			{
				//次のアイドル時間帯を探す
				for (; ind < _todayBehavior.Length; ind++)
				{
					if (BehaviorId.BehaviorId.Idle == _todayBehavior[ind])
					{
						break;
					}
					befBehavior = _todayBehavior[ind];
				}

				//アイドル時間を数える
				int toTime = 0;
				for (int i = ind; i < _todayBehavior.Length; i++)
				{
					toTime++;
					if (BehaviorId.BehaviorId.Idle != _todayBehavior[i])
					{
						break;
					}
				}

				//アイドル時間が無ければ終了
				if (0 == toTime)
				{
					break;
				}

				List<Bhavior> selectedBehaviors = new List<Bhavior>();
				int totalWeight = 0;

				WeatherIdsUtil util = new WeatherIdsUtil(_currentWeather);
				foreach (Bhavior bhavior in Behaviors)
				{
					//対象の天候かつ、行動可能時間内かつ、直前の行動と異なる場合は選択肢に追加
					if ((0 != (bhavior.Weather & (ulong)util.WeatherCondition)) && (bhavior.MinTime <= toTime) && (bhavior.Id != befBehavior))
					{
						selectedBehaviors.Add(bhavior);
						totalWeight += bhavior.Weight;
					}
				}

				//選択肢が無ければ次のアイドル時間帯へ
				if (selectedBehaviors.Count == 0)
				{
					ind += toTime;
					continue;
				}

				System.Random rand = new System.Random();
				int selected = rand.Next() % totalWeight;

				totalWeight = 0;
				Bhavior finalBehavior = selectedBehaviors[0];

				//重み付け抽選
				foreach (Bhavior bhavior in selectedBehaviors)
				{
					totalWeight += bhavior.Weight;
					if (selected <= totalWeight)
					{
						finalBehavior = bhavior;
						break;
					}
				}

				//行動時間決定＆設定
				if (finalBehavior.MinTime == finalBehavior.MaxTime)
				{
					for (int i = ind; i < ind + finalBehavior.MaxTime; i++)
					{
						_todayBehavior[i] = finalBehavior.Id;
					}
				}
				else if (toTime <= finalBehavior.MaxTime)
				{
					for (int i = ind; i < (ind + toTime) - 1; i++)
					{
						_todayBehavior[i] = finalBehavior.Id;
					}
				}
				else
				{
					selected = rand.Next() % (finalBehavior.MaxTime - finalBehavior.MinTime);
					for (int i = ind; i < ind + selected; i++)
					{
						_todayBehavior[i] = finalBehavior.Id;
					}
				}
			}
		}

		/// <summary>
		/// 周期的に実行する関数
		/// </summary>
		private void TimerFunc(object state)
		{
			//天候が変わっていなければ何もしない
			if (_weatherTask.GetWeatherId() == _currentWeather) return;
			//日付が変わっていなければ何もしない
			if (_finalCalcTime.Day == DateTimeNow.Day) return;

			_currentWeather = _weatherTask.GetWeatherId();

			ClearWithoutCompatibleBehavior();

			try
			{
				//各行動レイヤー関数を実行
				UnityEngine.Debug.Log("Behavior Recalculation");
				foreach (Action func in BehaviorDecideFuncs)
				{
					func();
				}
				_finalCalcTime = DateTimeNow;
				string tl = string.Empty;
				DateTime dt = SystemInfo.Instance.GetSystemDate().Date;
				for (int i = 0; i < _todayBehavior.Length; i++)
				{
					tl += $"{dt.AddMinutes(i * 10):HH:mm}, ";
				}

				string wr = string.Empty;
				foreach (BehaviorId.BehaviorId id in _todayBehavior)
				{
					wr += id.ToString() + ",";
				}
				UnityEngine.Debug.Log(tl + "\n");
				UnityEngine.Debug.Log(wr);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError($"Behavior Recalculation Error: {ex.Message}");
			}
		}

		/// <summary>
		/// 現在の行動を取得する
		/// </summary>
		/// <returns></returns>
		public string GetNowBehavior()
		{
			return _todayBehavior[GetNowBehaviorInd()].ToString();
		}

		/// <summary>
		/// 現在の行動IDを取得する
		/// </summary>
		/// <returns></returns>
		public BehaviorId.BehaviorId GetNowBehaviorId()
		{
			return _todayBehavior[GetNowBehaviorInd()];
		}

		/// <summary>
		/// 現在の行動インデックスを取得する
		/// </summary>
		/// <returns></returns>
		private int GetNowBehaviorInd()
		{
			return (int)((DateTimeNow - DateTimeNow.Date).TotalMinutes / 10);
		}

		/// <summary>
		/// 対応していない行動をクリアする
		/// </summary>
		private void ClearWithoutCompatibleBehavior()
		{
			int ind = GetNowBehaviorInd();
			BehaviorId.BehaviorId nowBehv = _todayBehavior[ind];

			WeatherIdsUtil util = new WeatherIdsUtil(_currentWeather);

			bool isCompatible = false;

			foreach (Bhavior bh in Behaviors)
			{
				if (bh.Id == nowBehv)
				{
					if (0 < (bh.Weather & (ulong)util.WeatherCondition))
					{
						isCompatible = true;
					}
					break;
				}
			}

			for (int i = 0; i < _todayBehavior.Length; i++)
			{
				int selectedInd = (i + ind) % _todayBehavior.Length;
				if ((nowBehv != _todayBehavior[selectedInd]) && isCompatible)
				{
					isCompatible = false;
				}

				if (!isCompatible)
				{
					_todayBehavior[selectedInd] = BehaviorId.BehaviorId.Idle;
				}
			}
		}

		public void Dispose()
		{
			_timer?.Dispose();
			_weatherTask?.Dispose();
		}
	}
}
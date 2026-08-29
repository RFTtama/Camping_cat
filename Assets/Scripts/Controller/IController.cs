using UnityEngine;
using System;
using System.ComponentModel;
using Assets.Scripts.View;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller
{
	public interface IController : IDisposable
	{
		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="view">viewのインスタンス</param>
		/// <param name="model">modelのインスタンス</param>
		public void Initial(IView view, IModel model);
	}
}

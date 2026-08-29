using Assets.Scripts.Model;
using Assets.Scripts.View;
using Assets.Scripts.Controller;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class Program : MonoBehaviour
{
	IModel model;
	IView view;
	IController controller;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		// インスタンス作成
		controller = Controller.Instance;
		view = View.Instance;
		model = Model.Instance;

		// コントローラの初期化
		controller.Initial(view, model);
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnDestroy()
	{
		model?.Dispose();
		view?.Dispose();
		controller.Dispose();
	}
}

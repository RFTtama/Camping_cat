using Assets;
using Assets.Scripts;
using Assets.Scripts.View.StatusTable;
using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	IStatusTables st;
	private SpriteRenderer spriteRenderer;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		st = FactoryStatusTable.GetInstance();
	}

	// Update is called once per frame
	void Update()
	{
		DateTime nowTime = st.SystemTbl.GetUpDateTime();
		Color cl = Color.skyBlue;

		if (DateTime.Now.Date.AddHours(21) <= nowTime)// 9時以降
		{
			cl = Color.darkBlue;
		}
		else if (st.SystemTbl.GetSunSetTime() <= nowTime)// 日没後
		{
			cl = Color.blue;
		}
		else if (st.SystemTbl.GetSunRiseTime() <= nowTime)// 昼
		{
			cl = Color.skyBlue;
		}
		else// 日の出前
		{
			cl = Color.darkBlue;
		}

		spriteRenderer.color = cl;
	}
}

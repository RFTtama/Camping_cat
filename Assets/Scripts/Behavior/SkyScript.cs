using Assets;
using Assets.Scripts;
using Assets.Scripts.View.StatusTable;
using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	IStatusTables st;
	private SpriteRenderer spriteRenderer;
	private DateTime oldTime;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		st = FactoryStatusTable.GetInstance();
		oldTime = new DateTime();
		SetSpriteSize();
	}

	// Update is called once per frame
	void Update()
	{
		DateTime nowTime = st.SystemTbl.GetUpDateTime();
		if (nowTime == oldTime) return;

		Color cl = Color.white;

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
		oldTime = nowTime;
		//UnityEngine.Debug.Log(cl.ToString());
	}

	private void SetSpriteSize()
	{
		float height = Camera.main.orthographicSize * 2.0f;
		float width = height * Camera.main.aspect;

		/*float scaleX = width / spriteRenderer.bounds.size.x;
		float scaleY = height / spriteRenderer.bounds.size.y;

		float scale = Mathf.Max(scaleX, scaleY);*/
		float scale = Mathf.Max(height, width);

		transform.localScale = new Vector3(scale, scale, 1.0f);
		UnityEngine.Debug.Log(transform.localScale.ToString());

	}
}

using Assets.Scripts.View.StatusTable;
using System;
using UnityEngine;

public class SunScript : MonoBehaviour
{
	IStatusTables st;
	private SpriteRenderer spriteRenderer;
	private DateTime oldTime;
	private float screenSize;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		st = FactoryStatusTable.GetInstance();
		oldTime = new DateTime();
		CalcScreenSize();
	}

	// Update is called once per frame
	void Update()
	{
		DateTime nowTime = st.SystemTbl.GetUpDateTime();
		if (nowTime == oldTime) return;

		DateTime sunrise = st.SystemTbl.GetSunRiseTime();
		DateTime sunset = st.SystemTbl.GetSunSetTime();

		//太陽の出てない時間なら非表示にする
		if ((nowTime < sunrise)
			|| (sunset < nowTime))
		{
			spriteRenderer.enabled = false;
			return;
		}

		spriteRenderer.enabled = true;

		TimeSpan sunshineTime = sunset - sunrise;	//太陽の出現時間
		TimeSpan nowsunshine = nowTime - sunrise;   //現在の太陽の位置

		float magn = (float)(nowsunshine.TotalSeconds / sunshineTime.TotalSeconds);
		float pos;

		//位置設定
		if (0.5 == magn)// 太陽が中心
		{
			pos = 0.0f;
		}
		else if (0.5 < magn)// 太陽が右側
		{
			pos = screenSize * ((magn - 0.5f) * 2.0f);
		}
		else// 太陽が左側
		{
			pos = (screenSize * -1.0f) * (1.0f - (magn * 2.0f));
		}

		transform.position = new Vector3(
			pos,
			transform.position.y,
			transform.position.z
		);

		oldTime = nowTime;
	}
	
	/// <summary>
	/// 画面端までの位置を計算する
	/// </summary>
	private void CalcScreenSize()
	{
		Camera cam = Camera.main;

		float height = cam.orthographicSize * 2f;
		float width = height * cam.aspect;

		float right = cam.transform.position.x + width / 2f;

		float spriteHalfWidth = spriteRenderer.bounds.size.x / 2f;
		screenSize = right - spriteHalfWidth;
	}
}

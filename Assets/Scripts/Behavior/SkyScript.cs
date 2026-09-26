using Assets.Scripts.View.StatusTable;
using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	IStatusTables st;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		st = FactoryStatusTable.GetInstance();
	}

	// Update is called once per frame
	void Update()
	{

	}
}

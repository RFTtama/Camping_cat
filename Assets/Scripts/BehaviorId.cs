using UnityEngine;

namespace Assets.Scripts.BehaviorId
{
	//行動ID
	public enum BehaviorId
	{
		//固定行動
		Idle,               //アイドル
		Sleep,              //睡眠
		Breakfast,          //朝食
		Launch,             //昼食
		Dinner,             //夕食
		FuelFire,           //焚火に火をくべる

		//ランダム行動
		FetchFirewood,      //薪を拾う
		Stroll,             //散歩
		StrollInRain,       //雨中散歩
		Sunbathing,         //日光浴
		ReadBook,           //読書
		Grooming,          //グルーミング
		WatchingOutside,    //外を眺める
	}
}

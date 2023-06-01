using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 클래스 설명 : 아이템의 시초클래스

// 아이템 대분류 
/// 0~1000      재료
/// 1001~2000   메인 무기 : 근접(1001~1500), 원거리(1501~2000)  
/// 2001~3000   보조 무기 : 근접(2001~2500), 원거리(2501~3000)
/// 3001~4000   방어구 : 머리(3001~3200), 바디업(3201~3400), 바디다운(3401~3600), 신발(3601~3800)
/// 4001~5000   장신구 : 반지(4001~4500), 목걸이(4501~5000)
/// 6001~7000   포션 // 세부포션은 나중에 생각하자

[ System.Serializable]
public class ItemData{   
    public int itemCode;
    public int maxAmount = 1;
    public string itemName;
    public Sprite icon;
    public GameObject itemobject;
    [TextArea] public string description;
}

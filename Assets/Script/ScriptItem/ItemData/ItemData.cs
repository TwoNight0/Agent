using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 아이템 대분류 

/// <summary>
/// 아이템 코드 구성
/// 0~1000      재료
/// 1001~2000   무기
/// 2001~3000   방어구
/// 3001~4000   포션
/// </summary>


[ System.Serializable]
public class ItemData
{   
    public int itemCode;
    public int maxAmount = 1;
    public string itemName;
    public Sprite icon;
    public GameObject itemobject;
    [TextArea] public string description;
}

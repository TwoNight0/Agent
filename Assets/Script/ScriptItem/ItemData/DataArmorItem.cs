using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 대분류 

/// <summary>
/// 아이템 코드 구성
/// 0~1000      재료
/// 1001~2000   무기
/// 2001~3000   방어구 (2001 천 2101 가죽 2201 철)
/// 3001~4000   포션
/// </summary>

public class DataArmorItem : DataEquipmentItem
{
    /// enum -> string 검색?
    // 방어력, 내구도, 무게, 등급

    private enum material
    {
        fabric, //천
        leather, //가죽
        metal,
    }


    public int defense_physical = 0;
    public int defense_magic = 0;

    
    
}

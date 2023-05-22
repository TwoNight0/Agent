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

public class DataWeaponItem : DataEquipmentItem
{
    private int dmg_physic = 0;
    private int dmg_magic = 0;
    private float attack_speed = 1f;

    public int Dmg_physic
    {
        get => dmg_physic;
        set => dmg_physic = value;
    }

    public int Dmg_magic
    {
        get 
        { 
            return dmg_magic;
        }
        set => dmg_magic = value;
    }

    public float Attack_speed
    {
        get => attack_speed;
        set => attack_speed = value;
    }


}

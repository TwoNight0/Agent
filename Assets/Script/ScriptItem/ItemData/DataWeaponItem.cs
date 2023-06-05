using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 대분류 

/// <summary>
/// 아이템 코드 구성
/// 0~1000      재료
/// 1001~2000   메인 무기 : 근접(1001~1500), 원거리(1501~2000)  
/// 2001~3000   보조 무기 : 근접(2001~2500), 원거리(2501~3000)
/// 3001~4000   방어구 : 머리(3001~3200), 바디업(3201~3400), 바디다운(3401~3600), 신발(3601~3800)
/// 4001~5000   장신구 : 반지(4001~4500), 목걸이(4501~5000)
/// 6001~7000   포션 // 세부포션은 나중에 생각하자
/// </summary>

public class DataWeaponItem : DataEquipmentItem{
    private float dmg_physical = 0;
    private float dmg_magical = 0;
    private float attack_speed = 1f;

    #region (Get,Set)
    public float PubDmg_physic{
        get => dmg_physical;
        set => dmg_physical = value;
    }

    public float PubDmg_magic{
        get { 
            return dmg_magical;
        }
        set => dmg_magical = value;
    }

    public float PubAttack_speed{
        get => attack_speed;
        set => attack_speed = value;
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��з� 

/// <summary>
/// ������ �ڵ� ����
/// 0~1000      ���
/// 1001~2000   ���� ���� : ����(1001~1500), ���Ÿ�(1501~2000)  
/// 2001~3000   ���� ���� : ����(2001~2500), ���Ÿ�(2501~3000)
/// 3001~4000   �� : �Ӹ�(3001~3200), �ٵ��(3201~3400), �ٵ�ٿ�(3401~3600), �Ź�(3601~3800)
/// 4001~5000   ��ű� : ����(4001~4500), �����(4501~5000)
/// 6001~7000   ���� // ���������� ���߿� ��������
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

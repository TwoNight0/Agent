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

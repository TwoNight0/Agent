using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��з� 

/// <summary>
/// ������ �ڵ� ����
/// 0~1000      ���
/// 1001~2000   ����
/// 2001~3000   �� (2001 õ 2101 ���� 2201 ö)
/// 3001~4000   ����
/// </summary>

public class DataArmorItem : DataEquipmentItem
{
    /// enum -> string �˻�?
    // ����, ������, ����, ���

    private enum material
    {
        fabric, //õ
        leather, //����
        metal,
    }


    public int defense_physical = 0;
    public int defense_magic = 0;

    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ŭ���� ���� : �������� ����Ŭ����

// ������ ��з� 
/// 0~1000      ���
/// 1001~2000   ���� ���� : ����(1001~1500), ���Ÿ�(1501~2000)  
/// 2001~3000   ���� ���� : ����(2001~2500), ���Ÿ�(2501~3000)
/// 3001~4000   �� : �Ӹ�(3001~3200), �ٵ��(3201~3400), �ٵ�ٿ�(3401~3600), �Ź�(3601~3800)
/// 4001~5000   ��ű� : ����(4001~4500), �����(4501~5000)
/// 6001~7000   ���� // ���������� ���߿� ��������

[ System.Serializable]
public class ItemData{   
    public int itemCode;
    public int maxAmount = 1;
    public string itemName;
    public Sprite icon;
    public GameObject itemobject;
    [TextArea] public string description;
}

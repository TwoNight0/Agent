using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ ��з� 

/// <summary>
/// ������ �ڵ� ����
/// 0~1000      ���
/// 1001~2000   ����
/// 2001~3000   ��
/// 3001~4000   ����
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

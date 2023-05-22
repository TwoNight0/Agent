using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DataEquipmentItem
/// 1. 무기 2. 장비 로 나뉨
/// </summary>

public class DataEquipmentItem : ItemData
{
    // 착용장비? 내구도, 등급

    // 일반(흰색), 희귀(초록), 레어(파랑), 유니크(보라), 레전드(주황)
    public enum rarity
    {
        common,
        rare,
        unique,
        legend,
    }

    private int durability = 10; // 기본 내구도 10



}

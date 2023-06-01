using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 클래스 설명 : 아이템 자체의 클래스로 코드가 생성할때 코드를 바꾸면 그 아이템이 생성된다

public class Item : MonoBehaviour{
    [SerializeField] private int itemCode = 1001;
    [SerializeField] private int Account = 1;

    #region (Get, Set)
    public int GetItemCode{
        get => itemCode;
        set => itemCode = value;
    }

    public int GetAccount{
        get => Account;
        set => Account = value;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ŭ���� ���� : ������ ��ü�� Ŭ������ �ڵ尡 �����Ҷ� �ڵ带 �ٲٸ� �� �������� �����ȴ�

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

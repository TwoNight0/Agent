using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int itemCode = 1001;
    [SerializeField] private int Account = 1;


    public int GetItemCode
    {
        get => itemCode;
        set => itemCode = value;
    }

    public int GetAccount
    {
        get => Account;
        set => Account = value;
    }
   
}

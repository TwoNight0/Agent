using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//���Կ� �����͸� �ֱ� ���� ��ũ��Ʈ
public class ItemSlotScript : MonoBehaviour
{
    [SerializeField] private int itemCode = 0;
    [SerializeField] private int Account = 0;
    [SerializeField] public Sprite slotIcon;
    public Image icon;
    [SerializeField] private TextMeshProUGUI text;

    public void changeImage(Sprite _sprite) {
        icon.sprite = _sprite;
    }



    public int PubItemCode
    {
        get => itemCode;
        set => itemCode = value;
    }

    public int PubAccount
    {
        get => Account;
        set => Account = value;
    }

    public TextMeshProUGUI PubText
    {
        get => text;
        set => text = value;

    }

}

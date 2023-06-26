using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Npc sell or buy item 
/// </summary>
public class NpcShop : MonoBehaviour{
    [SerializeField] private Button btn_item1; 
    [SerializeField] private Button btn_item2; 
    [SerializeField] private Button btn_item3; 



    // Start is called before the first frame update
    void Start(){
        InventoryUIMng.Instance.gold.text = PlayerMng.Instance.PubGold.ToString();
    }

    // Update is called once per frame
    void Update(){

    }


    /// <summary>
    /// 상인과 상호작용(레이케스트)하여 shop이 on되면 item고유의 값인 가격을 바탕으로 아이템을 사고팔 수있음
    /// 플레이어의 골드를 확인 -> 사려고하는 물건의 가격을 확인(부족시)popup 돈부족 -> 공간적 여유가되는지 확인 -> (안될시)popup 공간부족,
    /// 
    /// </summary>
    public void ButItem1() {
        if (PlayerMng.Instance.PubGold > 200)
        {
            PlayerMng.Instance.PubGold -= 200;
            InventoryUIMng.Instance.gold.text = PlayerMng.Instance.PubGold.ToString();
            PlayerMng.Instance.PubSpeed += 5;
        }
        else
        {
            MngPopup.Instance.m_objPopup.SetActive(true);
            MngPopup.Instance.ShowMessage(new cPopup("오류","돈부족", null));
        }
    }
    public void ButItem2()
    {
        if (PlayerMng.Instance.PubGold > 150)
        {
            PlayerMng.Instance.PubGold -= 150;
            InventoryUIMng.Instance.gold.text = PlayerMng.Instance.PubGold.ToString();
            PlayerMng.Instance.PubPlayerdefense_physical += 10;
        }
        else
        {
            MngPopup.Instance.m_objPopup.SetActive(true);
            MngPopup.Instance.ShowMessage(new cPopup("오류", "돈부족", null));
        }
    }

    public void ButItem3()
    {
        if (PlayerMng.Instance.PubGold > 250)
        {
            PlayerMng.Instance.PubGold -= 250;
            InventoryUIMng.Instance.gold.text = PlayerMng.Instance.PubGold.ToString();
            PlayerMng.Instance.GetHp += 50;
        }
        else
        {
            MngPopup.Instance.m_objPopup.SetActive(true);
            MngPopup.Instance.ShowMessage(new cPopup("오류", "돈부족", null));
        }
    }
}

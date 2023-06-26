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
    /// ���ΰ� ��ȣ�ۿ�(�����ɽ�Ʈ)�Ͽ� shop�� on�Ǹ� item������ ���� ������ �������� �������� ����� ������
    /// �÷��̾��� ��带 Ȯ�� -> ������ϴ� ������ ������ Ȯ��(������)popup ������ -> ������ �������Ǵ��� Ȯ�� -> (�ȵɽ�)popup ��������,
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
            MngPopup.Instance.ShowMessage(new cPopup("����","������", null));
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
            MngPopup.Instance.ShowMessage(new cPopup("����", "������", null));
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
            MngPopup.Instance.ShowMessage(new cPopup("����", "������", null));
        }
    }
}

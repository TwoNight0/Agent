using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ŭ���� ���� : UI ����, UI�Լ� ����
public class UserDisplay : MonoBehaviour{

    #region (GameObject)(UI ������)
    private GameObject hill;
    private GameObject nomarl;
    private GameObject ultimate;
    private GameObject Weapon;
    private GameObject Dash;
    #endregion

    #region (Image)(UI ������)
    [SerializeField] private Image hp_cur;
    [SerializeField] private Image hillImage;
    [SerializeField] private Image nomarlImage;
    [SerializeField] private Image ultimateImage;
    [SerializeField] private Image MainWeaponImage;
    [SerializeField] private Image SubWeaponImage;
    [SerializeField] private Image DashImage;
    #endregion

// ---- [ start , Update ] ----
    void Start(){
        initDisplay();
    }
    
    void Update(){
        hpUpdate();
    }
// ---- ---- ---- ---- 

    private void hpUpdate(){
        hp_cur.fillAmount = PlayerMng.Instance.GetHp / PlayerMng.Instance.GetMaxHp;
    }

    //UI�̹������� ������ ĳ������ ��ų�� �°� �������ִ� �޼���
    private void initDisplay(){
        //������Ʈ����
        hill = GameObject.Find("Hill");
        nomarl = GameObject.Find("skill_nomal");
        ultimate = GameObject.Find("skill_Ultimate");
        Weapon = GameObject.Find("Weapon");
        Dash = GameObject.Find("Dash");

        //�̹����Ҵ� //playerMng�� �̹����� �����ְ� �� �̹����� �޾���
        hp_cur = transform.Find("Hp_Cur").GetComponentInChildren<Image>();
        hillImage.sprite = PlayerMng.Instance.hillImg; 
        nomarlImage.sprite = PlayerMng.Instance.nomalSkillImg;
        ultimateImage.sprite = PlayerMng.Instance.UltimateSkillImg;
        MainWeaponImage.sprite = PlayerMng.Instance.MainWeaponImg;
        SubWeaponImage.sprite = PlayerMng.Instance.SubWeaponImg;
        DashImage.sprite = PlayerMng.Instance.DashImg;

        //������Ʈ�� �̹����� �Ҵ�
        hillImage = hill.GetComponent<Image>();
        nomarlImage = nomarl.GetComponent<Image>();
        ultimateImage = ultimate.GetComponent<Image>();
        MainWeaponImage = Weapon.GetComponent<Image>();
        SubWeaponImage = Weapon.GetComponent<Image>();
        DashImage = Dash.GetComponent<Image>();
    }
}

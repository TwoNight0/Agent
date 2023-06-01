using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 클래스 설명 : UI 관리, UI함수 보유
public class UserDisplay : MonoBehaviour{

    #region (GameObject)(UI 아이콘)
    private GameObject hill;
    private GameObject nomarl;
    private GameObject ultimate;
    private GameObject Weapon;
    private GameObject Dash;
    #endregion

    #region (Image)(UI 아이콘)
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

    //UI이미지들을 선택한 캐릭터의 스킬에 맞게 변경해주는 메서드
    private void initDisplay(){
        //오브젝트지정
        hill = GameObject.Find("Hill");
        nomarl = GameObject.Find("skill_nomal");
        ultimate = GameObject.Find("skill_Ultimate");
        Weapon = GameObject.Find("Weapon");
        Dash = GameObject.Find("Dash");

        //이미지할당 //playerMng가 이미지를 보내주고 그 이미지를 받았음
        hp_cur = transform.Find("Hp_Cur").GetComponentInChildren<Image>();
        hillImage.sprite = PlayerMng.Instance.hillImg; 
        nomarlImage.sprite = PlayerMng.Instance.nomalSkillImg;
        ultimateImage.sprite = PlayerMng.Instance.UltimateSkillImg;
        MainWeaponImage.sprite = PlayerMng.Instance.MainWeaponImg;
        SubWeaponImage.sprite = PlayerMng.Instance.SubWeaponImg;
        DashImage.sprite = PlayerMng.Instance.DashImg;

        //오브젝트에 이미지를 할당
        hillImage = hill.GetComponent<Image>();
        nomarlImage = nomarl.GetComponent<Image>();
        ultimateImage = ultimate.GetComponent<Image>();
        MainWeaponImage = Weapon.GetComponent<Image>();
        SubWeaponImage = Weapon.GetComponent<Image>();
        DashImage = Dash.GetComponent<Image>();
    }
}

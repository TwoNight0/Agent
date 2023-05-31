using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDisplay : MonoBehaviour
{
    [SerializeField] private Image hp_cur;
    private GameObject hill;
    private GameObject nomarl;
    private GameObject ultimate;
    private GameObject Weapon;

    [SerializeField] private Image hillImage;
    [SerializeField] private Image nomarlImage;
    [SerializeField] private Image ultimateImage;
    [SerializeField] private Image MainWeaponImage;
    [SerializeField] private Image SubWeaponImage;
    
    void Start()
    {
        hp_cur = transform.Find("Hp_Cur").GetComponentInChildren<Image>();
        initUIImg();
    }

    
    void Update()
    {
        hpUpdate();
    }

    private void hpUpdate()
    {
        hp_cur.fillAmount = PlayerMng.Instance.GetHp / PlayerMng.Instance.GetMaxHp;
    }

    //UI이미지들을 선택한 캐릭터의 스킬에 맞게 변경해주는 메서드
    private void initUIImg()
    {
        //오브젝트지정
        hill = GameObject.Find("Hill");
        nomarl = GameObject.Find("skill_nomal");
        ultimate = GameObject.Find("skill_Ultimate");
        Weapon = GameObject.Find("Weapon");

        //이미지할당
        hillImage.sprite = PlayerMng.Instance.hillImg;
        nomarlImage.sprite = PlayerMng.Instance.nomalSkillImg;
        ultimateImage.sprite = PlayerMng.Instance.UltimateSkillImg;
        MainWeaponImage.sprite = PlayerMng.Instance.MainWeaponImg;
        SubWeaponImage.sprite = PlayerMng.Instance.SubWeaponImg;

        //오브젝트에 이미지를 할당
        hillImage = hill.GetComponent<Image>();
        nomarlImage = nomarl.GetComponent<Image>();
        ultimateImage = ultimate.GetComponent<Image>();
        MainWeaponImage = Weapon.GetComponent<Image>();
    }



}

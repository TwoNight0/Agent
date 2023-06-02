using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Ŭ���� ���� : UI ����, UI�Լ� ����
public class UserDisplay : MonoBehaviour{
    public static UserDisplay Instance;

    #region (GameObject)(UI ������)
    private GameObject hill;
    private GameObject nomarl;
    private GameObject ultimate;
    private GameObject Weapon;
    private GameObject Dash;
    #endregion

    #region (Image)(UI ������)
    [SerializeField] public Image hp_cur;
    [SerializeField] public Image hillImage;
    [SerializeField] public Image nomarlImage;
    [SerializeField] public Image ultimateImage;
    [SerializeField] public Image MainWeaponImage;
    [SerializeField] public Image SubWeaponImage;
    [SerializeField] public Image DashImage;
    #endregion

    private int bullet;

    [SerializeField] public TextMeshProUGUI BulletTMP;

    // ---- [ start , Update ] ----
    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }
    void Start(){
        DontDestroyOnLoad(this);
        initDisplay();
        MainWeaponImage.color = Color.blue;
        SubWeaponImage.color = Color.blue;
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
        hp_cur = transform.Find("Hp_Cur").GetComponentInChildren<Image>();
        hill = GameObject.Find("Hill");
        nomarl = GameObject.Find("skill_nomal");
        ultimate = GameObject.Find("skill_Ultimate");
        Weapon = GameObject.Find("Weapon");
        Dash = GameObject.Find("Dash");

        //�̹����Ҵ� //playerMng�� �̹����� �����ְ� �� �̹����� �޾���
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

        bullet = 0;
    }

    private void applyColor(){

    }

    private void CrossHairUI(){

    }
}

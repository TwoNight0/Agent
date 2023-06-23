using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Ŭ���� ���� : UI ����, UI�Լ� ����
public class UserDisplay : MonoBehaviour{
    public static UserDisplay Instance;


    #region (Image)(UI ������)
    [SerializeField] public Image hp_cur;
    [SerializeField] public Image hillImage;
    [SerializeField] public Image nomarlImage;
    [SerializeField] public Image ultimateImage;
    [SerializeField] public Image MainWeaponImage;
    [SerializeField] public Image SubWeaponImage;
    [SerializeField] public Image DashImage;

    public Image Filter_Hill;
    public Image Filter_Dash;
    public Image Filter_skill_nomal;
    public Image Filter_skill_Ultimate;
    #endregion

    private int bullet;

    [SerializeField] public TextMeshProUGUI whichone;
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

        //�̹����Ҵ� //playerMng�� �̹����� �����ְ� �� �̹����� �޾���
        hillImage.sprite = PlayerMng.Instance.hillImg; 
        nomarlImage.sprite = PlayerMng.Instance.nomalSkillImg;
        ultimateImage.sprite = PlayerMng.Instance.UltimateSkillImg;
        MainWeaponImage.sprite = PlayerMng.Instance.MainWeaponImg;
        SubWeaponImage.sprite = PlayerMng.Instance.SubWeaponImg;
        DashImage.sprite = PlayerMng.Instance.DashImg;

        //�ʱ�� �ֱ�
        MainWeaponImage.color = Color.cyan;
        SubWeaponImage.color = Color.cyan;

        bullet = 0;
    }

    private void CrossHairUI(){

    }
}

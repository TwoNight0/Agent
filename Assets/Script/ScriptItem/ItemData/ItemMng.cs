using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemMng : MonoBehaviour
{
    public static ItemMng Instance;
    private int itemCode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<DataArmorItem> ArmorList = new List<DataArmorItem>();
    public List<DataWeaponItem> WeaponList = new List<DataWeaponItem>();

    #region DataArmor
    public DataArmorItem fabric_up = new DataArmorItem();
    public DataArmorItem fabric_down = new DataArmorItem();
    public DataArmorItem leather_up = new DataArmorItem();
    public DataArmorItem leather_down = new DataArmorItem();
    public DataArmorItem metal_up = new DataArmorItem();
    public DataArmorItem metal_down = new DataArmorItem();
    public DataArmorItem shoes = new DataArmorItem();
    #endregion
    #region DataWeapon
    public DataWeaponItem shotsword = new DataWeaponItem();
    public DataWeaponItem longsword = new DataWeaponItem();
    public DataWeaponItem wand = new DataWeaponItem();
    public DataWeaponItem stick = new DataWeaponItem();
    #endregion


    private void Start()
    {
        DontDestroyOnLoad(this);
        createItem();

        initWeaponData(); // 나중에 itemMng스크립트로 옮기자
        initArmorData();

        //이것도 자동화하는 방법이 있긴할거야 제이슨에 이름만적으면되겠끔해서
        AddItem(ArmorList, fabric_up);
        AddItem(ArmorList, fabric_down);
        AddItem(ArmorList, leather_up);
        AddItem(ArmorList, leather_down);
        AddItem(ArmorList, metal_up);
        AddItem(ArmorList, metal_down);
        AddItem(ArmorList, shoes);

        AddItem(WeaponList, shotsword);
        AddItem(WeaponList, longsword);
        AddItem(WeaponList, wand);
        AddItem(WeaponList, stick);
    }

    private void Update()
    {

    }


    //아이템을 리스트에 등록하는 코드(코드로 식별하기 위함)
    private void AddItem<T>(List<T> _ItemList, T _item)
    {
        _ItemList.Add(_item);
    }

    /// <summary>
    /// 아이템 코드 구성
    /// 0~1000      재료
    /// 1001~2000   메인 무기 : 근접(1001~1500), 원거리(1501~2000)  
    /// 2001~3000   보조 무기 : 근접(2001~2500), 원거리(2501~3000)
    /// 3001~4000   방어구 : 머리(3001~3200), 바디업(3201~3400), 바디다운(3401~3600), 신발(3601~3800)
    /// 4001~5000   장신구 : 반지(4001~4500), 목걸이(4501~5000)
    /// 6001~7000   포션 // 세부포션은 나중에 생각하자
    /// </summary>
    private void initArmorData()
    {
        fabric_up.itemCode = 3201;
        fabric_up.itemName = "fabric_up";
        fabric_up.icon = Resources.Load<Sprite>("armor/fabric_up");
        fabric_up.defense_magic = 6;

        fabric_down.itemCode = 3401;
        fabric_down.itemName = "fabric_down";
        fabric_down.defense_magic = 4;

        leather_up.itemCode = 3202;
        leather_up.itemName = "leather_up";
        leather_up.icon = Resources.Load<Sprite>("armor/leather_up");
        leather_up.defense_physical = 4;
        leather_up.defense_magic = 4;

        leather_down.itemCode = 3402;
        leather_down.itemName = "leather_down";
        leather_down.defense_physical = 3;
        leather_down.defense_magic = 3;

        metal_up.itemCode = 3203;
        metal_up.itemName = "metal_top";
        metal_up.icon = Resources.Load<Sprite>("armor/metal_up");
        metal_up.defense_physical = 10;

        metal_down.itemCode = 3403;
        metal_down.itemName = "metal_bottom";
        metal_down.defense_physical = 8;
    }

    private void initWeaponData()
    {
        shotsword.itemCode = 1001;
        shotsword.itemName = "shotsword";
        shotsword.icon = Resources.Load<Sprite>("weapon/shotsword");
        shotsword.Dmg_physic = 2;
        shotsword.Attack_speed = 2;


        longsword.itemCode = 1002;
        longsword.itemName = "longsword";
        longsword.icon = Resources.Load<Sprite>("weapon/longsword");
        longsword.Dmg_physic = 4;
        longsword.Attack_speed = 1;


        wand.itemCode = 1501;
        wand.itemName = "wand";
        wand.icon = Resources.Load<Sprite>("weapon/wand");
        wand.Dmg_magic = 8;
        wand.Attack_speed = 3;


        stick.itemCode = 1502;
        stick.itemName = "stick";
        stick.Dmg_magic = 6;
        stick.Attack_speed = 2;
    }

    private void createItem()
    {
        //오브젝트 만들고 코드 부여, 박스(3d), 위치속성
    }


  
}

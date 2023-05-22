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
    public DataArmorItem fabric_top = new DataArmorItem();
    public DataArmorItem fabric_bottom = new DataArmorItem();
    public DataArmorItem leather_top = new DataArmorItem();
    public DataArmorItem leather_bottom = new DataArmorItem();
    public DataArmorItem metal_top = new DataArmorItem();
    public DataArmorItem metal_bottom = new DataArmorItem();
    public DataArmorItem shoes = new DataArmorItem();
    #endregion
    #region DataWeapon
    public DataWeaponItem ShotSword = new DataWeaponItem();
    public DataWeaponItem LongSword = new DataWeaponItem();
    public DataWeaponItem Wand = new DataWeaponItem();
    public DataWeaponItem Stick = new DataWeaponItem();
    #endregion


    private void Start()
    {
        DontDestroyOnLoad(this);
        createItem();

        initWeaponData(); // ���߿� itemMng��ũ��Ʈ�� �ű���
        initArmorData();

        //�̰͵� �ڵ�ȭ�ϴ� ����� �ֱ��Ұž� ���̽��� �̸���������ǰڲ��ؼ�
        AddItem(ArmorList, fabric_top);
        AddItem(ArmorList, fabric_bottom);
        AddItem(ArmorList, leather_top);
        AddItem(ArmorList, leather_bottom);
        AddItem(ArmorList, metal_top);
        AddItem(ArmorList, metal_bottom);
        AddItem(ArmorList, shoes);

        AddItem(WeaponList, ShotSword);
        AddItem(WeaponList, LongSword);
        AddItem(WeaponList, Wand);
        AddItem(WeaponList, Stick);
    }

    private void Update()
    {

    }


    //�������� ����Ʈ�� ����ϴ� �ڵ�(�ڵ�� �ĺ��ϱ� ����)
    private void AddItem<T>(List<T> _ItemList, T _item)
    {
        _ItemList.Add(_item);
    }


    private void initArmorData()
    {
        fabric_top.itemCode = 2001;
        fabric_top.itemName = "fabic_top";
        fabric_top.defense_magic = 6;
        fabric_top.icon = Resources.Load<Sprite>("fabricTop");

        fabric_bottom.itemCode = 2011;
        fabric_bottom.itemName = "fabric_bottom";
        fabric_bottom.defense_magic = 4;

        leather_top.itemCode = 2101;
        leather_top.itemName = "leather_top";
        leather_top.defense_physical = 4;
        leather_top.defense_magic = 4;

        leather_bottom.itemCode = 2111;
        leather_bottom.itemName = "leather_bottom";
        leather_bottom.defense_physical = 3;
        leather_bottom.defense_magic = 3;

        metal_top.itemCode = 2201;
        metal_top.itemName = "metal_top";
        metal_top.defense_physical = 10;

        metal_bottom.itemCode = 2211;
        metal_bottom.itemName = "metal_bottom";
        metal_bottom.defense_physical = 8;

    }

    private void initWeaponData()
    {
        ShotSword.itemCode = 1001;
        ShotSword.itemName = "ShotSword";
        ShotSword.Dmg_physic = 2;
        ShotSword.Attack_speed = 2;


        LongSword.itemCode = 1002;
        LongSword.itemName = "LongSword";
        LongSword.icon = Resources.Load<Sprite>("LongSword");
        LongSword.Dmg_physic = 4;
        LongSword.Attack_speed = 1;


        Wand.itemCode = 1101;
        Wand.itemName = "Wand";
        Wand.Dmg_magic = 8;
        Wand.Attack_speed = 3;


        Stick.itemCode = 1102;
        Stick.itemName = "Stick";
        Stick.Dmg_magic = 6;
        Stick.Attack_speed = 2;
    }

    private void createItem()
    {
        //������Ʈ ����� �ڵ� �ο�, �ڽ�(3d), ��ġ�Ӽ�
    }


  
}

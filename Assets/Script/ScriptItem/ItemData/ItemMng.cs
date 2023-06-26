using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//Ŭ���� ���� : ������ ����, ������ �ҷ�����, ������ ���
//(�������� �ʴ� �κ�)
public class ItemMng : MonoBehaviour
{
    public static ItemMng Instance;
    private int itemCode;

    #region (List)(SaveForm, DataArmor, DataWeapon)
    private List<SaveForm> Inventory;
    public List<DataArmorItem> ArmorList = new List<DataArmorItem>();
    public List<DataWeaponItem> WeaponList = new List<DataWeaponItem>();
    #endregion

    private void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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

    private void Start(){
        initWeaponData();
        initArmorData();
        // ----

        createItem(); // �����ؾ��� 
    }


    private void AddItem<T>(List<T> _ItemList, T _item){
        _ItemList.Add(_item);
    }


    #region �ʱ�ȭ(initWeapon, initArmor)
    //�������� ����Ʈ�� ����ϴ� �ڵ�(�ڵ�� �ĺ��ϱ� ����)
    /// <summary>
    /// ������ �ڵ� ����
    /// 0~1000      ���
    /// 1001~2000   ���� ���� : ����(1001~1500), ���Ÿ�(1501~2000)  
    /// 2001~3000   ���� ���� : ����(2001~2500), ���Ÿ�(2501~3000)
    /// 3001~4000   �� : �Ӹ�(3001~3200), �ٵ��(3201~3400), �ٵ�ٿ�(3401~3600), �Ź�(3601~3800)
    /// 4001~5000   ��ű� : ����(4001~4500), �����(4501~5000)
    /// 6001~7000   ���� // ���������� ���߿� ��������
    /// </summary>
    private void initWeaponData(){
        shotsword.itemCode = 1001;
        shotsword.itemName = "shotsword";
        shotsword.icon = Resources.Load<Sprite>("weapon/shotsword");
        shotsword.PubDmg_physic = 2;
        shotsword.PubAttack_speed = 2;
        shotsword.PubPrice = 80;

        longsword.itemCode = 1002;
        longsword.itemName = "longsword";
        longsword.icon = Resources.Load<Sprite>("weapon/longsword");
        longsword.PubDmg_physic = 4;
        longsword.PubAttack_speed = 1;
        longsword.PubPrice = 110;

        wand.itemCode = 1501;
        wand.itemName = "wand";
        wand.icon = Resources.Load<Sprite>("weapon/wand");
        wand.PubDmg_magic = 8;
        wand.PubAttack_speed = 3;
        wand.PubPrice = 85;


        stick.itemCode = 1502;
        stick.itemName = "stick";
        stick.PubDmg_magic = 6;
        stick.PubAttack_speed = 2;
        stick.PubPrice = 90;

        AddItem(WeaponList, shotsword);
        AddItem(WeaponList, longsword);
        AddItem(WeaponList, wand);
        AddItem(WeaponList, stick);
    }
    private void initArmorData(){
        fabric_up.itemCode = 3201;
        fabric_up.itemName = "fabric_up";
        fabric_up.icon = Resources.Load<Sprite>("armor/fabric_up");
        fabric_up.PubDefense_magical = 6.0f;
        fabric_up.PubPrice = 60;

        fabric_down.itemCode = 3401;
        fabric_down.itemName = "fabric_down";
        fabric_down.PubDefense_magical = 4.0f;
        fabric_down.PubPrice = 50;


        leather_up.itemCode = 3202;
        leather_up.itemName = "leather_up";
        leather_up.icon = Resources.Load<Sprite>("armor/leather_up");
        leather_up.PubDefense_physical = 4.0f;
        leather_up.PubDefense_magical = 4.0f;
        leather_up.PubPrice = 65;

        leather_down.itemCode = 3402;
        leather_down.itemName = "leather_down";
        leather_down.PubDefense_physical = 3.0f;
        leather_down.PubDefense_magical = 3.0f;
        leather_down.PubPrice = 60;

        metal_up.itemCode = 3203;
        metal_up.itemName = "metal_top";
        metal_up.icon = Resources.Load<Sprite>("armor/metal_up");
        metal_up.PubDefense_physical = 10.0f;
        metal_up.PubPrice = 120;

        metal_down.itemCode = 3403;
        metal_down.itemName = "metal_bottom";
        metal_down.PubDefense_physical = 8.0f;
        metal_down.PubPrice = 110;

        AddItem(ArmorList, fabric_up);
        AddItem(ArmorList, fabric_down);
        AddItem(ArmorList, leather_up);
        AddItem(ArmorList, leather_down);
        AddItem(ArmorList, metal_up);
        AddItem(ArmorList, metal_down);
        AddItem(ArmorList, shoes);
    }
    #endregion

    private void createItem(){
        //������Ʈ ����� �ڵ� �ο�, �ڽ�(3d), ��ġ�Ӽ�
        //GameObject obj = Instantiate(itemSlot, transform);
        //ListSlot.Add(obj.GetComponent<ItemSlotScript>()); //listSlot�� ����ؼ� �����۽�ũ��Ʈ���� �������
                                                          //saveList.Add(obj.GetComponent<SaveForm>()); //����ɸ���Ʈ�� ���� ������ ���� 
        //obj.name = _name + invencount;
    }

    public (float, float) giveDmgData(int _itemCode){
        DataWeaponItem weaponItem = null;
        float physic = 0.0f;
        float magic = 0.0f;
        
        weaponItem = ItemMng.Instance.WeaponList.Find(x => x.itemCode == _itemCode);
        
        //List<DataWeaponItem> _WeaponList = ItemMng.Instance.WeaponList;
        //int count = _WeaponList.Count;
        //for (int iNum = 0; iNum < count; ++iNum)
        //{
        //    if (_WeaponList[iNum].itemCode == _itemCode)
        //    {
        //        weaponItem = _WeaponList[iNum];
        //        break;
        //    }
        //}
        
        physic = weaponItem.PubDmg_physic;
        magic = weaponItem.PubDmg_magic;
        
        return (physic, magic);
    }

    public (float, float) giveDefenseData(int _itemCode){
        DataArmorItem armorItem;
        float physic = 0.0f;
        float magic = 0.0f;
        
        armorItem = ItemMng.Instance.ArmorList.Find(x => x.itemCode == _itemCode);
        physic = armorItem.PubDefense_physical;
        magic = armorItem.PubDefense_magical;
        
        return (physic, magic);
    }

   


    #region Save, Load
    //--- Save ---
    public void inventorysave(List<SaveForm> _List)
    {
        Inventory = _List; //���ΰ�ħ
        string key = "inven";

        string invensave = JsonConvert.SerializeObject(Inventory);
        PlayerPrefs.SetString(key, invensave);
        InventoryUIMng.Instance.printInventroy(Inventory); //�� �����߳� Ȯ��
        Debug.Log("���� : " + invensave);

    }

    //ū����Ʈ�� �Ϻ������� ��������Ʈ�� �Űܴ���(�̹����� Jason���� �����Ҽ� ���⶧���� �̹����� ������ Ŭ������ �Űܴ���)
    public List<SaveForm> ListCopy(List<ItemSlotScript> rawList)
    {
        List<SaveForm> copyList = new List<SaveForm>();
        int len = rawList.Count;
        for (int i = 0; i < len; i++)
        {
            SaveForm form = new SaveForm() { SAccount = rawList[i].PubAccount, Sitemcode = rawList[i].PubItemCode };
            copyList.Add(form);
            //copyList[i].Sitemcode = rawList[i].PubItemCode;
            //copyList[i].SAccount = rawList[i].PubAccount;
        }
        return copyList;
    }
    //----

    // --- Load ---
    //�������� ����Ǿ�� �� �ñ�>
    //�������� �����Ҷ�
    //�������� �ű涧
    //�������� ������
    public void inventoryload(string _jsonsaved)
    {
        string key = "inven";
        Inventory = JsonConvert.DeserializeObject<List<SaveForm>>(_jsonsaved);
        Debug.Log("�ҷ��� : ");
        PlayerPrefs.GetString(key);
        InventoryUIMng.Instance.printInventroy(Inventory); //�� �ҷ��Գ� Ȯ��
    }
    #endregion

}

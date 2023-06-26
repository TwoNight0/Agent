using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//클래스 설명 : 아이템 저장, 아이템 불러오기, 아이템 등록
//(보여지지 않는 부분)
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

        createItem(); // 제작해야함 
    }


    private void AddItem<T>(List<T> _ItemList, T _item){
        _ItemList.Add(_item);
    }


    #region 초기화(initWeapon, initArmor)
    //아이템을 리스트에 등록하는 코드(코드로 식별하기 위함)
    /// <summary>
    /// 아이템 코드 구성
    /// 0~1000      재료
    /// 1001~2000   메인 무기 : 근접(1001~1500), 원거리(1501~2000)  
    /// 2001~3000   보조 무기 : 근접(2001~2500), 원거리(2501~3000)
    /// 3001~4000   방어구 : 머리(3001~3200), 바디업(3201~3400), 바디다운(3401~3600), 신발(3601~3800)
    /// 4001~5000   장신구 : 반지(4001~4500), 목걸이(4501~5000)
    /// 6001~7000   포션 // 세부포션은 나중에 생각하자
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
        //오브젝트 만들고 코드 부여, 박스(3d), 위치속성
        //GameObject obj = Instantiate(itemSlot, transform);
        //ListSlot.Add(obj.GetComponent<ItemSlotScript>()); //listSlot에 등록해서 아이템스크립트들을 등록했음
                                                          //saveList.Add(obj.GetComponent<SaveForm>()); //저장될리스트도 같은 개수로 만듬 
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
        Inventory = _List; //새로고침
        string key = "inven";

        string invensave = JsonConvert.SerializeObject(Inventory);
        PlayerPrefs.SetString(key, invensave);
        InventoryUIMng.Instance.printInventroy(Inventory); //잘 저장했나 확인
        Debug.Log("저장 : " + invensave);

    }

    //큰리스트의 일부정보를 스몰리스트에 옮겨담음(이미지는 Jason으로 저장할수 없기때문에 이미지를 제외한 클래스로 옮겨다음)
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
    //아이템이 저장되어야 할 시기>
    //아이템을 습득할때
    //아이템을 옮길때
    //아이템을 버릴때
    public void inventoryload(string _jsonsaved)
    {
        string key = "inven";
        Inventory = JsonConvert.DeserializeObject<List<SaveForm>>(_jsonsaved);
        Debug.Log("불러옴 : ");
        PlayerPrefs.GetString(key);
        InventoryUIMng.Instance.printInventroy(Inventory); //잘 불러왔나 확인
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//클래스 설명 : 아이템 저장, 아이템 불러오기, 아이템 등록
//(보여지지 않는 부분)
public class ItemMng : MonoBehaviour{
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
        DontDestroyOnLoad(this);
        initWeaponData(); 
        initArmorData();
        // ----

        createItem(); // 제작해야함 
    }

    
    private void AddItem<T>(List<T> _ItemList, T _item)
    {
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

        AddItem(WeaponList, shotsword);
        AddItem(WeaponList, longsword);
        AddItem(WeaponList, wand);
        AddItem(WeaponList, stick);
    }
    private void initArmorData(){
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
    }

    #region Save, Load
    //--- Save ---
    public void inventorysave(List<SaveForm> _List){
        Inventory = _List; //새로고침
        string key = "inven";
        
        string invensave = JsonConvert.SerializeObject(Inventory);
        PlayerPrefs.SetString(key, invensave);
        InventoryUIMng.Instance.printInventroy(Inventory); //잘 저장했나 확인
        Debug.Log("저장 : " + invensave);

    }

    //큰리스트의 일부정보를 스몰리스트에 옮겨담음(이미지는 Jason으로 저장할수 없기때문에 이미지를 제외한 클래스로 옮겨다음)
    public List<SaveForm> ListCopy(List<ItemSlotScript> rawList){
        List<SaveForm> copyList = new List<SaveForm>();
        int len = rawList.Count;
        for(int i = 0; i<len; i++){
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
    public void inventoryload(string _jsonsaved){
        string key = "inven";
        Inventory = JsonConvert.DeserializeObject<List<SaveForm>>(_jsonsaved);
        Debug.Log("불러옴 : ");
        PlayerPrefs.GetString(key);
        InventoryUIMng.Instance.printInventroy(Inventory); //잘 불러왔나 확인
    }
    #endregion

  
}

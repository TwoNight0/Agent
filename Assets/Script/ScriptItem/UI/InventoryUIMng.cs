using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 클래스 설명 : 인벤토리에있는 UI를 관리
// (보여지는 부분) inventory와 상호작용하는 스크립트
public class InventoryUIMng : MonoBehaviour{
    public static InventoryUIMng Instance;

    //실제적인 인벤토리공간 이걸 capacity 만큼만 운용되게 만들어야해
    public List<ItemSlotScript> ListSlot = new List<ItemSlotScript>(); //인벤토리안의 리스트
    //public List<SaveForm> saveList = new List<SaveForm>();
    private EventSystem m_eventSystem;
    PlayerMng playerMng;
 
    //

    //슬롯 선택시 사용하는 변수들
    ItemSlotScript firstItemSlot = null;
    ItemSlotScript secondItemSlot = null;
    
    
    public RectTransform DragImage = new RectTransform();
    [Header("슬롯 수")]
    [SerializeField] private int capacity = 20; //인벤토리 칸 추후늘리면됨
    private int invencount = 0;

    [SerializeField] private GameObject itemSlot;

    #region (Button)(inven,stat,skill,Exit)
    //버튼
    [SerializeField] public Button btn_inven;
    [SerializeField] public Button btn_stat;
    [SerializeField] public Button btn_skill;
    [SerializeField] public Button btn_Exit;
    #endregion


    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }

    // 산하에 capacity
    private void Start(){
        DontDestroyOnLoad(this);

        playerMng = FindObjectOfType<PlayerMng>();
        initInventory(); //인벤토리 생성
        addbuttonAction(); //버튼액션 지정
        
    }

    private void Update(){
        slotMove();
        
    }

    // 아이템 대분류 
    /// 0~1000      재료
    /// 1001~2000   메인 무기 : 근접(1001~1500), 원거리(1501~2000)  
    /// 2001~3000   보조 무기 : 근접(2001~2500), 원거리(2501~3000)
    /// 3001~4000   방어구 : 머리(3001~3200), 바디업(3201~3400), 바디다운(3401~3600), 신발(3601~3800)
    /// 4001~5000   장신구 : 반지(4001~4500), 목걸이(4501~5000)
    /// 6001~7000   포션 // 세부포션은 나중에 생각하자

    #region Init
    //초기 capacity만큼 인벤토리칸을 생성해주는 코드 
    private void initInventory(){
        for (int i = 0; i < capacity; i++){
            createItemSlot("ItemSlot");
        }
    }
    #endregion

    #region Slot관리
    //정보를 찾아서 자료형에 맞게 반환
    public (DataWeaponItem weapondata, DataArmorItem armordata) findObjData(int _itemCode){
        DataWeaponItem weapondata = null;
        DataArmorItem armordata = null;
        if (_itemCode > 0 && _itemCode < 1001){//재료
         //재료리스트 만들기
        }
        else if (_itemCode > 1000 && _itemCode < 3001){//메인무기
            weapondata = ItemMng.Instance.WeaponList.Find(x => x.itemCode == _itemCode);
        }
        else if (_itemCode > 3000 && _itemCode < 4000){//방어구
            armordata = ItemMng.Instance.ArmorList.Find(x => x.itemCode == _itemCode);
        }
        return (weapondata, armordata);
    }

    //슬롯생성하는 코드
    private void createItemSlot(string _name = "itemSlot"){
        if(invencount < capacity){// 슬롯보다 더 많은 양이 생성되지 않도록
            GameObject obj = Instantiate(itemSlot, transform);
            ListSlot.Add(obj.GetComponent<ItemSlotScript>()); //listSlot에 등록해서 아이템스크립트들을 등록했음
            //saveList.Add(obj.GetComponent<SaveForm>()); //저장될리스트도 같은 개수로 만듬 
            obj.name = _name+ invencount;

            invencount++;
        }
    }

    //슬롯에다가 아이템의 정보를 넘겨줌 (이것도 아이템을 찾는 것과 넣는것으로 분리했어야했다 
    public void giveDataItemSlot(int _itemCode){
        (DataWeaponItem, DataArmorItem) data = findObjData(_itemCode); // 각종 리스트들 중에서 원하는 오브젝트찾음 
        
        //List에서 아이템코드가 같은게 있는지 item을 찾음
        ItemSlotScript item = ListSlot.Find(x => x.PubItemCode == _itemCode);
        if (item != null){//item이 이미 있다면. 갯수를 증가시킴
            item.PubAccount++;
            item.PubText.text = item.PubAccount.ToString();
            List<SaveForm> saveList = ItemMng.Instance.ListCopy(ListSlot);
            ItemMng.Instance.inventorysave(saveList);
        }
        else{//만약 item이 없다면
            bool findit = false; //못찾음
            int count = ListSlot.Count; //리스트의 개수
            for (int i = 0; i < count; ++i){
                item = ListSlot[i]; 
                if (item.PubItemCode == 0){//코드가 0이라면
                    item.PubItemCode = _itemCode;
                    item.PubAccount = 1;
                    item.PubText.text = item.PubAccount.ToString();
                    // 인벤토리 아이콘과 수량표시를 변경해야함
                    if (_itemCode > 1000 && _itemCode < 3001){
                        item.changeImage(data.Item1.icon);
                    }
                    else if (_itemCode > 3000 && _itemCode < 4001){
                        item.changeImage(data.Item2.icon);
                    }
                    List<SaveForm> saveList = ItemMng.Instance.ListCopy(ListSlot);
                    ItemMng.Instance.inventorysave(saveList);
                    findit = true;
                    break;
                }
            }

            if (findit == false){
                //TODO : 아이템 창이 가득 찼다고 알리고 아이템을 먹지안도록해야함
            }
        }
    }

    // 레이케스트를 쏨 -> 슬롯을 선택 슬롯에 대한 스크립트를 가져옴 -> 선택된슬롯이라는 곳에 잠시저장. 이후선택된 오브젝트를 교환
    private void slotMove(){     
            PointerEventData pointEvent = new PointerEventData(m_eventSystem);
            
            //첫번째 슬롯
            if (Input.GetMouseButtonDown(0)){
                RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");//리스트 중에서 tag가 ItemSlot인것을 찾음
                if (result.gameObject == null){//예외처리
                    return;
                }
                else{
                    Debug.Log("첫번째 슬롯 :" + result);
                    firstItemSlot = result.gameObject.GetComponent<ItemSlotScript>();
                }
            }

            if (Input.GetMouseButton(0)){ //마우스를 누르고있으면 이미지가 계속따라오는 함수
                if (firstItemSlot != null){
                    DragImage.gameObject.SetActive(true);
                    DragImage.position = Input.mousePosition;
                    Image DI = DragImage.GetComponent<Image>();
                    DI.sprite = firstItemSlot.icon.sprite;
                }
            }

            //두번째 슬롯
            if (Input.GetMouseButtonUp(0)){
                DragImage.gameObject.SetActive(false);
                RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");

                if (result.gameObject == null){//예외처리
                    firstItemSlot = null;
                    return;
                }
                else{//null 아니면
                    Debug.Log("두번째 슬롯 : " + result);
                    secondItemSlot = result.gameObject.GetComponent<ItemSlotScript>(); // 스크립트 가져왔음
                    if ((firstItemSlot != secondItemSlot)){
                        //아이템슬롯의 아이템일때
                        if (secondItemSlot.gameObject.name.Contains("ItemSlot")){
                            Debug.Log("Test");
                            //코드 교환
                            int temp = firstItemSlot.PubItemCode;
                            firstItemSlot.PubItemCode = secondItemSlot.PubItemCode;
                            secondItemSlot.PubItemCode = temp;

                            //갯수 교환
                            int tmp = firstItemSlot.PubAccount;
                            firstItemSlot.PubAccount = secondItemSlot.PubAccount;
                            secondItemSlot.PubAccount = tmp;

                            //텍스트 표시
                            firstItemSlot.PubText.text = firstItemSlot.PubAccount.ToString();
                            secondItemSlot.PubText.text = secondItemSlot.PubAccount.ToString();

                            //이미지 교환
                            Sprite tmpicon;
                            tmpicon = firstItemSlot.icon.sprite;
                            firstItemSlot.icon.sprite = secondItemSlot.icon.sprite;
                            secondItemSlot.icon.sprite = tmpicon;
                        }

                        //장비일때
                        else if (firstItemSlot.PubItemCode > 1000 && firstItemSlot.PubItemCode < 2001 && secondItemSlot.gameObject.name.Contains("equip_mainweapon")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubMainWeapon = secondItemSlot.PubItemCode; //코드넘겨받기
                            PlayerMng.Instance.setPlayerDmgStat();//장비를 장착했을 때 장착한 아이템만큼 자신의 공격력 수치가 변해야함
                        }
                        else if (firstItemSlot.PubItemCode > 2000 && firstItemSlot.PubItemCode < 3001 && secondItemSlot.gameObject.name.Contains("equip_subWaepon")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubSubWeapon = secondItemSlot.PubItemCode;
                            PlayerMng.Instance.setPlayerDmgStat();
                        }
                        else if (firstItemSlot.PubItemCode > 3000 && firstItemSlot.PubItemCode < 3201 && secondItemSlot.gameObject.name.Contains("equip_head")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubHead = secondItemSlot.PubItemCode;
                            PlayerMng.Instance.setPlayerDefenseStat();
                        }
                        else if (firstItemSlot.PubItemCode > 3200 && firstItemSlot.PubItemCode < 3401 && secondItemSlot.gameObject.name.Contains("equip_bodyUp")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubBodyUp = secondItemSlot.PubItemCode;
                            PlayerMng.Instance.setPlayerDefenseStat();

                        }
                        else if (firstItemSlot.PubItemCode > 3400 && firstItemSlot.PubItemCode < 3601 && secondItemSlot.gameObject.name.Contains("equip_bodydown")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubBodyDown = secondItemSlot.PubItemCode;
                            PlayerMng.Instance.setPlayerDefenseStat();
 
                        }
                        else if (firstItemSlot.PubItemCode > 3600 && firstItemSlot.PubItemCode < 3801 && secondItemSlot.gameObject.name.Contains("equip_shoes")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubShoes = secondItemSlot.PubItemCode;
                            PlayerMng.Instance.setPlayerDefenseStat();
     
                        }
                        else if (firstItemSlot.PubItemCode > 4000 && firstItemSlot.PubItemCode < 4501 && secondItemSlot.gameObject.name.Contains("equip_ring")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubRing = secondItemSlot.PubItemCode;
                            PlayerMng.Instance.setPlayerDefenseStat();
 
                        }
                        else if (firstItemSlot.PubItemCode > 4500 && firstItemSlot.PubItemCode < 5000 && secondItemSlot.gameObject.name.Contains("equip_necklace")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubNecklace = secondItemSlot.PubItemCode;
                            PlayerMng.Instance.setPlayerDefenseStat();
                        }
                        else{
                            return;
                        }
                        firstItemSlot = null;
                    }
                }
            }
    }

    public void removeSlot(ItemSlotScript _slot){
        _slot.icon.sprite = Resources.Load<Sprite>("White");
        _slot.PubAccount = 0;
        _slot.PubItemCode = 0;
    }

    // 인벤토리 -> 장비탭으로 갈때 한개만갈지 아니면 1개가 통째로 들어갈지 결정
    private void fillequipSlot(ItemSlotScript first, ItemSlotScript second){
        //코드 교환
        int temp = first.PubItemCode;
        first.PubItemCode = second.PubItemCode;
        second.PubItemCode = temp;

        //이미지 교환 (1개라면 교환), 1보다많다면 복사
        if (first.PubAccount == 1){
            Sprite tmpicon;
            tmpicon = first.icon.sprite;
            first.icon.sprite = second.icon.sprite;
            second.icon.sprite = tmpicon;

            //투명도 변경
            Color color = second.icon.color;
            color.a = 1.0f;
            second.icon.color = color;
        }
        else{//복사
            //투명도변경
            second.icon.sprite = first.icon.sprite;
            Color color = second.icon.color;
            color.a = 1.0f;
            second.icon.color = color;
        }
        //갯수 변경 (1개만 빼야함) 및 다시 텍스트 수정
        first.PubAccount--; //한개 빼기
        first.PubText.text = first.PubAccount.ToString();
    }

    //마우스포인터가 있는 위치를 레이케스트로 쏘고 리스트에 담고 리스트에서 테그가 같은 것을 골라 알려줌
    public RaycastResult findRaycastObject(PointerEventData pointEvent, string tag_name){
        pointEvent.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>(); //레이케스트 리스트

        EventSystem.current.RaycastAll(pointEvent, raycastResults); //레이케스트로 선택된친구를 리스트에 반환
        return  raycastResults.Find(x => x.gameObject.tag == tag_name);//리스트 중에서 tag가 ItemSlot인것을 찾음
    }

    //저장될 Jason 형태의 리스트를 프린트함(확인용)
    public void printInventroy(List<SaveForm> _ListSlot){
        foreach (SaveForm a in _ListSlot) {
            //Debug.Log(a.SAccount);
        }
    }
    #endregion

    private void addbuttonAction(){
        btn_Exit.onClick.AddListener(playerMng.SwichingActive); //playctrl에 있는 함수를 등록
        PlayerMng.Instance.Can_Attack = true;
    }


 

}

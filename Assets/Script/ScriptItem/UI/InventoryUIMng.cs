using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// 인벤토리에있는 UI를 관리 (보여지는 부분) inventory와 상호작용하는 스크립트
// - 헤더 잡고이동
// - x 버튼을 눌러 닫기
// 


public class InventoryUIMng : MonoBehaviour
{
    public static InventoryUIMng Instance;

    //실제적인 인벤토리공간 이걸 capacity 만큼만 운용되게 만들어야해
    public List<ItemSlotScript> ListSlot = new List<ItemSlotScript>();
    private EventSystem m_eventSystem;
    //

    ItemSlotScript firstItemSlot = null;
    ItemSlotScript secondItemSlot = null;
    public RectTransform DragImage = new RectTransform();

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

    [SerializeField] private int capacity = 20; //인벤토리 칸 추후늘리면됨
    private int invencount = 0;
    
    
    [SerializeField] private GameObject itemSlot;

    // 산하에 capacity
    private void Start()
    {
        DontDestroyOnLoad(this);

        initInventory(); //인벤토리 생성
        
    }

    private void Update()
    {
        slotMove();
    }

    //아이템코드가 있는지 찾기 : 물건검색
    public DataWeaponItem findWeaponitemcode(int _itemCode){
        DataWeaponItem data = ItemMng.Instance.WeaponList.Find(x => x.itemCode == _itemCode);
        return data;
    }
    public DataArmorItem findArmoritemcode(int _itemCode)
    {
        DataArmorItem data = ItemMng.Instance.ArmorList.Find(x => x.itemCode == _itemCode);
        return data;
    }


    //초기 capacity만큼 인벤토리칸을 생성해주는 코드 
    private void initInventory()
    {
        for (int i = 0; i < capacity; i++)
        {
            createItemSlot("itemSlot");
        }
    }


    //슬롯생성하는 코드
    private void createItemSlot(string _name = "itemSlot")
    {
        if(invencount < capacity) // 슬롯보다 더 많은 양이 생성되지 않도록
        {
            GameObject obj = Instantiate(itemSlot, transform);
            ListSlot.Add(obj.GetComponent<ItemSlotScript>()); //listSlot에 등록해서 아이템스크립트들을 등록했음
            obj.name = _name+ invencount;
            invencount++;
        }
    }

    //슬롯에다가 아이템의 정보를 넘겨줘야함 
    public void giveDataItemSlot(int _itemCode)
    {
        DataWeaponItem data = findWeaponitemcode(_itemCode); // 오브젝트찾음 
        
        //List에서 아이템코드가 같은게 있는지 item을 찾음
        ItemSlotScript item = ListSlot.Find(x => x.PubItemCode == _itemCode);
        if (item != null) //item이 이미 있다면. 갯수를 증가시킴
        {
            item.PubAccount++;
            item.PubText.text = item.PubAccount.ToString();
        }
        else //만약 item이 없다면
        {
            bool findit = false; //못찾음
            int count = ListSlot.Count; //리스트의 개수
            for (int i = 0; i < count; ++i)
            {
                item = ListSlot[i]; 
                if (item.PubItemCode == 0) //코드가 0이라면
                {
                    item.PubItemCode = _itemCode;
                    item.PubAccount = 1;
                    item.PubText.text = item.PubAccount.ToString();
                    // 인벤토리 아이콘과 수량표시를 변경해야함
                    item.changeImage(data.icon);
                    findit = true;
                    break;
                }
            }

            if (findit == false)
            {
                //아이템 창이 가득 찼다고 알리고 아이템을 먹지안도록해야함
            }
        }
    }

    //레이케스트를 쏨 -> 슬롯을 선택 슬롯에 대한 스크립트를 가져옴 -> 선택된슬롯이라는 곳에 잠시저장. 이후선택된애를 교환
    private void slotMove(){
        
        PointerEventData pointEvent = new PointerEventData(m_eventSystem);
        
        //첫번째 슬롯
        if (Input.GetMouseButtonDown(0)){
            pointEvent.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>(); //레이케스트 리스트

            EventSystem.current.RaycastAll(pointEvent, raycastResults); //레이케스트로 선택된친구를 리스트에 반환
            RaycastResult result = raycastResults.Find(x => x.gameObject.tag == "ItemSlot");//리스트 중에서 tag가 ItemSlot인것을 찾음

            if (result.gameObject == null) {//예외처리
                return;
            }
            else{
                Debug.Log("첫번째 슬롯 :" + result);
                firstItemSlot = result.gameObject.GetComponent<ItemSlotScript>();
            }

            // 마우스 donw => 이동 아이템 시작위치 선택
            // 드래그 중 => 아이콘을 붙들고있게하고
            // 마우스 up => 도착위치 선택 , 시작위치와 도착위치에 있는 슬롯의 정보를 서로 바꿈(바꿔야 할 것 : 아이템code, 아이템 account, 아이템icon)
        }

        if (Input.GetMouseButton(0)){ //마우스를 누르고있으면 이미지가 계속따라오는 함수
            if (firstItemSlot != null) {
                DragImage.gameObject.SetActive(true);
                DragImage.position = Input.mousePosition;
                Image DI = DragImage.GetComponent<Image>();
                DI.sprite = firstItemSlot.icon.sprite;
            }
        }



        //두번째 슬롯
        if (Input.GetMouseButtonUp(0)){
            DragImage.gameObject.SetActive(false);
            pointEvent.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>(); //레이케스트 리스트

            EventSystem.current.RaycastAll(pointEvent, raycastResults); //레이케스트로 선택된친구를 리스트에 반환
            RaycastResult result = raycastResults.Find(x => x.gameObject.tag == "ItemSlot");//리스트 중에서 tag가 ItemSlot인것을 찾음

            if (result.gameObject == null)
            {//예외처리
                firstItemSlot = null;
                return;
            }
            else
            {
                Debug.Log("두번째 슬롯 : " + result);
                secondItemSlot = result.gameObject.GetComponent<ItemSlotScript>(); // 스크립트 가져왔음
                //스크립트 교환

                if ((firstItemSlot != secondItemSlot))
                {
                    //코드 교환
                    int temp = firstItemSlot.PubItemCode;
                    firstItemSlot.PubItemCode = secondItemSlot.PubItemCode;
                    secondItemSlot.PubItemCode = temp;

                    //갯수 교환
                    int tmp = firstItemSlot.PubAccount;
                    firstItemSlot.PubAccount = secondItemSlot.PubAccount;
                    secondItemSlot.PubAccount = tmp;

                    //텍스트 교환
                    firstItemSlot.PubText.text = firstItemSlot.PubAccount.ToString();
                    secondItemSlot.PubText.text = secondItemSlot.PubAccount.ToString();

                    //이미지 교환
                    Sprite tmpicon;
                    tmpicon = firstItemSlot.icon.sprite;
                    firstItemSlot.icon.sprite = secondItemSlot.icon.sprite;
                    secondItemSlot.icon.sprite = tmpicon;

                    firstItemSlot = null;
                }
                
            }
            
        }

    }
    //집에가서 이거 찾아볼것
    //public void OnDrag(PointerEventData eventData)
    //{
    //    ((IDragHandler)Instance).OnDrag(eventData);
    //}
}

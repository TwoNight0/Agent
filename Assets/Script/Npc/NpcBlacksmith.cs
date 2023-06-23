using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NpcBlacksmith : MonoBehaviour{
    private EventSystem m_eventSystem;
    int[] itemarray = new int[3];
    ItemSlotScript firstItemSlot = null;
    ItemSlotScript secondItemSlot = null;

    [SerializeField] public ItemSlotScript slot1;
    [SerializeField] public ItemSlotScript slot2;
    [SerializeField] public ItemSlotScript slot3;



    [SerializeField] private ItemSlotScript slot_result;

    private int tmpCode;
    private int count;
    
    // Start is called before the first frame update
    void Start(){
        count = 0;
    }

    // Update is called once per frame
    void Update(){
        inputItem();
        mixItem();
    }

    /// <summary>
    /// 슬롯을 인식해서 데이터 어레이에 넣음
    /// 
    /// 
    /// </summary>


    private void inputItem(){
        PointerEventData pointEvent = new PointerEventData(m_eventSystem);

        // 첫번째 슬롯 선택
        if (Input.GetMouseButtonDown(0)){ 
            RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");
            if (result.gameObject == null){//예외처리
                return;
            }
            else{
                Debug.Log("첫번째 슬롯 :" + result);
                firstItemSlot = result.gameObject.GetComponent<ItemSlotScript>();
            }
        }

        //두번째 슬롯 선택
        if (Input.GetMouseButtonUp(0)){
            //DragImage.gameObject.SetActive(false);
            RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");

            if (result.gameObject == null){//예외처리
                firstItemSlot = null;
                return;
            }
            else{
                Debug.Log("두번째 슬롯 : " + result);
                secondItemSlot = result.gameObject.GetComponent<ItemSlotScript>(); // 스크립트 가져왔음


                if ((firstItemSlot != secondItemSlot)){ //서로 다른아이템일때
                    //아이템슬롯의 아이템일때
                    if (secondItemSlot.gameObject.name.Contains("ItemSlot") && secondItemSlot.gameObject.layer == 7){

                        if (count <= 2){
                            itemarray[count] = firstItemSlot.PubItemCode;
                            Debug.Log(count);
                            count++;
                        }
                        

                    }
                    
                    //firstItemSlot = null;
                }
            }
        }
    }

    /// <summary>
    /// 아이템 조합
    /// </summary>
    private void mixItem(){
        if(count == 3){
            System.Array.Sort(itemarray);
            //Debug.Log(itemarray[0]);
            //Debug.Log(itemarray[1]);
            //Debug.Log(itemarray[2]);
            if (itemarray[0] == 1001 && itemarray[1] == 1501 && itemarray[2] == 3201) {
                //Debug.Log("합체!");
                //채워넣기
                
                //넣은 데이터 초기화
                InventoryUIMng.Instance.removeSlot(slot1);
                InventoryUIMng.Instance.removeSlot(slot2);
                InventoryUIMng.Instance.removeSlot(slot3);
                slot_result.PubItemCode = 3203;
                slot_result.PubAccount = 1;
                slot_result.PubText.text = slot_result.PubAccount.ToString();
                (DataWeaponItem,  DataArmorItem) temp = InventoryUIMng.Instance.findObjData(3203);
                slot_result.icon.sprite = temp.Item2.icon;

                count = -99;
            }


        }
    }

    

    /// <summary>
    /// 레이캐스트를 쏘고 태그를 찾음
    /// </summary>
    /// <param name="pointEvent"></param>
    /// <param name="tag_name"></param>
    /// <returns></returns>
    public RaycastResult findRaycastObject(PointerEventData pointEvent, string tag_name){
        pointEvent.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>(); //레이케스트 리스트

        EventSystem.current.RaycastAll(pointEvent, raycastResults); //레이케스트로 선택된친구를 리스트에 반환
        return raycastResults.Find(x => x.gameObject.tag == tag_name);//리스트 중에서 tag가 ItemSlot인것을 찾음
    }



}

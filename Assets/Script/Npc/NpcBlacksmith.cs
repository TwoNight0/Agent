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
    /// ������ �ν��ؼ� ������ ��̿� ����
    /// 
    /// 
    /// </summary>


    private void inputItem(){
        PointerEventData pointEvent = new PointerEventData(m_eventSystem);

        // ù��° ���� ����
        if (Input.GetMouseButtonDown(0)){ 
            RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");
            if (result.gameObject == null){//����ó��
                return;
            }
            else{
                Debug.Log("ù��° ���� :" + result);
                firstItemSlot = result.gameObject.GetComponent<ItemSlotScript>();
            }
        }

        //�ι�° ���� ����
        if (Input.GetMouseButtonUp(0)){
            //DragImage.gameObject.SetActive(false);
            RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");

            if (result.gameObject == null){//����ó��
                firstItemSlot = null;
                return;
            }
            else{
                Debug.Log("�ι�° ���� : " + result);
                secondItemSlot = result.gameObject.GetComponent<ItemSlotScript>(); // ��ũ��Ʈ ��������


                if ((firstItemSlot != secondItemSlot)){ //���� �ٸ��������϶�
                    //�����۽����� �������϶�
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
    /// ������ ����
    /// </summary>
    private void mixItem(){
        if(count == 3){
            System.Array.Sort(itemarray);
            //Debug.Log(itemarray[0]);
            //Debug.Log(itemarray[1]);
            //Debug.Log(itemarray[2]);
            if (itemarray[0] == 1001 && itemarray[1] == 1501 && itemarray[2] == 3201) {
                //Debug.Log("��ü!");
                //ä���ֱ�
                
                //���� ������ �ʱ�ȭ
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
    /// ����ĳ��Ʈ�� ��� �±׸� ã��
    /// </summary>
    /// <param name="pointEvent"></param>
    /// <param name="tag_name"></param>
    /// <returns></returns>
    public RaycastResult findRaycastObject(PointerEventData pointEvent, string tag_name){
        pointEvent.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>(); //�����ɽ�Ʈ ����Ʈ

        EventSystem.current.RaycastAll(pointEvent, raycastResults); //�����ɽ�Ʈ�� ���õ�ģ���� ����Ʈ�� ��ȯ
        return raycastResults.Find(x => x.gameObject.tag == tag_name);//����Ʈ �߿��� tag�� ItemSlot�ΰ��� ã��
    }



}

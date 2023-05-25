using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// �κ��丮���ִ� UI�� ���� (�������� �κ�) inventory�� ��ȣ�ۿ��ϴ� ��ũ��Ʈ
// - ��� ����̵�
// - x ��ư�� ���� �ݱ�
// 


public class InventoryUIMng : MonoBehaviour
{
    public static InventoryUIMng Instance;

    //�������� �κ��丮���� �̰� capacity ��ŭ�� ���ǰ� ��������
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

    [SerializeField] private int capacity = 20; //�κ��丮 ĭ ���Ĵø����
    private int invencount = 0;
    
    
    [SerializeField] private GameObject itemSlot;

    // ���Ͽ� capacity
    private void Start()
    {
        DontDestroyOnLoad(this);

        initInventory(); //�κ��丮 ����
        
    }

    private void Update()
    {
        slotMove();
    }

    //�������ڵ尡 �ִ��� ã�� : ���ǰ˻�
    //public DataWeaponItem findWeaponitemcode(int _itemCode)
    //{
        
    //    return data;
    //}

    //public DataArmorItem findArmoritemcode(int _itemCode)
    //{
        
    //    return data;
    //}
    // ������ ��з� 
    // 
    /// <summary>
    /// ������ �ڵ� ����
    /// 0~1000      ���
    /// 1001~2000   ���� ���� : ����(1001~1500), ���Ÿ�(1501~2000)  
    /// 2001~3000   ���� ���� : ����(2001~2500), ���Ÿ�(2501~3000)
    /// 3001~4000   �� : �Ӹ�(3001~3200), �ٵ��(3201~3400), �ٵ�ٿ�(3401~3600), �Ź�(3601~3800)
    /// 4001~5000   ��ű� : ����(4001~4500), �����(4501~5000)
    /// 6001~7000   ���� // ���������� ���߿� ��������
    /// </summary>
    /// 
    //public (DataWeaponItem weapondata, DataArmorItem armordata) findObjData(int _itemCode)
    //{
    //    DataWeaponItem weapondata;
    //    DataArmorItem armordata;
    //    if (_itemCode > 0 && _itemCode < 1001) {//���
    //                                            //��Ḯ��Ʈ �����
    //    }
    //    else if (_itemCode > 1000 && _itemCode < 3001) //���ι���
    //    {
    //        weapondata = ItemMng.Instance.WeaponList.Find(x => x.itemCode == _itemCode);

    //    }
    //    else if (_itemCode > 3000 && _itemCode < 4000) //��
    //    {
    //        armordata = ItemMng.Instance.ArmorList.Find(x => x.itemCode == _itemCode);
    //    }
    //    return (weapondata, armordata);

    //}
    //������
    //public T covertClass<T>(T classname){
        
    //    return T data;
    //}


    //�ʱ� capacity��ŭ �κ��丮ĭ�� �������ִ� �ڵ� 
    private void initInventory()
    {
        for (int i = 0; i < capacity; i++)
        {
            createItemSlot("ItemSlot");
        }
    }


    //���Ի����ϴ� �ڵ�
    private void createItemSlot(string _name = "itemSlot")
    {
        if(invencount < capacity) // ���Ժ��� �� ���� ���� �������� �ʵ���
        {
            GameObject obj = Instantiate(itemSlot, transform);
            ListSlot.Add(obj.GetComponent<ItemSlotScript>()); //listSlot�� ����ؼ� �����۽�ũ��Ʈ���� �������
            obj.name = _name+ invencount;
            invencount++;
        }
    }

    //���Կ��ٰ� �������� ������ �Ѱ������ 
    public void giveDataItemSlot(int _itemCode)
    {
        DataWeaponItem data = findWeaponitemcode(_itemCode); // ������Ʈã�� 
        
        //List���� �������ڵ尡 ������ �ִ��� item�� ã��
        ItemSlotScript item = ListSlot.Find(x => x.PubItemCode == _itemCode);
        if (item != null) //item�� �̹� �ִٸ�. ������ ������Ŵ
        {
            item.PubAccount++;
            item.PubText.text = item.PubAccount.ToString();
        }
        else //���� item�� ���ٸ�
        {
            bool findit = false; //��ã��
            int count = ListSlot.Count; //����Ʈ�� ����
            for (int i = 0; i < count; ++i)
            {
                item = ListSlot[i]; 
                if (item.PubItemCode == 0) //�ڵ尡 0�̶��
                {
                    item.PubItemCode = _itemCode;
                    item.PubAccount = 1;
                    item.PubText.text = item.PubAccount.ToString();
                    // �κ��丮 �����ܰ� ����ǥ�ø� �����ؾ���
                    item.changeImage(data.icon);
                    findit = true;
                    break;
                }
            }

            if (findit == false)
            {
                //������ â�� ���� á�ٰ� �˸��� �������� �����ȵ����ؾ���
            }
        }
    }

    //�����ɽ�Ʈ�� �� -> ������ ���� ���Կ� ���� ��ũ��Ʈ�� ������ -> ���õȽ����̶�� ���� �������. ���ļ��õȾָ� ��ȯ
    private void slotMove(){
        
        PointerEventData pointEvent = new PointerEventData(m_eventSystem);
        
        //ù��° ����
        if (Input.GetMouseButtonDown(0)){
            RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");//����Ʈ �߿��� tag�� ItemSlot�ΰ��� ã��
            if (result.gameObject == null) {//����ó��
                return;
            }
            else{
                Debug.Log("ù��° ���� :" + result);
                firstItemSlot = result.gameObject.GetComponent<ItemSlotScript>();
            }
        }

        if (Input.GetMouseButton(0)){ //���콺�� ������������ �̹����� ��ӵ������ �Լ�
            if (firstItemSlot != null) {
                DragImage.gameObject.SetActive(true);
                DragImage.position = Input.mousePosition;
                Image DI = DragImage.GetComponent<Image>();
                DI.sprite = firstItemSlot.icon.sprite;
            }
        }

        //�ι�° ����
        if (Input.GetMouseButtonUp(0)){
            DragImage.gameObject.SetActive(false);

            RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");

            if (result.gameObject == null)
            {//����ó��
                firstItemSlot = null;
                return;
            }
            else
            {
                Debug.Log("�ι�° ���� : " + result);
                secondItemSlot = result.gameObject.GetComponent<ItemSlotScript>(); // ��ũ��Ʈ ��������
                //��ũ��Ʈ ��ȯ

                if ((firstItemSlot != secondItemSlot))
                {
                    //�����۽����� �������϶�
                    if (secondItemSlot.gameObject.name.Contains("ItemSlot"))
                    {
                        //�ڵ� ��ȯ
                        int temp = firstItemSlot.PubItemCode;
                        firstItemSlot.PubItemCode = secondItemSlot.PubItemCode;
                        secondItemSlot.PubItemCode = temp;

                        //���� ��ȯ
                        int tmp = firstItemSlot.PubAccount;
                        firstItemSlot.PubAccount = secondItemSlot.PubAccount;
                        secondItemSlot.PubAccount = tmp;

                        //�ؽ�Ʈ ��ȯ
                        firstItemSlot.PubText.text = firstItemSlot.PubAccount.ToString();
                        secondItemSlot.PubText.text = secondItemSlot.PubAccount.ToString();

                        //�̹��� ��ȯ
                        Sprite tmpicon;
                        tmpicon = firstItemSlot.icon.sprite;
                        firstItemSlot.icon.sprite = secondItemSlot.icon.sprite;
                        secondItemSlot.icon.sprite = tmpicon;

                        
                    }

                    //����϶�
                    if (secondItemSlot.gameObject.name.Contains("equip"))
                    {
                        //�ڵ� ��ȯ
                        int temp = firstItemSlot.PubItemCode;
                        firstItemSlot.PubItemCode = secondItemSlot.PubItemCode;
                        secondItemSlot.PubItemCode = temp;

                        //�̹��� ��ȯ (1����� ��ȯ), 1���ٸ��ٸ� ����
                        if(firstItemSlot.PubAccount == 1)
                        {
                            Sprite tmpicon;
                            tmpicon = firstItemSlot.icon.sprite;
                            firstItemSlot.icon.sprite = secondItemSlot.icon.sprite;
                            secondItemSlot.icon.sprite = tmpicon;
                        }
                        else //����
                        {
                            secondItemSlot.icon.sprite = firstItemSlot.icon.sprite;
                        }

                        //���� ���� (1���� ������) �� �ٽ� �ؽ�Ʈ ����
                        firstItemSlot.PubAccount--;
                        firstItemSlot.PubText.text = firstItemSlot.PubAccount.ToString();
                    }

                    firstItemSlot = null;
                }
                
            }
            
        }

    }
    //�������� �̰� ã�ƺ���
    //public void OnDrag(PointerEventData eventData)
    //{
    //    ((IDragHandler)Instance).OnDrag(eventData);
    //}

    
    //���콺�����Ͱ� �ִ� ��ġ�� �����ɽ�Ʈ�� ��� ����Ʈ�� ��� ����Ʈ���� �ױװ� ���� ���� ��� �˷���
    public RaycastResult findRaycastObject(PointerEventData pointEvent, string tag_name)
    {
        pointEvent.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>(); //�����ɽ�Ʈ ����Ʈ

        EventSystem.current.RaycastAll(pointEvent, raycastResults); //�����ɽ�Ʈ�� ���õ�ģ���� ����Ʈ�� ��ȯ
        return  raycastResults.Find(x => x.gameObject.tag == tag_name);//����Ʈ �߿��� tag�� ItemSlot�ΰ��� ã��
    }
}

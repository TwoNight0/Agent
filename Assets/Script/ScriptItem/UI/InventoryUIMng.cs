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
    public DataWeaponItem findWeaponitemcode(int _itemCode){
        DataWeaponItem data = ItemMng.Instance.WeaponList.Find(x => x.itemCode == _itemCode);
        return data;
    }
    public DataArmorItem findArmoritemcode(int _itemCode)
    {
        DataArmorItem data = ItemMng.Instance.ArmorList.Find(x => x.itemCode == _itemCode);
        return data;
    }


    //�ʱ� capacity��ŭ �κ��丮ĭ�� �������ִ� �ڵ� 
    private void initInventory()
    {
        for (int i = 0; i < capacity; i++)
        {
            createItemSlot("itemSlot");
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
            pointEvent.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>(); //�����ɽ�Ʈ ����Ʈ

            EventSystem.current.RaycastAll(pointEvent, raycastResults); //�����ɽ�Ʈ�� ���õ�ģ���� ����Ʈ�� ��ȯ
            RaycastResult result = raycastResults.Find(x => x.gameObject.tag == "ItemSlot");//����Ʈ �߿��� tag�� ItemSlot�ΰ��� ã��

            if (result.gameObject == null) {//����ó��
                return;
            }
            else{
                Debug.Log("ù��° ���� :" + result);
                firstItemSlot = result.gameObject.GetComponent<ItemSlotScript>();
            }

            // ���콺 donw => �̵� ������ ������ġ ����
            // �巡�� �� => �������� �ٵ���ְ��ϰ�
            // ���콺 up => ������ġ ���� , ������ġ�� ������ġ�� �ִ� ������ ������ ���� �ٲ�(�ٲ�� �� �� : ������code, ������ account, ������icon)
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
            pointEvent.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>(); //�����ɽ�Ʈ ����Ʈ

            EventSystem.current.RaycastAll(pointEvent, raycastResults); //�����ɽ�Ʈ�� ���õ�ģ���� ����Ʈ�� ��ȯ
            RaycastResult result = raycastResults.Find(x => x.gameObject.tag == "ItemSlot");//����Ʈ �߿��� tag�� ItemSlot�ΰ��� ã��

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
}

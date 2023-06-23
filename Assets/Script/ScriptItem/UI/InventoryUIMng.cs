using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Ŭ���� ���� : �κ��丮���ִ� UI�� ����
// (�������� �κ�) inventory�� ��ȣ�ۿ��ϴ� ��ũ��Ʈ
public class InventoryUIMng : MonoBehaviour{
    public static InventoryUIMng Instance;

    //�������� �κ��丮���� �̰� capacity ��ŭ�� ���ǰ� ��������
    public List<ItemSlotScript> ListSlot = new List<ItemSlotScript>(); //�κ��丮���� ����Ʈ
    //public List<SaveForm> saveList = new List<SaveForm>();
    private EventSystem m_eventSystem;
    PlayerMng playerMng;
 
    //

    //���� ���ý� ����ϴ� ������
    ItemSlotScript firstItemSlot = null;
    ItemSlotScript secondItemSlot = null;
    
    
    public RectTransform DragImage = new RectTransform();
    [Header("���� ��")]
    [SerializeField] private int capacity = 20; //�κ��丮 ĭ ���Ĵø����
    private int invencount = 0;

    [SerializeField] private GameObject itemSlot;

    #region (Button)(inven,stat,skill,Exit)
    //��ư
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

    // ���Ͽ� capacity
    private void Start(){
        DontDestroyOnLoad(this);

        playerMng = FindObjectOfType<PlayerMng>();
        initInventory(); //�κ��丮 ����
        addbuttonAction(); //��ư�׼� ����
        
    }

    private void Update(){
        slotMove();
        
    }

    // ������ ��з� 
    /// 0~1000      ���
    /// 1001~2000   ���� ���� : ����(1001~1500), ���Ÿ�(1501~2000)  
    /// 2001~3000   ���� ���� : ����(2001~2500), ���Ÿ�(2501~3000)
    /// 3001~4000   �� : �Ӹ�(3001~3200), �ٵ��(3201~3400), �ٵ�ٿ�(3401~3600), �Ź�(3601~3800)
    /// 4001~5000   ��ű� : ����(4001~4500), �����(4501~5000)
    /// 6001~7000   ���� // ���������� ���߿� ��������

    #region Init
    //�ʱ� capacity��ŭ �κ��丮ĭ�� �������ִ� �ڵ� 
    private void initInventory(){
        for (int i = 0; i < capacity; i++){
            createItemSlot("ItemSlot");
        }
    }
    #endregion

    #region Slot����
    //������ ã�Ƽ� �ڷ����� �°� ��ȯ
    public (DataWeaponItem weapondata, DataArmorItem armordata) findObjData(int _itemCode){
        DataWeaponItem weapondata = null;
        DataArmorItem armordata = null;
        if (_itemCode > 0 && _itemCode < 1001){//���
         //��Ḯ��Ʈ �����
        }
        else if (_itemCode > 1000 && _itemCode < 3001){//���ι���
            weapondata = ItemMng.Instance.WeaponList.Find(x => x.itemCode == _itemCode);
        }
        else if (_itemCode > 3000 && _itemCode < 4000){//��
            armordata = ItemMng.Instance.ArmorList.Find(x => x.itemCode == _itemCode);
        }
        return (weapondata, armordata);
    }

    //���Ի����ϴ� �ڵ�
    private void createItemSlot(string _name = "itemSlot"){
        if(invencount < capacity){// ���Ժ��� �� ���� ���� �������� �ʵ���
            GameObject obj = Instantiate(itemSlot, transform);
            ListSlot.Add(obj.GetComponent<ItemSlotScript>()); //listSlot�� ����ؼ� �����۽�ũ��Ʈ���� �������
            //saveList.Add(obj.GetComponent<SaveForm>()); //����ɸ���Ʈ�� ���� ������ ���� 
            obj.name = _name+ invencount;

            invencount++;
        }
    }

    //���Կ��ٰ� �������� ������ �Ѱ��� (�̰͵� �������� ã�� �Ͱ� �ִ°����� �и��߾���ߴ� 
    public void giveDataItemSlot(int _itemCode){
        (DataWeaponItem, DataArmorItem) data = findObjData(_itemCode); // ���� ����Ʈ�� �߿��� ���ϴ� ������Ʈã�� 
        
        //List���� �������ڵ尡 ������ �ִ��� item�� ã��
        ItemSlotScript item = ListSlot.Find(x => x.PubItemCode == _itemCode);
        if (item != null){//item�� �̹� �ִٸ�. ������ ������Ŵ
            item.PubAccount++;
            item.PubText.text = item.PubAccount.ToString();
            List<SaveForm> saveList = ItemMng.Instance.ListCopy(ListSlot);
            ItemMng.Instance.inventorysave(saveList);
        }
        else{//���� item�� ���ٸ�
            bool findit = false; //��ã��
            int count = ListSlot.Count; //����Ʈ�� ����
            for (int i = 0; i < count; ++i){
                item = ListSlot[i]; 
                if (item.PubItemCode == 0){//�ڵ尡 0�̶��
                    item.PubItemCode = _itemCode;
                    item.PubAccount = 1;
                    item.PubText.text = item.PubAccount.ToString();
                    // �κ��丮 �����ܰ� ����ǥ�ø� �����ؾ���
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
                //TODO : ������ â�� ���� á�ٰ� �˸��� �������� �����ȵ����ؾ���
            }
        }
    }

    // �����ɽ�Ʈ�� �� -> ������ ���� ���Կ� ���� ��ũ��Ʈ�� ������ -> ���õȽ����̶�� ���� �������. ���ļ��õ� ������Ʈ�� ��ȯ
    private void slotMove(){     
            PointerEventData pointEvent = new PointerEventData(m_eventSystem);
            
            //ù��° ����
            if (Input.GetMouseButtonDown(0)){
                RaycastResult result = findRaycastObject(pointEvent, "ItemSlot");//����Ʈ �߿��� tag�� ItemSlot�ΰ��� ã��
                if (result.gameObject == null){//����ó��
                    return;
                }
                else{
                    Debug.Log("ù��° ���� :" + result);
                    firstItemSlot = result.gameObject.GetComponent<ItemSlotScript>();
                }
            }

            if (Input.GetMouseButton(0)){ //���콺�� ������������ �̹����� ��ӵ������ �Լ�
                if (firstItemSlot != null){
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

                if (result.gameObject == null){//����ó��
                    firstItemSlot = null;
                    return;
                }
                else{//null �ƴϸ�
                    Debug.Log("�ι�° ���� : " + result);
                    secondItemSlot = result.gameObject.GetComponent<ItemSlotScript>(); // ��ũ��Ʈ ��������
                    if ((firstItemSlot != secondItemSlot)){
                        //�����۽����� �������϶�
                        if (secondItemSlot.gameObject.name.Contains("ItemSlot")){
                            Debug.Log("Test");
                            //�ڵ� ��ȯ
                            int temp = firstItemSlot.PubItemCode;
                            firstItemSlot.PubItemCode = secondItemSlot.PubItemCode;
                            secondItemSlot.PubItemCode = temp;

                            //���� ��ȯ
                            int tmp = firstItemSlot.PubAccount;
                            firstItemSlot.PubAccount = secondItemSlot.PubAccount;
                            secondItemSlot.PubAccount = tmp;

                            //�ؽ�Ʈ ǥ��
                            firstItemSlot.PubText.text = firstItemSlot.PubAccount.ToString();
                            secondItemSlot.PubText.text = secondItemSlot.PubAccount.ToString();

                            //�̹��� ��ȯ
                            Sprite tmpicon;
                            tmpicon = firstItemSlot.icon.sprite;
                            firstItemSlot.icon.sprite = secondItemSlot.icon.sprite;
                            secondItemSlot.icon.sprite = tmpicon;
                        }

                        //����϶�
                        else if (firstItemSlot.PubItemCode > 1000 && firstItemSlot.PubItemCode < 2001 && secondItemSlot.gameObject.name.Contains("equip_mainweapon")){
                            fillequipSlot(firstItemSlot, secondItemSlot);
                            PlayerMng.Instance.PubMainWeapon = secondItemSlot.PubItemCode; //�ڵ�Ѱܹޱ�
                            PlayerMng.Instance.setPlayerDmgStat();//��� �������� �� ������ �����۸�ŭ �ڽ��� ���ݷ� ��ġ�� ���ؾ���
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

    // �κ��丮 -> ��������� ���� �Ѱ������� �ƴϸ� 1���� ��°�� ���� ����
    private void fillequipSlot(ItemSlotScript first, ItemSlotScript second){
        //�ڵ� ��ȯ
        int temp = first.PubItemCode;
        first.PubItemCode = second.PubItemCode;
        second.PubItemCode = temp;

        //�̹��� ��ȯ (1����� ��ȯ), 1���ٸ��ٸ� ����
        if (first.PubAccount == 1){
            Sprite tmpicon;
            tmpicon = first.icon.sprite;
            first.icon.sprite = second.icon.sprite;
            second.icon.sprite = tmpicon;

            //���� ����
            Color color = second.icon.color;
            color.a = 1.0f;
            second.icon.color = color;
        }
        else{//����
            //��������
            second.icon.sprite = first.icon.sprite;
            Color color = second.icon.color;
            color.a = 1.0f;
            second.icon.color = color;
        }
        //���� ���� (1���� ������) �� �ٽ� �ؽ�Ʈ ����
        first.PubAccount--; //�Ѱ� ����
        first.PubText.text = first.PubAccount.ToString();
    }

    //���콺�����Ͱ� �ִ� ��ġ�� �����ɽ�Ʈ�� ��� ����Ʈ�� ��� ����Ʈ���� �ױװ� ���� ���� ��� �˷���
    public RaycastResult findRaycastObject(PointerEventData pointEvent, string tag_name){
        pointEvent.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>(); //�����ɽ�Ʈ ����Ʈ

        EventSystem.current.RaycastAll(pointEvent, raycastResults); //�����ɽ�Ʈ�� ���õ�ģ���� ����Ʈ�� ��ȯ
        return  raycastResults.Find(x => x.gameObject.tag == tag_name);//����Ʈ �߿��� tag�� ItemSlot�ΰ��� ã��
    }

    //����� Jason ������ ����Ʈ�� ����Ʈ��(Ȯ�ο�)
    public void printInventroy(List<SaveForm> _ListSlot){
        foreach (SaveForm a in _ListSlot) {
            //Debug.Log(a.SAccount);
        }
    }
    #endregion

    private void addbuttonAction(){
        btn_Exit.onClick.AddListener(playerMng.SwichingActive); //playctrl�� �ִ� �Լ��� ���
        PlayerMng.Instance.Can_Attack = true;
    }


 

}

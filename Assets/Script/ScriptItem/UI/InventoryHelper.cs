using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//�� ��ũ��Ʈ�� �����۵����͸� �������ִ� �Ŵ��� Ŭ����
// ������ �ڵ�(int)�� ������� �����͸� ��������


public class InventoryHelper : MonoBehaviour
{
    public static InventoryHelper instance = null;

    //itemData : �������� ������ form
    [SerializeField] public List<ItemData> itemlist = new(); //�ܺ� itemdata�� ����Ʈ
    private ItemData item = new ItemData();

    // -- Item value --
    public string ItemName;
    public Sprite icon;
    public int amount; // ���� ������ �ִ� ������ ���� , max���� ���� �� ����
    public int maxamount; //��ø�� �� �ִ� ������ ������ �Ѱ� ex) ��øx, 10, 99
    public string description; //���콺�� ������ ���� �� ������ ������ ����
    // ----


    private static string SavePath;
    //System.IO.Path.Combine(Application.persistentDataPath, "actors.json"); // persistentDataPath unity save game path, actors.json name of the file containing actors 


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //SavePath = Application.persistentDataPath; //���̺� ���
        //Debug.Log(SavePath);
        inputdata();
        save();
    }

    private void save()
    {
        string key = "keyInven";

        item.itemName = "���";
        List<ItemData> list_data = new List<ItemData>(); //����Ʈ�� �����


        list_data.Add(item); //����Ʈ�� item�� �߰�(1, ������� ���)

        string value = JsonUtility.ToJson(new Serialization<ItemData>(list_data)); //string ������ json ���� ������
        PlayerPrefs.SetString(key, value);

        string json = JsonConvert.SerializeObject(item); //��ġ��

        List<ItemData> newlist = JsonConvert.DeserializeObject<List<ItemData>>(json); //�ٽ� ������ȭ�ؼ� �������� ���

    }

    private void inputdata()
    {

        string key = "keyInven";

        item.itemCode = 1;
        //item.itemTpye = ���
        item.itemName = "iron";
        //item.icon =  ���ҽ��� ������
        //item.itemobject = 
        item.maxAmount = 99;
        item.description = "sds";

        item.itemCode = 2;
        //item.itemTpye = ���
        item.itemName = "iron";
        //item.icon =  ���ҽ��� ������
        //item.itemobject = 
        item.maxAmount = 99;
        item.description = "sds";

    }


    /// <summary>
    /// 1. �κ��丮���� -> ĭ�� ���� ���������ؾ���
    /// 2. ������ ������, 
    /// 3. �����͸� �־��ִ�
    /// 
    /// 
    /// �巡�׾ص�� Ŭ���ϰ� ��涧 ������ ��ġ�� ���� ��ġ�� �´��� �ƴ϶�� �ۿ�������
    /// </summary>
    /// 



    //���̽������� �����ϴ� �޼���
    //private void CreateJsonFile(string createPath, string fileName, string jsonData)
    //{
    //    FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
    //    byte[] data = Encoding.UTF8.GetBytes(jsonData);
    //    fileStream.Write(data, 0, data.Length);
    //    fileStream.Close();
    //}

    //����� ���̽� ������ �ҷ����� �޼���
    //T LoadJsonFile<T>(string loadPath, string fileName)
    //{
    //    FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
    //    byte[] data = new byte[fileStream.Length];
    //    fileStream.Read(data, 0, data.Length);
    //    fileStream.Close();
    //    string jsonData = Encoding.UTF8.GetString(data);
    //    return JsonUtility.FromJson<T>(jsonData);
    //}

    private void load()
    {
        string key = "keyInven";

        string value = PlayerPrefs.GetString(key);
        ItemData item = JsonUtility.FromJson<ItemData>(value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // item ������ �������� �ڵ�
    public void getItemdata(ItemData _ItemData)
    {
        ItemName = _ItemData.itemName;
        icon = _ItemData.icon;

        maxamount = _ItemData.maxAmount;
        description = _ItemData.description;
    }

    public bool addItem(ItemData _itemToAdd)
    {
        for (int i = 0; i < itemlist.Count; i++)
        {
            if (itemlist[i] == null)
            {
                itemlist[i] = _itemToAdd;
                return true;
            }
        }
        Debug.Log("�κ��丮�� �������� �����ϴ�.");
        return false;
    }


    public void removeItem()
    {
        //items.Remove();
    }

    //�ڵ������� ����������
    public void saveItem()
    {

    }

    //item�� �ִ��� Ȯ��
    public void checkItem()
    {

    }

    //������ ����
    public void UpdateInventory()
    {
        //for(int i = 0; i< ; i++)
        //{

        //}

    }

    //���콺�� ȭ�� ������ ��������� �տ� ������ �޼���
    public void dropItem()
    {

    }
}

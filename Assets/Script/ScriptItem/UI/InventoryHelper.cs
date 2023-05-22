using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//이 스크립트는 아이템데이터를 가져와주는 매니저 클래스
// 아이템 코드(int)를 기반으로 데이터를 가져와줌


public class InventoryHelper : MonoBehaviour
{
    public static InventoryHelper instance = null;

    //itemData : 아이템의 데이터 form
    [SerializeField] public List<ItemData> itemlist = new(); //외부 itemdata의 리스트
    private ItemData item = new ItemData();

    // -- Item value --
    public string ItemName;
    public Sprite icon;
    public int amount; // 현재 가지고 있는 아이템 개수 , max값을 넘을 수 없음
    public int maxamount; //중첩될 수 있는 아이템 수량의 한계 ex) 중첩x, 10, 99
    public string description; //마우스를 가져다 댔을 때 나오는 아이템 정보
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
        //SavePath = Application.persistentDataPath; //세이브 경로
        //Debug.Log(SavePath);
        inputdata();
        save();
    }

    private void save()
    {
        string key = "keyInven";

        item.itemName = "사과";
        List<ItemData> list_data = new List<ItemData>(); //리스트를 만들고


        list_data.Add(item); //리스트에 item을 추가(1, 사과값이 담김)

        string value = JsonUtility.ToJson(new Serialization<ItemData>(list_data)); //string 값으로 json 값을 가져옴
        PlayerPrefs.SetString(key, value);

        string json = JsonConvert.SerializeObject(item); //패치됨

        List<ItemData> newlist = JsonConvert.DeserializeObject<List<ItemData>>(json); //다시 역직렬화해서 가져오는 기능

    }

    private void inputdata()
    {

        string key = "keyInven";

        item.itemCode = 1;
        //item.itemTpye = 재료
        item.itemName = "iron";
        //item.icon =  리소스의 아이콘
        //item.itemobject = 
        item.maxAmount = 99;
        item.description = "sds";

        item.itemCode = 2;
        //item.itemTpye = 재료
        item.itemName = "iron";
        //item.icon =  리소스의 아이콘
        //item.itemobject = 
        item.maxAmount = 99;
        item.description = "sds";

    }


    /// <summary>
    /// 1. 인벤토리구현 -> 칸당 직접 수정가능해야함
    /// 2. 데이터 얻어오고, 
    /// 3. 데이터를 넣어주는
    /// 
    /// 
    /// 드래그앤드랍 클릭하고 당길때 놨을때 위치가 놓을 위치가 맞는지 아니라면 밖에버리기
    /// </summary>
    /// 



    //제이슨파일을 저장하는 메서드
    //private void CreateJsonFile(string createPath, string fileName, string jsonData)
    //{
    //    FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
    //    byte[] data = Encoding.UTF8.GetBytes(jsonData);
    //    fileStream.Write(data, 0, data.Length);
    //    fileStream.Close();
    //}

    //저장된 제이슨 파일을 불러오는 메서드
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

    // item 정보를 가져오는 코드
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
        Debug.Log("인벤토리에 아이템이 없습니다.");
        return false;
    }


    public void removeItem()
    {
        //items.Remove();
    }

    //자동적으로 아이템저장
    public void saveItem()
    {

    }

    //item이 있는지 확인
    public void checkItem()
    {

    }

    //아이템 갱신
    public void UpdateInventory()
    {
        //for(int i = 0; i< ; i++)
        //{

        //}

    }

    //마우스를 화면 밖으로 끌어냈을때 앞에 버리는 메서드
    public void dropItem()
    {

    }
}

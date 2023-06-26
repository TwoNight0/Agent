using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngDisplay : MonoBehaviour{
    static public MngDisplay Instance;

    [SerializeField] public GameObject UI_Itemshop;
    [SerializeField] public GameObject UI_forge;
    [SerializeField] public GameObject UI_chestBox;
    [SerializeField] public GameObject UI_setting;

    public bool isforge;
    public bool isItemShop;
    public bool ischest;
    public bool issetting;

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }

    private void Start(){
        DontDestroyOnLoad(this);
        initComponent();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update(){
        Setting();
    }

    /// <summary>
    /// √ ±‚»≠
    /// </summary>
    private void initComponent(){
        UI_forge.SetActive(false);
        UI_Itemshop.SetActive(false);
        UI_chestBox.SetActive(false);
        UI_setting.SetActive(false);
        isforge = false;
        ischest = false;
        issetting = false;
        isItemShop = false;
    }


    /// <summary>
    /// ¥Î¿Â∞£ √¢ ¥›±‚
    /// </summary>
    public void exitForge(){
        if (isforge)
        {
            isforge = false;
            UI_forge.SetActive(false);
            PlayerMng.Instance.Can_Attack = true;
        }
    }

    /// <summary>
    /// æ∆¿Ã≈€º• ¥›±‚
    /// </summary>

    public void exitItemShop(){
        if (isItemShop) {
            isItemShop = false;
            UI_Itemshop.SetActive(false);
            PlayerMng.Instance.Can_Attack = true;
        }
    }

    /// <summary>
    /// chestBox ¥›±‚
    /// </summary>
    public void exitChestBox(){
        if (ischest) {
            ischest = false;
            UI_chestBox.SetActive(false);
            PlayerMng.Instance.Can_Attack = true;
        }
        
    }


    /// <summary>
    /// setting ¥›±‚
    /// </summary>
    public void Setting(){
        if (issetting && Input.GetKeyDown(KeyCode.Escape) && !isforge && !ischest && !isItemShop){
            issetting=false;
            UI_setting.SetActive(false);
        }
        else if (!issetting && Input.GetKeyDown(KeyCode.Escape) && !isforge && !ischest && !isItemShop)
        {
            issetting = true;
            UI_setting.SetActive(true);
        }
    }
}

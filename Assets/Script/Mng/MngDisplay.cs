using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngDisplay : MonoBehaviour{
    static public MngDisplay Instance;

    [SerializeField] public GameObject UI_Itemshop;
    [SerializeField] public GameObject UI_forge;
    [SerializeField] public GameObject UI_chestBox;


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
        
        exitForge();
        exitItemShop();
        exitChestBox();
    }

    /// <summary>
    /// √ ±‚»≠
    /// </summary>
    private void initComponent(){
        UI_forge.SetActive(false);
        UI_Itemshop.SetActive(false);
        UI_chestBox.SetActive(false);
    }


    /// <summary>
    /// ¥Î¿Â∞£ √¢ ¥›±‚
    /// </summary>
    public void exitForge(){
        if (UI_forge == true && Input.GetKeyDown(KeyCode.Escape)){
            UI_forge.SetActive(false);
            PlayerMng.Instance.Can_Attack = true;
        }
    }

    /// <summary>
    /// æ∆¿Ã≈€º• ¥›±‚
    /// </summary>

    public void exitItemShop(){
        if (UI_Itemshop == true && Input.GetKeyDown(KeyCode.Escape)){
            UI_Itemshop.SetActive(false);
            PlayerMng.Instance.Can_Attack = true;
        }
    }

    /// <summary>
    /// chestBox ¥›±‚
    /// </summary>
    public void exitChestBox(){
        if (UI_chestBox == true && Input.GetKeyDown(KeyCode.Escape)){
            UI_chestBox.SetActive(false);
            PlayerMng.Instance.Can_Attack = true;
        }
    }
}

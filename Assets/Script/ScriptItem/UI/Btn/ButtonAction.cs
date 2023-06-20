using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour{
    [SerializeField] public Button m_button;
    void Start(){
        m_button.onClick.AddListener(LoadFieldScene);
        
    }

    private void LoadFieldScene(){
        //Debug.Log("����");
        SceneManager.LoadScene("Scenes/Field");
    }

}

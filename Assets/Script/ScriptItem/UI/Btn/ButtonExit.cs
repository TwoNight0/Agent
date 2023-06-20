using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonExit : MonoBehaviour{
    [SerializeField] public Button m_button;
    void Start(){
        m_button.onClick.AddListener(turnOff);
        
    }

    private void LoadFieldScene(){
        //Debug.Log("눌림");
        SceneManager.LoadScene("Scenes/TestField");
    }

    private void turnOff(){
        Debug.Log("눌림");
#if(UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

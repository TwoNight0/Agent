using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundcheck : MonoBehaviour{

     private bool m_isGround = false;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Ground"){
            Debug.Log("출돌중임");
            m_isGround = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Ground"){
            Debug.Log("출돌중임");
            m_isGround = false;
        }
    }

    public bool getGround(){
        return m_isGround;
    }
}

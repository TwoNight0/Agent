using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 클래스 설명 : 충돌하는지 체크하는 클래스 (Colider 사용)
// 사용법 :
// 1. 사용할 클래스에서 private Groundcheck m_groundchecker; 생성
// 2. init구간)  m_groundchecker = transform.GetComponentInChildren<Groundcheck>(); 추가
public class Groundcheck : MonoBehaviour{
     private bool m_isGround = false;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Ground" || other.gameObject.layer == 6){
            //Debug.Log("출돌중임");
            m_isGround = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Ground"){
            //Debug.Log("출돌중임");
            m_isGround = false;
        }
    }

    public bool getGround(){
        return m_isGround;
    }
}

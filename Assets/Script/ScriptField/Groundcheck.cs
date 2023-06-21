using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ŭ���� ���� : �浹�ϴ��� üũ�ϴ� Ŭ���� (Colider ���)
// ���� :
// 1. ����� Ŭ�������� private Groundcheck m_groundchecker; ����
// 2. init����)  m_groundchecker = transform.GetComponentInChildren<Groundcheck>(); �߰�
public class Groundcheck : MonoBehaviour{
     private bool m_isGround = false;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Ground" || other.gameObject.layer == 6){
            //Debug.Log("�⵹����");
            m_isGround = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Ground"){
            //Debug.Log("�⵹����");
            m_isGround = false;
        }
    }

    public bool getGround(){
        return m_isGround;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potal1 : MonoBehaviour{
    [Header("�̵��� ��ġ")]
    public Transform where;
     
    void Start(){
        Debug.Log(where.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //other.transform.position += (where.position - other.transform.position); //�̰� �� �ȵɱ�?
        //PlayerMng.Instance.transform.localPosition = where.position;
        Debug.Log(other.transform.position);
        //PlayerMng.Instance.m_characterController.Move(where.position);
        //PlayerMng.Instance.m_characterController.transform.position = where.position;
    }





}

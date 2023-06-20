using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potal1 : MonoBehaviour{
    [Header("이동할 위치")]
    public Transform where;
     
    void Start(){
        Debug.Log(where.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        //other.transform.position += (where.position - other.transform.position); //이건 왜 안될까?
        //PlayerMng.Instance.transform.localPosition = where.position;
        Debug.Log(other.transform.position);
        //PlayerMng.Instance.m_characterController.Move(where.position);
        //PlayerMng.Instance.m_characterController.transform.position = where.position;
    }





}

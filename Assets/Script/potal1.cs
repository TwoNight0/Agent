using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// istrigger
/// </summary>
public class potal1 : MonoBehaviour{
    [Header("이동할 위치")]
    public Transform where;
    private PlayerMng playerMng; 

    void Start(){
        //Debug.Log(where.position);
        playerMng = PlayerMng.Instance;
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.name);
        //other.transform.position += (where.position - other.transform.position); //이건 왜 안될까?
        Debug.Log(other.transform.position);
        //playerMng.transform.position = where.position;
        //PlayerMng.Instance.m_characterController.Move(where.position);
        //PlayerMng.Instance.m_characterController.transform.position = where.position;
        teleport();
    }

    private void teleport(){
        //other.transform.position = where.position;

        playerMng.PlayerTeleport(where.position);
        MngMonster.Instance.spawnOn = true;
        
        //playerObj.transform.position = where.transform.position;
    }





}

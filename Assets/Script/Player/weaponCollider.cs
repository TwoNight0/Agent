using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollider : MonoBehaviour{
    private void OnCollisionEnter(Collision collision){
        if (collision.transform.tag == "Monster"){
            
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour{
    public float cur_Hp;
    public float max_Hp;
    public float dmg_physical;
    public float dmg_magical;


    private void Start(){
        max_Hp = 200;
        cur_Hp = max_Hp;
        dmg_magical = 10.0f;
        dmg_physical = 10.0f;
    }




}

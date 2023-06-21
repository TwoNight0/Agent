using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// tag : monster, weaponscript -> name change
/// </summary>
public class PunchingBagScript : Monster{

    public bool reset;
    public float timer_reset;

    // Start is called before the first frame update
    void Start(){
        max_Hp = 100.0f;
        cur_Hp = max_Hp;
        reset = false;
    }

    // Update is called once per frame
    void Update(){
        LookAtTarget();
        hpBarApply();
        restTimer();
    }

    //reset이 on이면 시간을 돌림 -> timer도 weapon쪽에서 다시 0으로 바꿈 
    private void restTimer(){
        if (reset){
            timer_reset += Time.deltaTime;
        }

        //n초 이상 데미지를 안받으면 체력되돌림
        if(timer_reset > 3.0f){
            reset = false;
            timer_reset = 0;
            cur_Hp = max_Hp;
        }
    }
}

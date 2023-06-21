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

    //reset�� on�̸� �ð��� ���� -> timer�� weapon�ʿ��� �ٽ� 0���� �ٲ� 
    private void restTimer(){
        if (reset){
            timer_reset += Time.deltaTime;
        }

        //n�� �̻� �������� �ȹ����� ü�µǵ���
        if(timer_reset > 3.0f){
            reset = false;
            timer_reset = 0;
            cur_Hp = max_Hp;
        }
    }
}

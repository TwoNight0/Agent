using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponCollider : MonoBehaviour{
    // 스크립트 위치별 장단점 
    // 위치 : 검 -> 1. 몬스터 말고도 다른 오브젝트가 닿아야할 시 그 코드 또한 여기서 관리 가능
    // 위치 : 몬스터 -> 1. 웨폰스크립트가 필요하지않음, 단점 몬스터말고 항아리와 같은 다른 오브젝트와 충돌해야하면 또다시 같은문제와 마주침, 항아리에 스크립트를 넣을 것이냐, 검에 넣을 것이냐
    private float dmg_physical;
    private float dmg_magical;
    Monster tempMonster;
    private bool colorOn;
    private float timer_Hit;

   

    private void Update(){
        hitColorChange();
    }

    //데미지 만큼 몬스터의 피를까야해

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            return;
        }

        Debug.Log($"충돌 = {other.name}");
        if (other.CompareTag("Monster")){
            //Debug.Log("몬스터 인식");
            colorOn = true;
            tempMonster = other.transform.GetComponent<Monster>();
            //때릴때마다 데미지가 바뀔수도있는거니까
            dmg_physical = PlayerMng.Instance.PubPlayerDmg_physical; // 물뎀
            dmg_magical = PlayerMng.Instance.PubPlayerDmg_magical; // 마뎀
            tempMonster.changeColor(Color.red);
            PlayerMng.Instance.AttackDmg(dmg_physical, dmg_magical, tempMonster);


            PlayerMng.Instance.weaponMeshCollider.enabled = false;
        }

        if(other.name == "punching bag"){
            //Debug.Log("샌드백");
            PunchingBagScript punch;
            punch = other.transform.GetComponent<PunchingBagScript>();
            punch.changeColor(Color.red);
            punch.reset = true;
            punch.timer_reset = 0.0f;
        }

    }

    // 공격받으면 불온 -> 타이머돌아감 -> 타이머가 일정으로 다되면 -> 다시 색 복원 -> 초 리셋, 불끄기
    private void hitColorChange(){
        if (colorOn){
            timer_Hit += Time.deltaTime;
        }

        if (timer_Hit > 1.0f){
            tempMonster.changeColor(Color.white);
            timer_Hit = 0.0f;
            colorOn = false;
        }
    }

    //private void OnCollisionEnter(Collision collision){
    //    Debug.Log("충돌");
    //    if (collision.collider.CompareTag("Monster")){
    //        Debug.Log("몬스터 인식");
    //        tempMonster = collision.transform.GetComponent<Monster>();
    //        //때릴때마다 데미지가 바뀔수도있는거니까
    //        dmg_physical = PlayerMng.Instance.PlayerAttackStat().Item1; // 물뎀
    //        dmg_magical = PlayerMng.Instance.PlayerAttackStat().Item2; // 마뎀
    //        tempMonster.cur_Hp = tempMonster.cur_Hp - dmg_physical;
    //        tempMonster.cur_Hp = tempMonster.cur_Hp - dmg_magical;

    //        PlayerMng.Instance.weaponMeshCollider.enabled = false;
    //    }
    //}

}

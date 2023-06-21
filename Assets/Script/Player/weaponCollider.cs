using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class weaponCollider : MonoBehaviour{
    // ��ũ��Ʈ ��ġ�� ����� 
    // ��ġ : �� -> 1. ���� ���� �ٸ� ������Ʈ�� ��ƾ��� �� �� �ڵ� ���� ���⼭ ���� ����
    // ��ġ : ���� -> 1. ������ũ��Ʈ�� �ʿ���������, ���� ���͸��� �׾Ƹ��� ���� �ٸ� ������Ʈ�� �浹�ؾ��ϸ� �Ǵٽ� ���������� ����ħ, �׾Ƹ��� ��ũ��Ʈ�� ���� ���̳�, �˿� ���� ���̳�
    private float dmg_physical;
    private float dmg_magical;
    Monster tempMonster;
    private bool colorOn;
    private float timer_Hit;

   

    private void Update(){
        hitColorChange();
    }

    //������ ��ŭ ������ �Ǹ������

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            return;
        }

        Debug.Log($"�浹 = {other.name}");
        if (other.CompareTag("Monster")){
            //Debug.Log("���� �ν�");
            colorOn = true;
            tempMonster = other.transform.GetComponent<Monster>();
            //���������� �������� �ٲ�����ִ°Ŵϱ�
            dmg_physical = PlayerMng.Instance.PubPlayerDmg_physical; // ����
            dmg_magical = PlayerMng.Instance.PubPlayerDmg_magical; // ����
            tempMonster.changeColor(Color.red);
            PlayerMng.Instance.AttackDmg(dmg_physical, dmg_magical, tempMonster);


            PlayerMng.Instance.weaponMeshCollider.enabled = false;
        }

        if(other.name == "punching bag"){
            //Debug.Log("�����");
            PunchingBagScript punch;
            punch = other.transform.GetComponent<PunchingBagScript>();
            punch.changeColor(Color.red);
            punch.reset = true;
            punch.timer_reset = 0.0f;
        }

    }

    // ���ݹ����� �ҿ� -> Ÿ�̸ӵ��ư� -> Ÿ�̸Ӱ� �������� �ٵǸ� -> �ٽ� �� ���� -> �� ����, �Ҳ���
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
    //    Debug.Log("�浹");
    //    if (collision.collider.CompareTag("Monster")){
    //        Debug.Log("���� �ν�");
    //        tempMonster = collision.transform.GetComponent<Monster>();
    //        //���������� �������� �ٲ�����ִ°Ŵϱ�
    //        dmg_physical = PlayerMng.Instance.PlayerAttackStat().Item1; // ����
    //        dmg_magical = PlayerMng.Instance.PlayerAttackStat().Item2; // ����
    //        tempMonster.cur_Hp = tempMonster.cur_Hp - dmg_physical;
    //        tempMonster.cur_Hp = tempMonster.cur_Hp - dmg_magical;

    //        PlayerMng.Instance.weaponMeshCollider.enabled = false;
    //    }
    //}

}

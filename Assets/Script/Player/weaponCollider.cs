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

    //������ ��ŭ ������ �Ǹ������

    private void OnCollisionEnter(Collision collision){
        Debug.Log("�浹");
        if (collision.collider.CompareTag("Monster")){
            Debug.Log("���� �ν�");
            tempMonster = collision.transform.GetComponent<Monster>();
            //���������� �������� �ٲ�����ִ°Ŵϱ�
            dmg_physical = PlayerMng.Instance.PlayerAttackStat().Item1; // ����
            dmg_magical = PlayerMng.Instance.PlayerAttackStat().Item2; // ����
            tempMonster.cur_Hp = tempMonster.cur_Hp - dmg_physical;
            tempMonster.cur_Hp = tempMonster.cur_Hp - dmg_magical;

            PlayerMng.Instance.weaponMeshCollider.enabled = false;
        }
    }

}

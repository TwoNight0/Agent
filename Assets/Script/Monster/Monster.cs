using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour{
    public float cur_Hp;
    public float max_Hp;
    public float dmg_physical;
    public float dmg_magical;
    private bool attackActive;
    public Transform target;
    private Image cur_HpImg;
    public Renderer MonsterColor;
    //
    public RectTransform uiRectHP;

    private void Start(){
        max_Hp = 50;
        cur_Hp = max_Hp;
        dmg_magical = 10.0f;
        dmg_physical = 10.0f;
        MonsterColor = gameObject.GetComponent<Renderer>();
        target = PlayerMng.Instance.transform;
        //cur_HpImg = transform.Find("Hp_Cur");
    }

    private void Update(){
        death();
        LookAtTarget();
    }


    private void createitem(){

    }

    private void death(){
        // 현재이미지.fillAmount = 닳는것 / 최대치 ;
       
        if (cur_Hp <= 0){
            transform.gameObject.SetActive(false);
        }
    }

    public void changeColor(Color color){
        MonsterColor.material.color = color;
    }

    public void LookAtTarget(){
        //transform.LookAt(target);
        uiRectHP.LookAt(target);
    }
}

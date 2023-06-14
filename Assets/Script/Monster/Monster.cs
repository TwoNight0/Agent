using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour{
    #region 컴포넌트
    public Transform target;
    private Vector3 vector_target;
    [SerializeField] private Image cur_HpImg;
    public Renderer objectColor;
    public RectTransform uiRectHP;
    #endregion

    #region 스탯
    public float cur_Hp;
    public float max_Hp;
    public float dmg_physical;
    public float dmg_magical;
    private float Speed;
    #endregion

    [SerializeField] private bool attackMode;//몬스터 공격 모드


    private void Start(){
        max_Hp = 50;
        cur_Hp = max_Hp;
        dmg_magical = 10.0f;
        dmg_physical = 10.0f;
        Speed = 1.0f;
        objectColor = gameObject.GetComponent<Renderer>();
        target = PlayerMng.Instance.transform;
        attackMode = false;
        gameObject.AddComponent<BoxCollider>();
    }

    private void Update(){
        death();
        LookAtTarget();
        hpBarApply();
        MonsterMove();
    }


    private void createitem(){

    }

    //체력바 적용
    public void hpBarApply(){
        cur_HpImg.fillAmount = cur_Hp / max_Hp;
    }

    private void death(){
        // 현재이미지.fillAmount = 닳는것 / 최대치 ;
       
        if (cur_Hp <= 0){
            transform.gameObject.SetActive(false);
        }
    }

    public void changeColor(Color color){
        objectColor.material.color = color;
    }

    public void LookAtTarget(){
        Vector3 relativePos = target.position - uiRectHP.position;
        Vector3 testPos = new Vector3(target.position.x, uiRectHP.position.y, target.position.z);
        Vector3 testPos2 = new Vector3(target.position.x, target.position.y, target.position.z);
        
        uiRectHP.LookAt(testPos2);
        testPos2.x = 0;
        testPos2.z = 0;
        uiRectHP.Rotate(testPos2);
    }

    private void MonsterMove(){
        if (attackMode){
            vector_target = target.position;
            //transform.Translate(-vector_target * Time.deltaTime);
            transform.Translate((target.position - transform.position) * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);

        }
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.transform.tag == "Player"){
            Debug.Log("플레이어와 충돌함");
            PlayerMng.Instance.GetHp = PlayerMng.Instance.GetHp - dmg_magical;
            PlayerMng.Instance.GetHp = PlayerMng.Instance.GetHp - dmg_physical;


        }
    }
}

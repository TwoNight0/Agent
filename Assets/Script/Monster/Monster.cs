using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour{
    #region 컴포넌트
    public Transform target;
    private Vector3 vector_target;
    [SerializeField] private Image cur_HpImg;
    [SerializeField] private Image mid_HpImg;
    public Renderer objectColor;
    public RectTransform uiRectHP;
    public Rigidbody rb;
    public float movespeed;
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
        //attackMode = false;
        gameObject.AddComponent<BoxCollider>();
    }

    private void Update(){
        death();
        LookAtTarget();
        Invoke("MonsterMove", 1f);
        hpBarApply();

    }


    private void SpawnMonster(){

    }

    //체력바 적용
    public void hpBarApply(){
        cur_HpImg.fillAmount = (cur_Hp / max_Hp);
        if(cur_HpImg.fillAmount < mid_HpImg.fillAmount){
            mid_HpImg.fillAmount -= Time.deltaTime * (1 / 5.0f);
            if(mid_HpImg.fillAmount < cur_HpImg.fillAmount){
                mid_HpImg.fillAmount = cur_HpImg.fillAmount;
            }
        }
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
        Vector3 testPos = new Vector3(target.position.x, uiRectHP.position.y, target.position.z);
        
        //testPos.y = 3; //선생님이 생각하셨던 답은 이거같네
        //Debug.Log(uiRectHP.position.y);
        //Debug.Log(testPos);

        //2. 룩엣할때 y좌표를 상수로 바꾸기 안됨.
        
        uiRectHP.LookAt(testPos);


        //uiRectHP.LookAt(target.position);
        

        //1. 특정각도로 회전하기 
        //uiRectHP.rotation = Quaternion.Euler(0, 200, 0);
    }

    private void MonsterMove(){
        if (attackMode){
            vector_target = target.position;
            //transform.Translate(-vector_target * Time.deltaTime);
            transform.Translate((target.position - transform.position) * Time.deltaTime * movespeed);
            //rb.AddForce(target.transform.position, ForceMode.Impulse);

            //transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);

        }


    }
    private void OnCollisionEnter(Collision collision){
        if (collision.transform.tag == "Player"){
            Debug.Log("플레이어와 충돌함");
            PlayerMng.Instance.TakeDmg(dmg_physical, dmg_physical);


        }
    }
}

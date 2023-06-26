using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Monster : MonoBehaviour{
    #region 컴포넌트
    public Transform target;
    private Vector3 vector_target;
    [SerializeField] private Image cur_HpImg;
    [SerializeField] private Image mid_HpImg;
    //protected MeshRenderer[] render;
    protected Material[] mats;
    public RectTransform uiRectHP;
    public Rigidbody rb;
    public float movespeed;
    NavMeshAgent nav;
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
        //render = GetComponentInChildren<MeshRenderer>();

        target = PlayerMng.Instance.transform;
        nav = GetComponent<NavMeshAgent>();

        List<Material> listMats = new List<Material>();
        Renderer[] rens = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rens.Length; i++)
        {
            Renderer ren = rens[i];
            listMats.Add(ren.material);
        }
        mats = listMats.ToArray();
    }

    private void Update(){
        death();
        LookAtTarget();
        hpBarApply();
        MonsterMove();
        if (nav != null)
        { 
            string value = (nav.velocity.magnitude).ToString();
            //Debug.Log(value);
        }
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

        //
        //for (int i = 0; i < render.Length; i++) {
        //    render[i] = GetComponentsInChildren<MeshRenderer>()[i]; //메쉬 랜더러를 받아옴
        //    render[i].material.color = color;
        //}
        //
        int count = mats.Length;
        for (int i = 0; i < count; i++)
        {
            mats[i].color = color;
        }
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
        if (attackMode && nav != null &&  nav.enabled){
            nav.SetDestination(target.position);
        }


    }
    private void OnCollisionEnter(Collision collision){
        if (collision.transform.tag == "Player"){
            Debug.Log("플레이어와 충돌함");
            PlayerMng.Instance.TakeDmg(dmg_physical, dmg_physical);


        }
    }


    void FreezeVelocity()
    {
        //물리력이 NavAgent의 이동을 방해하지 않도록함
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}

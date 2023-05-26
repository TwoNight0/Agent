using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{

    // -- Component --
    //private CameraManager m_cameraMng;
    //private Rigidbody m_rb;
    private CharacterController m_characterController;
    private Animator m_animator;
    private Groundcheck m_groundchecker;
    //private Camera m_camFps;
    // ----

    // -- variable --
    [SerializeField, Range(0f, 1f)] float m_fDistanceToGround;
    private float m_verticalVelocity = 0;
    private float m_gravity = 9.81f;
    public float mouseSensitivity = 100f;
    public float m_jumpForce = 5f;
    // ----

    // -- Vector --
    private Vector3 m_rotationValue;
    private Vector3 m_moveDir;
    private Vector3 m_dashDirection;
    private Vector3 m_slopeVector;
    // ---- 

    // -- bool --
    private bool m_pressJump = false;
    private bool m_isGround = false;
    public bool m_isSlope = false;
    private bool m_dashcheck = false;
    public bool invenflag = false;
    // ----

    private GameObject UI;

    // -- Timer --
    private float m_timerDash = 10.0f;
    private float m_dashDuration = 2.0f;
    //----

    // -- Status
    private float m_dmgPaladin = 30.0f;
    private float m_moveSpeed = 2f;
    private float maxhp = 400f;
    private float hp = 0f;
    
    //데미지 공식
    // ((플레이어 기본데미지 + (무기데미지 * 크리티컬함수) + ( 마법데미지 * 크리티컬함수 ) * 악세서리 추가데미지 ) 




    // ---

    // -- keySetting -- 변경하는함수도 만들자
    // ----

    private enum moveParameter{
        Move_vertical,
        Move_horizontal,
        Dash_shift,
    }


    private void Awake(){
        //m_rb = this.transform.GetComponent<Rigidbody>();
        m_characterController = transform.GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_groundchecker = transform.GetComponentInChildren<Groundcheck>();
    }

    void Start(){
        //m_cameraMng = CameraManager.Instance;
        //m_camFps = m_cameraMng.GetCamera(enumCamera.CamFps);


        UI = GameObject.Find("UI_Inventory");

        m_rotationValue = transform.rotation.eulerAngles;
        hp = maxhp;//피 최대로 초기화
    }


    private void Update(){
        // -- 문제 없음 --
        Jump();
        Rotation();
        //UpdateTimer();


        // ----

        // -- 체크 중 ---
        Moving();
        PlayDashAction();
        //checkSlopeVelocity();
        checkGravity();
        //DoAttack();

        inventoryopen();


        // ----



    }

    private void OnAnimatorIK(int layerIndex){
        m_animator.SetLookAtWeight(1); //0~1������ �켱����
        //m_animator.SetLookAtPosition(m_trsLookAtObj.position);

        m_animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        m_animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);

        if (Physics.Raycast(m_animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down,
            out RaycastHit hit, m_fDistanceToGround + 1f, LayerMask.GetMask("Ground")))
        {
            Vector3 footPos = hit.point;
            footPos.y += m_fDistanceToGround;
            m_animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPos);

            m_animator.SetIKRotation(AvatarIKGoal.LeftFoot,
                Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal));
        }

        m_animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        m_animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        if (Physics.Raycast(m_animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down,
            out RaycastHit hit2, m_fDistanceToGround + 1f, LayerMask.GetMask("Ground")))
        {
            Vector3 footPos = hit2.point;
            footPos.y += m_fDistanceToGround;
            m_animator.SetIKPosition(AvatarIKGoal.RightFoot, footPos); 

            m_animator.SetIKRotation(AvatarIKGoal.RightFoot,
                Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit2.normal), hit2.normal));
        }
    }

    //-- 갱신 코드 --
    private void UpdateTimer() {
        if (m_timerDash < 2.0f) { // 2초쿨
            m_timerDash += Time.deltaTime;
            if(m_timerDash > 1.9f){
                //Debug.Log($"대쉬가능 ={m_timerDash}");
                
                m_animator.SetBool(moveParameter.Dash_shift.ToString(), false);

            }
        }
    }

    private void UpdateisGround(){
        this.m_isGround = m_groundchecker.getGround();
        //Debug.Log(m_isGround);
    }

    private void PlayDashAction() {
        if (m_dashcheck){
            m_animator.Play("Stand To Roll");
            m_dashcheck = false;
        }
    }

    public void checkSlopeVelocity()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, m_characterController.height * 0.7f)) //뭔가에 닿으면
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            //Debug.Log(angle);
            if (angle >= m_characterController.slopeLimit)
            {
                m_isSlope = true;
                m_slopeVector = Vector3.ProjectOnPlane(new Vector3(0, m_gravity, 0), hit.normal);
            }
            else
            {
                m_isSlope = false;
            }
        }
    }
    //-----

    //TODO 대쉬 돌아오기
    private void Moving(){
        m_moveDir.x = Input.GetAxis("Horizontal");
        m_moveDir.z = Input.GetAxis("Vertical");
        m_moveDir = new Vector3(m_moveDir.x, 0f, m_moveDir.z).normalized;

        //애니메이션 수치변경 Blend용
        m_animator.SetFloat(moveParameter.Move_vertical.ToString(), Input.GetAxis("Vertical"));
        m_animator.SetFloat(moveParameter.Move_horizontal.ToString(), Input.GetAxis("Horizontal"));


        //m_rb.AddForce(transform.rotation * m_moveDir * m_moveSpeed);

        //왜돌아옴?ㅋㅋㅋ
        if (Input.GetKeyDown(KeyCode.LeftShift) && m_timerDash > m_dashDuration){

            //Debug.Log($"대쉬불가 : {m_timerDash}");
            m_dashDirection = m_moveDir; //이걸해줘야 키를 때도 대쉬 방향이 유지됨 키 때면 대쉬 0.0 되버림
            //Debug.Log(m_dashDirection);
            m_timerDash = 0.0f;
            m_dashcheck = true;
        }

        if(m_timerDash <= m_dashDuration){ //대쉬타이머가 듀레이션보다 작을작을때 작동 shift 누르면 작아짐
            m_characterController.Move(m_dashDirection * 4 * Time.deltaTime);//이동
            m_timerDash += Time.deltaTime;
         
        }


        if (m_isSlope){ //경사로일때 경사로 반대로 이동(미끄러짐)
            m_characterController.Move(-m_slopeVector * Time.deltaTime);
        }
        else{ //기본 움직임
            m_characterController.Move(transform.rotation * m_moveDir * m_moveSpeed * Time.deltaTime);
        }
    }

    private void Jump(){
        UpdateisGround();
        if (m_isGround == true && Input.GetKeyDown(KeyCode.Space)){
            m_pressJump = true;
        }
    }

    private void Rotation(){
        float MouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        m_rotationValue.x += MouseY * -1f;
        m_rotationValue.y += MouseX;

        m_rotationValue.x = Mathf.Clamp(m_rotationValue.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, m_rotationValue.y, 0f);
        //m_camFps.transform.rotation = Quaternion.Euler(m_rotationValue.x, m_rotationValue.y, 0f);
    }

    private void DoAttack() {
        //TODO : 정면으로안가고 사선으로감, 공격 및 데미지 주고받는거 적용시켜야함
        if (Input.GetMouseButtonDown(0)){// 공격 
            m_animator.Play("Two Hand Club Combo");
        }
        else if (Input.GetMouseButtonDown(1)) { //패링
            m_animator.Play("Two Hand Club Combo");
        }
    }
    private void inventoryopen()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            invenflag = !invenflag;

            //Debug.Log(invenflag);
            
        }

        if (invenflag)//트루면 켜짐
        {
            UI.SetActive(true);
        }
        else//false면 꺼짐
        {
            UI.SetActive(false);
        }

    }

    public void SwichingActive()
    {
        invenflag = !invenflag;
        UI.SetActive(invenflag);
    }

    private void TakeDamage(float dmg) {
        hp -= dmg;

        if (hp <= 0)
        {
            Kill();
        }

    }
    public void Kill()
    {
        Debug.Log("사망");
        m_animator.Play("Explosion");

        //EventMng.ins.DeathPanel.SetActive(true);
        //SoundMng.ins.PlayEffect("Grenade Explosion");
        //TODO 코루틴 사용할것
        Time.timeScale = 0.0f;
    }

    private void checkGravity(){
        if (m_groundchecker.getGround() == false){ // IsGround = false
          //땅에 붙어있지않아 중력을 받아야 할 때
            m_verticalVelocity -= m_gravity * Time.deltaTime; //수직속도를 계속 낮춰 중력을 받게함
            m_pressJump = false;
        }
        else{ // IsGround = true, space 눌를시 점프
            if (m_pressJump){
                m_verticalVelocity = m_jumpForce; //수직속도를 정상적으로 넣어서 작동하게함
            }
            else{ // pressJump가 눌리지않으면 점프 x
                m_verticalVelocity = 0f; 
            }
        }

        //m_verticalVelocity -= m_gravity * Time.deltaTime;
        m_characterController.Move(new Vector3(0, m_verticalVelocity, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Item")
        {
            Item temp = other.transform.GetComponent<Item>(); //스크립트를 가져온것
            int tempCode = temp.GetItemCode;
            InventoryUIMng.Instance.giveDataItemSlot(tempCode);
            Debug.Log("아이템 코드 : " + tempCode);
            Destroy(temp.gameObject);//여기서 게임오브젝트까지 지워주지않으면 스크립트만 지워짐.
        }
    }

}

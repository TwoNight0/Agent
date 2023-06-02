using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 클래스 설명 : 캐릭터를 사용하데 필요한 공용 메서드 들을 가지고 있음

public class PlayerMng : MonoBehaviour{

    public static PlayerMng Instance;

    #region (Component)
    // -- Component --
    //private CameraManager m_cameraMng;
    //private Camera m_camFps;
    //private Rigidbody m_rb;
    private CharacterController m_characterController;
    private Animator m_animator;
    private Groundcheck m_groundchecker;
    private CharacterData Character = null;
    private GameObject UI;
    // ----
    #endregion

    #region (Enum)(moveParameter, PlayAbleCharacter)
    private enum moveParameter{
        Move_vertical,
        Move_horizontal,
        Dash_shift,
    }
    public enum PlayAbleCharacter{
        Paladin,
        Archer,
    }
    #endregion

    #region (Vecter)
    // -- Vector --
    private Vector3 m_rotationValue;
    private Vector3 m_moveDir;
    private Vector3 m_dashDirection;
    private Vector3 m_slopeVector;
    // ---- 
    #endregion

    #region (Bool)
    // -- bool --
    private bool m_pressJump = false;
    private bool m_isGround = false;
    public bool m_isSlope = false;
    public bool invenflag = false;
    // ----
    #endregion

    #region (Sprite)아이콘이미지
    //--아이콘이미지
    public Sprite hillImg;
    public Sprite nomalSkillImg;
    public Sprite UltimateSkillImg;
    public Sprite MainWeaponImg;
    public Sprite SubWeaponImg;
    public Sprite DashImg;
    #endregion

    #region [Timer]
    //--Timer --
    private float m_dashDuration = 2.0f;
    private float timer_Dash = 10.0f;
    private float timer_hill;
    private float timer_normal;
    private float timer_Ultimate;
    #endregion

    #region [variable]
    // -- variable --
    [SerializeField, Range(0f, 1f)] float m_fDistanceToGround;
    private float m_verticalVelocity = 0;
    private float m_gravity = 9.81f;
    public float mouseSensitivity = 400.0f;
    public float m_jumpForce = 5f;
    public int Chosedcharacter; //선택한 캐릭터의 클래스
    // ----
    #endregion

    #region [Status] Player value
    // -- Status (characterData를 가져와서 변경해서 사용할 값들)
    private float dmg;
    private float magic = 0;
    [SerializeField] private float hp_cur = 0.0f;
    private float hp_max;
    private float m_moveSpeed;
    private float defense_physical;
    private float defense_magic;


    // 초기에 characterData에서 복사한데이터로 이 데이터를 변경(증가, 감소)하여 사용
    private float skill_hill_cool = 5.0f;
    private float skill_nomal_cool;
    private float skill_Ultimate_cool;
    private float Dash_cool;
    #endregion

    #region (Get, Set)private value 넘김
    public float GetHp
    {
        get => hp_cur;
        set => hp_cur = value;
    }

    public float GetMaxHp
    {
        get => hp_max;
        set => hp_max = value;
    }
    #endregion

 // ---- [ Awake, start , Update ] ----
    private void Awake() {
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
        initComponent();
    }

    void Start() {
        DontDestroyOnLoad(this);
        //캐릭터 선택
        Chosedcharacter = (int)PlayAbleCharacter.Paladin; 
        //--

        initCharacterData(Chosedcharacter);
    }

    private void Update() {

        // -- 문제 없음 --
        Jump();
        Rotation();
        // ----


        // -- 체크 중 ---
        Moving();
        UserInput();
        //checkSlopeVelocity();
        checkGravity();
        //DoAttack();
        inventoryopen();

        // ----
    }
//  ---- ---- ---- ---- 

    #region 초기화(init)
    public void initCharacterData(int _character){
        
        switch (Chosedcharacter){
            case 0:
                Character = new CharacterData(PlayAbleCharacter.Paladin);
                break;
            case 1:
                Character = new CharacterData(PlayAbleCharacter.Archer);
                break;
        }

        //초기화(값 할당)
        dmg = Character.Pubdmg;
        magic = Character.Pubmagic;
        hp_max = Character.PubHp_max;
        m_moveSpeed = Character.Pubm_moveSpeed;
        defense_physical = Character.PubDefense_physical;
        defense_magic = Character.PubDefense_magical;
        skill_hill_cool = Character.Pubskill_hill_cool;
        skill_nomal_cool = Character.Pubskill_nomal_cool;
        skill_Ultimate_cool = Character.Pubskill_Ultimate_cool;
        Dash_cool = Character.PubDash;

        //초기화(이미지)
        hillImg = Character.hillSprite;
        nomalSkillImg = Character.nomalSkillSprite;
        UltimateSkillImg = Character.UltimateSkillSprite;
        MainWeaponImg = Character.MainWeaponSprite;
        SubWeaponImg = Character.SubWeaponSprtie;
        DashImg = Character.DashSprite;
        UserDisplay.Instance.SubWeaponImage.enabled = false;

        //초기화(타이머)
        timer_hill = skill_hill_cool;
        timer_normal = skill_nomal_cool;
        timer_Ultimate = skill_Ultimate_cool;
        timer_Dash = Character.PubDash;

        //초기화(체력)
        hp_cur = hp_max;
    }

    private void initComponent() {
        //m_rb = this.transform.GetComponent<Rigidbody>();
        m_characterController = transform.GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_groundchecker = transform.GetComponentInChildren<Groundcheck>();
        //m_cameraMng = CameraManager.Instance;
        //m_camFps = m_cameraMng.GetCamera(enumCamera.CamFps);
        UI = GameObject.Find("UI_Inventory");
        m_rotationValue = transform.rotation.eulerAngles;
    }
    #endregion
    
    private void OnAnimatorIK(int layerIndex) {
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
    }// 3D 부착 컴포넌트

    #region checker(그라운드, 그래비티, 슬로프)
    //그라운드 체커
    private void UpdateisGround() {
        this.m_isGround = m_groundchecker.getGround();
        //Debug.Log(m_isGround);
    }

    //중력 체커
    private void checkGravity(){
        if (m_groundchecker.getGround() == false){ // IsGround = false
          //땅에 붙어있지않아 중력을 받아야 할 때
            m_verticalVelocity -= m_gravity * Time.deltaTime; //수직속도를 계속 낮춰 중력을 받게함
            m_pressJump = false;
        }
        else{ // IsGround = true, space 눌를시 점프
            if (m_pressJump)
            {
                m_verticalVelocity = m_jumpForce; //수직속도를 정상적으로 넣어서 작동하게함
            }
            else
            { // pressJump가 눌리지않으면 점프 x
                m_verticalVelocity = 0f;
            }
        }
        //m_verticalVelocity -= m_gravity * Time.deltaTime;
        m_characterController.Move(new Vector3(0, m_verticalVelocity, 0) * Time.deltaTime);
    }

    //경사도 체커
    public void checkSlopeVelocity(){
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, m_characterController.height * 0.7f)){ //뭔가에 닿으면
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            //Debug.Log(angle);
            if (angle >= m_characterController.slopeLimit){
                m_isSlope = true;
                m_slopeVector = Vector3.ProjectOnPlane(new Vector3(0, m_gravity, 0), hit.normal);
            }
            else{
                m_isSlope = false;
            }
        }
    }
    #endregion

    #region Move, Jump, rotate
    private void Moving() {
        m_moveDir.x = Input.GetAxis("Horizontal");
        m_moveDir.z = Input.GetAxis("Vertical");
        m_moveDir = new Vector3(m_moveDir.x, 0f, m_moveDir.z).normalized;

        //애니메이션 수치변경 Blend용
        m_animator.SetFloat(moveParameter.Move_vertical.ToString(), Input.GetAxis("Vertical"));
        m_animator.SetFloat(moveParameter.Move_horizontal.ToString(), Input.GetAxis("Horizontal"));

        //m_rb.AddForce(transform.rotation * m_moveDir * m_moveSpeed);


        if (m_isSlope) { //경사로일때 경사로 반대로 이동(미끄러짐)
            m_characterController.Move(-m_slopeVector * Time.deltaTime);
        }

        else { //기본 움직임
            m_characterController.Move(transform.rotation * m_moveDir * m_moveSpeed * Time.deltaTime);
        }
    }

    private void Jump() {
        UpdateisGround();
        if (m_isGround == true && Input.GetKeyDown(KeyCode.Space)) {
            m_pressJump = true;
        }
    }

    private void Rotation() {
        float MouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        m_rotationValue.x += MouseY * -1f;
        m_rotationValue.y += MouseX;

        m_rotationValue.x = Mathf.Clamp(m_rotationValue.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, m_rotationValue.y, 0f);
        //m_camFps.transform.rotation = Quaternion.Euler(m_rotationValue.x, m_rotationValue.y, 0f);
    }
    #endregion

    #region PlayerInput(공격, 스킬, 상호작용(인벤토리))
    private void DoAttack(){
        //TODO : 정면으로안가고 사선으로감, 공격 및 데미지 주고받는거 적용시켜야함
        if (Input.GetMouseButtonDown(0)){// 공격 
            m_animator.Play("Two Hand Club Combo");
        }
        else if (Input.GetMouseButtonDown(1)){ //TODO패링
            m_animator.Play("Two Hand Club Combo");
        }
    }
    //데미지 공식
    // ((플레이어 기본데미지 + (무기데미지 * 크리티컬함수) + ( 마법데미지 * 크리티컬함수 ) * 악세서리 추가데미지 ) 
    // 방어공식 실제데미지 = 데미지 * 100 / 100 + 방어력 
    private void TakeDamage(float dmg){
        hp_cur -= dmg;

        if (hp_cur <= 0){
            Kill();
        }
    }

    public void Kill(){
        Debug.Log("사망");
        m_animator.Play("Explosion");

        //EventMng.ins.DeathPanel.SetActive(true);
        //SoundMng.ins.PlayEffect("Grenade Explosion");
        //TODO 코루틴 사용할것
        Time.timeScale = 0.0f;
    }

    // invenflag의 값에 따라 인벤을 열고 닫음
    private void inventoryopen(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            invenflag = !invenflag;
        }
        if (invenflag){//트루면 켜짐
            UI.SetActive(true);
        }
        else{//false면 꺼짐
            UI.SetActive(false);
        }
    }
    public void SwichingActive(){
        invenflag = !invenflag;
        UI.SetActive(invenflag);
    }

    private void UserInput(){
        
        if (skill_hill_cool > timer_hill) {//스킬 쿨 일때
            timer_hill += Time.deltaTime;
            UserDisplay.Instance.Filter_Hill.fillAmount += (1 / skill_hill_cool) * Time.deltaTime;
        } 
        else{ //스킬 쿨 다돌았을 때
            UserDisplay.Instance.Filter_Hill.fillAmount = 0.0f;
            UserDisplay.Instance.hillImage.color = Color.green;
        }

        if (skill_nomal_cool > timer_normal) { 
            timer_normal += Time.deltaTime;
            UserDisplay.Instance.Filter_skill_nomal.fillAmount += (1 / skill_nomal_cool) * Time.deltaTime;
        }

        else{
            UserDisplay.Instance.Filter_skill_nomal.fillAmount = 0.0f;
            UserDisplay.Instance.nomarlImage.color = Color.green;
        }

        if (skill_Ultimate_cool > timer_Ultimate) { 
            timer_Ultimate += Time.deltaTime;
            UserDisplay.Instance.Filter_skill_Ultimate.fillAmount += (1 / skill_Ultimate_cool) * Time.deltaTime;
        }
        else{
            UserDisplay.Instance.Filter_skill_Ultimate.fillAmount = 0.0f;
            UserDisplay.Instance.ultimateImage.color = Color.green;
        }

        if (Dash_cool > timer_Dash) { 
            timer_Dash += Time.deltaTime;
            UserDisplay.Instance.Filter_Dash.fillAmount += (1 / Dash_cool) * Time.deltaTime;
        }
        else{
            UserDisplay.Instance.Filter_Dash.fillAmount = 0.0f;
            UserDisplay.Instance.DashImage.color = Color.green;
        }

        //힐(Q)
        if (Input.GetKeyDown(KeyCode.Q) && (skill_hill_cool <= timer_hill)){
            Debug.Log("Q");
            Debug.Log(timer_hill);
            timer_hill = 0;
            UserDisplay.Instance.hillImage.color = Color.white;
        }
        //노말스킬(F)
        if (Input.GetKeyDown(KeyCode.F) && (skill_nomal_cool <= timer_normal)){
            Debug.Log("F");
            timer_normal = 0;
            UserDisplay.Instance.nomarlImage.color = Color.white;
        }

        //궁극기(G)
        if (Input.GetKeyDown(KeyCode.G) && (skill_Ultimate_cool <= timer_Ultimate)){
            Debug.Log("G");
            timer_Ultimate = 0;
            UserDisplay.Instance.ultimateImage.color = Color.white;
        }

        //구르기(Dash, shift)
        if (Input.GetKeyDown(KeyCode.LeftShift) && (Dash_cool <= timer_Dash)){
            Debug.Log("Dash");
            timer_Dash = 0;
            UserDisplay.Instance.DashImage.color = Color.white;
            //m_characterController.Move(m_dashDirection * 4 * Time.deltaTime);//이동
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            //TMP도 바꿔야함 그리고 출력도해야하고 
            Debug.Log("1");


            //보이는 이미지 변경
            //방법1.
            UserDisplay.Instance.SubWeaponImage.enabled = false;
            //방법2.
            //Color color = UserDisplay.Instance.SubWeaponImage.color;
            //color.a = 0.0f;
            //UserDisplay.Instance.SubWeaponImage.color = color;

            //텍스트 변경
            UserDisplay.Instance.whichone.text = "<color=green>1</color> / 2";

            //필터 적용


        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            Debug.Log("2");

            //보이는 이미지 변경
            //방법1.
            UserDisplay.Instance.SubWeaponImage.enabled = true;
            //방법2.
            //Color color = UserDisplay.Instance.SubWeaponImage.color;
            //color.a = 1.0f;
            //UserDisplay.Instance.SubWeaponImage.color = color;

            //텍스트 변경
            UserDisplay.Instance.whichone.text = "1 / <color=green>2</color>";

        }
    }

   
    #endregion

    #region 충돌관리(아이템)
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
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// notify message to player   
/// 사용법 : 
/// 1. UI_그리는 부분에 PopUp을 만들고
/// 2. TMP로 text 두개 button 하나를 만들고 각각 title, value, btn_check 할당해줌
/// 4. Obj PopUp 에는 껏다켰다했을때 안보일 수있도록 맨위에 
/// </summary>
public class MngPopup : MonoBehaviour{
    public static MngPopup Instance;
    
    [SerializeField] public GameObject m_objPopup;

    [SerializeField, Header("알림 제목")] private TextMeshProUGUI m_textTitle;
    [SerializeField, Header("알림 내용")] private TextMeshProUGUI m_textValue;

    [SerializeField, Tooltip("보여줄 이미지")] private Image m_imgPopup;
    [SerializeField, Tooltip("확인 누르기용 ")] private Button m_btnPopup;

    //메시지 리스트
    private List<cPopup> m_listMessages = new List<cPopup>();
    [Tooltip("꺼지는 시간 조절")]public float m_fCloseTime = 1.0f;


    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(Instance);    
        }
        else{
            Destroy(Instance);
        }

    }

    // Start is called before the first frame update
    private void Start(){
        init();
    }

    private void init(){
        m_objPopup.SetActive(false);
        m_textTitle.text = string.Empty;
        m_textValue.text = string.Empty;

        setTextAlpaha(m_textTitle, 1.0f); // 글
        setTextAlpaha(m_textValue, 1.0f); // 글
        setImageAlpaha(m_imgPopup, 1.0f); // 이미지

        m_btnPopup.onClick.RemoveAllListeners();
        m_btnPopup.onClick.AddListener(() => {

            StartCoroutine(close());
        });


    }

    //(TEXT) 투명도 조절
    private void setTextAlpaha(TextMeshProUGUI _text, float _alpha){
        _alpha = Mathf.Clamp(_alpha, 0f, 1f); //최대 최소   
        Color color = _text.color;
        color.a = _alpha;
        _text.color = color;
    }

    //(Image) 투명도 조절
    private void setImageAlpaha(Image _img, float _alpha){
        _alpha = Mathf.Clamp(_alpha, 0f, 1f); //최대 최소   
        Color color = _img.color;
        color.a = _alpha;
        _img.color = color;

    }

    /// <summary>
    /// 내용을 리스트에 넣고 / 보여줌 
    /// </summary>
    /// <param name="_value"></param>
    public void ShowMessage(cPopup _value){
        m_listMessages.Add(_value);
        showMessage();
    }

    /// <summary>
    /// 실제 보여주고 보여준 뒤 내용을 삭제하는 코드
    /// </summary>
    private void showMessage(){
        if (m_listMessages.Count == 0){//0이면 리턴
            return;
        }

        cPopup current = m_listMessages[0];

        m_textTitle.text = current.Title;
        m_textValue.text = current.Value;

        setTextAlpaha(m_textTitle, 1.0f); // 글
        setTextAlpaha(m_textValue, 1.0f); // 글
        setImageAlpaha(m_imgPopup, 1.0f); // 이미지

        m_btnPopup.interactable = true;
        m_objPopup.SetActive(true);

    }

    IEnumerator close(){
        m_btnPopup.interactable = false;
        Color color = new Color(0, 0, 0, 1f);
        while (true){
            m_textTitle.color -= color * Time.deltaTime * (1/ m_fCloseTime); // 타이머는 1을 그시간동안 나누면됨
            m_textValue.color -= color * Time.deltaTime * (1 / m_fCloseTime);
            m_imgPopup.color -= color * Time.deltaTime * (1 / m_fCloseTime);

            if (m_imgPopup.color.a > 0){
                yield return null;
            }
            else{
                break;
            }
        }
        if(m_listMessages[0].Action != null){
            m_listMessages[0].Action.Invoke();
        }
        //내용 지워주기
        m_listMessages.RemoveAt(0);
        //끄기 
        m_objPopup.SetActive(false);
        showMessage();
    }
}

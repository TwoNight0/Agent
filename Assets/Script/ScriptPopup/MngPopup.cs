using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// notify message to player   
/// ���� : 
/// 1. UI_�׸��� �κп� PopUp�� �����
/// 2. TMP�� text �ΰ� button �ϳ��� ����� ���� title, value, btn_check �Ҵ�����
/// 4. Obj PopUp ���� �����״������� �Ⱥ��� ���ֵ��� ������ 
/// </summary>
public class MngPopup : MonoBehaviour{
    public static MngPopup Instance;
    
    [SerializeField] public GameObject m_objPopup;

    [SerializeField, Header("�˸� ����")] private TextMeshProUGUI m_textTitle;
    [SerializeField, Header("�˸� ����")] private TextMeshProUGUI m_textValue;

    [SerializeField, Tooltip("������ �̹���")] private Image m_imgPopup;
    [SerializeField, Tooltip("Ȯ�� ������� ")] private Button m_btnPopup;

    //�޽��� ����Ʈ
    private List<cPopup> m_listMessages = new List<cPopup>();
    [Tooltip("������ �ð� ����")]public float m_fCloseTime = 1.0f;


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

        setTextAlpaha(m_textTitle, 1.0f); // ��
        setTextAlpaha(m_textValue, 1.0f); // ��
        setImageAlpaha(m_imgPopup, 1.0f); // �̹���

        m_btnPopup.onClick.RemoveAllListeners();
        m_btnPopup.onClick.AddListener(() => {

            StartCoroutine(close());
        });


    }

    //(TEXT) ���� ����
    private void setTextAlpaha(TextMeshProUGUI _text, float _alpha){
        _alpha = Mathf.Clamp(_alpha, 0f, 1f); //�ִ� �ּ�   
        Color color = _text.color;
        color.a = _alpha;
        _text.color = color;
    }

    //(Image) ���� ����
    private void setImageAlpaha(Image _img, float _alpha){
        _alpha = Mathf.Clamp(_alpha, 0f, 1f); //�ִ� �ּ�   
        Color color = _img.color;
        color.a = _alpha;
        _img.color = color;

    }

    /// <summary>
    /// ������ ����Ʈ�� �ְ� / ������ 
    /// </summary>
    /// <param name="_value"></param>
    public void ShowMessage(cPopup _value){
        m_listMessages.Add(_value);
        showMessage();
    }

    /// <summary>
    /// ���� �����ְ� ������ �� ������ �����ϴ� �ڵ�
    /// </summary>
    private void showMessage(){
        if (m_listMessages.Count == 0){//0�̸� ����
            return;
        }

        cPopup current = m_listMessages[0];

        m_textTitle.text = current.Title;
        m_textValue.text = current.Value;

        setTextAlpaha(m_textTitle, 1.0f); // ��
        setTextAlpaha(m_textValue, 1.0f); // ��
        setImageAlpaha(m_imgPopup, 1.0f); // �̹���

        m_btnPopup.interactable = true;
        m_objPopup.SetActive(true);

    }

    IEnumerator close(){
        m_btnPopup.interactable = false;
        Color color = new Color(0, 0, 0, 1f);
        while (true){
            m_textTitle.color -= color * Time.deltaTime * (1/ m_fCloseTime); // Ÿ�̸Ӵ� 1�� �׽ð����� �������
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
        //���� �����ֱ�
        m_listMessages.RemoveAt(0);
        //���� 
        m_objPopup.SetActive(false);
        showMessage();
    }
}

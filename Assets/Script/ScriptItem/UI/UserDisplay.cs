using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDisplay : MonoBehaviour
{
    [SerializeField] private Image hp_cur;
    
    
    void Start()
    {
        hp_cur = transform.Find("Hp_Cur").GetComponentInChildren<Image>();
    }

    
    void Update()
    {
        hpUpdate();
    }

    private void hpUpdate()
    {
        hp_cur.fillAmount = PlayerMng.Instance.GetHp / PlayerMng.Instance.GetMaxHp;
    }


    //Icon.6_86 힐킷
    //Icon.6_26 궁극기(데마시아!)
    //Icon.6_98 메인무기
    //Icon.8_21보조무기



}

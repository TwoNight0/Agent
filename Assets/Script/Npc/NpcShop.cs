using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Npc sell or buy item 
/// </summary>
public class NpcShop : MonoBehaviour{

    public bool shopOn = false;


    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        shopisOn();
    }


    /// <summary>
    /// ���ΰ� ��ȣ�ۿ�(�����ɽ�Ʈ)�Ͽ� shop�� on�Ǹ� item������ ���� ������ �������� �������� ����� ������
    /// �÷��̾��� ��带 Ȯ�� -> ������ϴ� ������ ������ Ȯ��(������)popup ������ -> ������ �������Ǵ��� Ȯ�� -> (�ȵɽ�)popup ��������,
    /// 
    /// </summary>
    private void shopisOn() {
        if (shopOn) {
        
        }

    }

}

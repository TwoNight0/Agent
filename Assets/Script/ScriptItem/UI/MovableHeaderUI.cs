using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Ŭ���� ���� : UI�� �巡�� ��� �� �� �ְ�����
/// <summary>
/// usage : make header, and put in this script
/// and _targetTr is parent
/// </summary>
public class MovableHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler{
    [Header("�̵��� ������Ʈ")]
    [SerializeField] private Transform _targetTr;


    private Vector2 _beginPoint;//������
    private Vector2 _moveBegin;//������ ���� 

    private void Awake(){
        // �̵� ��� UI�� �������� ���� ���, �ڵ����� �θ�� �ʱ�ȭ
        if (_targetTr == null)
            _targetTr = transform.parent;
    }

    // �巡�� ���� ��ġ ����
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
        //_beginPoint = transform.position; //�ڱ� �ڽ������ϸ� �ڲ� �߰��� Ŭ���̵Ǽ� �ٲ�
        _beginPoint = transform.parent.position;
        _moveBegin = eventData.position;
        //Debug.Log("move : " + _moveBegin);
        //Debug.Log("begin2 : " + _beginPoint);
    }

    // �巡�� : ���콺 Ŀ�� ��ġ�� �̵�
    void IDragHandler.OnDrag(PointerEventData eventData){
        _targetTr.position = (eventData.position - (_moveBegin - _beginPoint));
    }

}


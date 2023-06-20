using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 클래스 설명 : UI를 드래그 드랍 할 수 있게해줌
/// <summary>
/// usage : make header, and put in this script
/// and _targetTr is parent
/// </summary>
public class MovableHeaderUI : MonoBehaviour, IPointerDownHandler, IDragHandler{
    [Header("이동할 오브젝트")]
    [SerializeField] private Transform _targetTr;


    private Vector2 _beginPoint;//시작점
    private Vector2 _moveBegin;//움직인 지점 

    private void Awake(){
        // 이동 대상 UI를 지정하지 않은 경우, 자동으로 부모로 초기화
        if (_targetTr == null)
            _targetTr = transform.parent;
    }

    // 드래그 시작 위치 지정
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
        //_beginPoint = transform.position; //자기 자신으로하면 자꾸 중간이 클릭이되서 바꿈
        _beginPoint = transform.parent.position;
        _moveBegin = eventData.position;
        //Debug.Log("move : " + _moveBegin);
        //Debug.Log("begin2 : " + _beginPoint);
    }

    // 드래그 : 마우스 커서 위치로 이동
    void IDragHandler.OnDrag(PointerEventData eventData){
        _targetTr.position = (eventData.position - (_moveBegin - _beginPoint));
    }

}


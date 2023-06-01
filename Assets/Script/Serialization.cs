using System;
using System.Collections.Generic;
using UnityEngine;

// Jason을 사용할때 이 클래스를 이용해서 다른 오브젝트를 직렬화 할수있음

[Serializable] //클래스, 구조체, enum, 델리게이트 에만 적용가능
public class Serialization<T>{
    [SerializeField]
    private List<T> target;
    public Serialization(List<T> _value){
        target = _value;
    }

    public List<T> ToList(){
        return target;
    }
}

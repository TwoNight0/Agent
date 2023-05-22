using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] //클래스, 구조체, enum, 델리게이트 에만 적용가능
public class Serialization<T>
{
    [SerializeField]
    private List<T> target;
    public Serialization(List<T> _value)
    {
        target = _value;
    }

    public List<T> ToList()
    {
        return target;
    }
}

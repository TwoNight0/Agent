using System;
using System.Collections.Generic;
using UnityEngine;

// Jason�� ����Ҷ� �� Ŭ������ �̿��ؼ� �ٸ� ������Ʈ�� ����ȭ �Ҽ�����

[Serializable] //Ŭ����, ����ü, enum, ��������Ʈ ���� ���밡��
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

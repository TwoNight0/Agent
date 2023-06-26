using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Npc sell or buy item 
/// </summary>
public class NpcShop : MonoBehaviour{



    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Buy_shop();
    }


    /// <summary>
    /// 상인과 상호작용(레이케스트)하여 shop이 on되면 item고유의 값인 가격을 바탕으로 아이템을 사고팔 수있음
    /// 플레이어의 골드를 확인 -> 사려고하는 물건의 가격을 확인(부족시)popup 돈부족 -> 공간적 여유가되는지 확인 -> (안될시)popup 공간부족,
    /// 
    /// </summary>
    private void Buy_shop() {
        

    }

}

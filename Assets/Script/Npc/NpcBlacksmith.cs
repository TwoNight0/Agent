using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NpcBlacksmith : MonoBehaviour{
    private EventSystem m_eventSystem;
    int[] itemarray = new int[4];
    ItemSlotScript firstItemSlot = null;
    ItemSlotScript secondItemSlot = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 아이템슬롯에 아이템을 넣으면
    /// </summary>
    private void inputarray(){
        PointerEventData pointEvent = new PointerEventData(m_eventSystem);
    }


}

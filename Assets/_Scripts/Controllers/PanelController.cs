using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public void CheckAllSlots()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            SlotInfo currentSlot = null; 
            if(transform.GetChild(i).TryGetComponent(out currentSlot))
            {
                currentSlot.CheckStatus();
            }
        }
    }
}

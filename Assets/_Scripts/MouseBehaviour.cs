using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class MouseBehaviour : MonoBehaviour
{

    bool IsDown;


    public Action OnMouseDownEvent;
    public Action<bool> OnMouseOverEvent;

    private void OnMouseDown()
    {
        IsDown = true;
        if (OnMouseDownEvent != null)
        {
            OnMouseDownEvent.Invoke();
        }


    }
    void OnMouseOver()
    {
        if (IsDown == false)
        {
            if (OnMouseOverEvent != null)
            {
                OnMouseOverEvent.Invoke(true);
            }
        }

    }

    void OnMouseExit()
    {
        if (IsDown == false)
        {
            if (OnMouseOverEvent != null)
            {
                OnMouseOverEvent.Invoke(false);
            }
        }

    }
}

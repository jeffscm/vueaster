using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectComponent : MonoBehaviour, IDragHandler
{

    private int _current = 0;

    public int maxEggs = 7;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.delta.x);
        if (eventData.delta.x > 0.1)
        {
            MoveEgg(1);
        }
    }

    private void MoveEgg(int direction)
    {
        var temp = direction + _current;

        if (temp < 0)
        {
            temp = maxEggs - 1;
        }
        else if (temp >= maxEggs)
        {
            temp = 0;
        }
        _current = temp;


    }
}

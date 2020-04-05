using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ViewDragComponent : MonoBehaviour, IDragHandler
{
    public Transform target;
    private Vector3 _euler = Vector3.zero;
    public void OnDrag(PointerEventData eventData)
    {
        _euler.y -= eventData.delta.x;
        _euler.x += eventData.delta.y / 2f;
        _euler.x = Mathf.Clamp(_euler.x, -35f, 35f);
        target.localEulerAngles = _euler;
    }

  
}

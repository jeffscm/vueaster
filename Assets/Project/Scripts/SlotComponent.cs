using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotComponent : MonoBehaviour
{

    public static Action<int> OnSlotClicked;

    public GameObject hasEggCheckmark;
    public int idxSlot;

    private void OnEnable()
    {
        hasEggCheckmark.SetActive(FilesService.Available[idxSlot]);
    }

    public void OnClick()
    {
        OnSlotClicked?.Invoke(idxSlot);
    }
}

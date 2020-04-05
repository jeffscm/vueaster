using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AUDIO { CLICK = 0, DOWNLOAD, WIN, NEWEGG };

public enum PANEL {MENU, VIEWEGG, PAINT, SELECTSLOT };
public enum SELECTIONACTION { NONE, VIEW, PAINT, SAVE, DOWNLOAD, MENU, MOVEEGG, SETEGGCOLOR, DOWNLOAD_ART };
public class UIController : MonoBehaviour
{
    public static Action<SELECTIONACTION, int> OnMainAction;
    public GameObject[] listPanels;
    private SELECTIONACTION _selectionAction = SELECTIONACTION.NONE;
    private int _slotSelected = -1;

    public GameObject fadePanel, hideShot;
    public GameObject firstTimeInstructions;
    public GameObject messageBox;
    public Text messageText;
    public AudioClip[] audioList;
    public AudioSource audioSource;

    public Image soundIcon;
    public Sprite[] soundSprites;


    private int _showIdxEgg = 0;
    // Start is called before the first frame update
    void Start()
    {

        var temp = PlayerPrefs.GetString("SOUND", "ON");
        AudioListener.volume = (temp == "ON") ? 1f : 0f;
        soundIcon.sprite = soundSprites[(temp == "ON") ? 0 : 1];


        firstTimeInstructions.SetActive(false);
        messageBox.SetActive(false);
        fadePanel.SetActive(false);

        SlotComponent.OnSlotClicked += (slotIdx) => {
            _slotSelected = slotIdx;
            switch (_selectionAction)
            {
                //case SELECTIONACTION.PAINT:
                //    firstTimeInstructions.SetActive(true);
                //    ShowPanel(PANEL.PAINT);
                //    OnMainAction?.Invoke(SELECTIONACTION.PAINT, _slotSelected);
                //    PlaySound(AUDIO.NEWEGG);
                //    break;
                //case SELECTIONACTION.VIEW:
                //    ShowPanel(PANEL.VIEWEGG);
                //    OnMainAction?.Invoke(SELECTIONACTION.VIEW, _slotSelected);
                //    PlaySound(AUDIO.CLICK);
                //    break;
                case SELECTIONACTION.NONE:
                    ShowPanel(PANEL.MENU);
                    PlaySound(AUDIO.CLICK);
                    break;
            }                    
        };
    }

    public void OnClickNewEgg()
    {
        _slotSelected = _showIdxEgg;
        firstTimeInstructions.SetActive(true);
        ShowPanel(PANEL.PAINT);
        OnMainAction?.Invoke(SELECTIONACTION.PAINT, _slotSelected);
        PlaySound(AUDIO.NEWEGG);

        //_selectionAction = SELECTIONACTION.PAINT;
        //ShowPanel(PANEL.SELECTSLOT);
        //PlaySound(AUDIO.CLICK);
    }

    public void OnClickViewEgg()
    {
        _slotSelected = _showIdxEgg;
        ShowPanel(PANEL.VIEWEGG);
        OnMainAction?.Invoke(SELECTIONACTION.VIEW, _slotSelected);
        PlaySound(AUDIO.CLICK);

        //_selectionAction = SELECTIONACTION.VIEW;
        //ShowPanel(PANEL.SELECTSLOT);
        //PlaySound(AUDIO.CLICK);
    }

    public void OnClickBackToStart()
    {
        ShowPanel(PANEL.MENU);
        PlaySound(AUDIO.CLICK);
    }

    public void OnClickDownloadEgg()
    {
        hideShot.SetActive(false);
        OnMainAction?.Invoke(SELECTIONACTION.DOWNLOAD, _slotSelected);        
        PlaySound(AUDIO.DOWNLOAD);
        Invoke(nameof(UnhideShot), 0.25f);
    }

    void UnhideShot()
    {
        ShowMessage("Egg Image Downloaded!");
        hideShot.SetActive(true);
    }

    public void OnClickSoundOnOff()
    {
        SetSound();
        PlaySound(AUDIO.CLICK);
    }

    public void OnClickSaveEgg()
    {        
        ShowPanel(PANEL.MENU);
        OnMainAction?.Invoke(SELECTIONACTION.SAVE, _slotSelected);
        ShowMessage("New Egg Added to Basket");
        PlaySound(AUDIO.WIN);
    }

    public void OnClickEggToRight()
    {
        _showIdxEgg++;
        if (_showIdxEgg >= 7)
            _showIdxEgg = 0;
        OnMainAction?.Invoke(SELECTIONACTION.MOVEEGG, _showIdxEgg);
        PlaySound(AUDIO.CLICK);
    }
    public void OnClickEggToLeft()
    {
        _showIdxEgg--;
        if (_showIdxEgg < 0)
            _showIdxEgg = 6;
        OnMainAction?.Invoke(SELECTIONACTION.MOVEEGG, _showIdxEgg);
        PlaySound(AUDIO.CLICK);
    }

    public void OnClickViewEggColor(int idx)
    {
        OnMainAction?.Invoke(SELECTIONACTION.SETEGGCOLOR, idx);
        PlaySound(AUDIO.CLICK);
    }

    public void OnClickDownloadArt()
    {
        Application.OpenURL("https://paralagames.public.cloudvps.com/media/easter.jpg");
    }

    public void OnClickDownloadVideo()
    {
        Application.OpenURL("https://paralagames.public.cloudvps.com/media/easterhelp.mp4");
    }

    //Panels ------------------

    private void ShowPanel(PANEL panel)
    {
        if (panel == PANEL.MENU) OnMainAction?.Invoke(SELECTIONACTION.MENU, _slotSelected);
        for (int i = 0; i < listPanels.Length; i++)
        {
            listPanels[i].SetActive((i == (int)panel));
        }
    }

    private void ShowMessage(string text)
    {
        messageText.text = text;
        messageBox.SetActive(true);
        CancelInvoke(nameof(DelayClose));
        Invoke(nameof(DelayClose), 3f);
    }

    private void DelayClose()
    {
        messageBox.SetActive(false);
    }

    private void PlaySound(AUDIO sound)
    {
        audioSource.PlayOneShot(audioList[(int)sound]);
    }

    private void SetSound()
    {
        var sound = !(PlayerPrefs.GetString("SOUND", "ON") == "ON");
        PlayerPrefs.SetString("SOUND", (sound) ? "ON" : "OFF");
        AudioListener.volume = (sound) ? 1f : 0f;
        soundIcon.sprite = soundSprites[sound ? 0 : 1];
    }
}

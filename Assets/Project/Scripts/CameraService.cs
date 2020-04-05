using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NatShare;


public class CameraService : MonoBehaviour
{
    public static Action<int> OnNewEggImage;
    public GameObject menuSceneItemsBase;
    public Transform cameraMenu;
    public Renderer viewEggRender;
    public RenderTextureCamera renderTexture;

    public Transform baseMoveEggs;

    private float[] _paramMetal = { 0.25f, 0f, 0.7f};
    private float[] _paramSmooth = { 0.7f, 0f, 0.7f };

    // Start is called before the first frame update
    void Start()
    {
        UIController.OnMainAction += (newAction, idx) =>
        {
            switch(newAction)
            {
                case SELECTIONACTION.MENU:
                    AlignCameraMenu(0f);
                    SetMenuObjectsActive(true);
                    break;
                case SELECTIONACTION.VIEW:
                    AlignCameraMenu(180f);
                    MapViewEgg(idx);
                    break;
                case SELECTIONACTION.PAINT:
                    SetMenuObjectsActive(false);
                    break;
                case SELECTIONACTION.DOWNLOAD:
                    SaveScreen();
                    break;
                case SELECTIONACTION.SAVE:
                    renderTexture.MakeScreenWithPath(Application.persistentDataPath + "/egg" + idx + ".png", () => {
                        OnNewEggImage?.Invoke(idx);
                    });
                    FilesService.Available[idx] = true;
                    
                    break;
                case SELECTIONACTION.MOVEEGG:
                    AnimageMoveEggs(idx);
                    break;
                case SELECTIONACTION.SETEGGCOLOR:

                    viewEggRender.material.SetFloat("_Metallic", _paramMetal[idx]);
                    viewEggRender.material.SetFloat("_Glossiness", _paramSmooth[idx]);

                    break;

            }
        };
    }

    private void AlignCameraMenu(float degress)
    {
        cameraMenu.localEulerAngles= new Vector3(6, degress, 0);
    }

    private void SetMenuObjectsActive(bool active)
    {
        menuSceneItemsBase.SetActive(active);
        //renderTexture.gameObject.SetActive(!active);
    }

    private void MapViewEgg(int idx)
    {
        var tempTex2D = new Texture2D(32, 32);
        var data = FilesService.GetImage(idx: idx);
        if (data != null)
        {
            tempTex2D.LoadImage(data, true);            
        }
        else
        {
            tempTex2D = Resources.Load("whiteimg") as Texture2D;
        }
        viewEggRender.material.SetTexture("_MainTex", tempTex2D);
    }

    private void SaveScreen()
    {
        var screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        using (var payload = new SavePayload())
        {
            payload.AddImage(screenShot);
        }
    }

    private void AnimageMoveEggs(int idx)
    {
        LeanTween.cancel(baseMoveEggs.gameObject);
        LeanTween.moveLocalX(baseMoveEggs.gameObject, idx * -3f, 1f).setEaseOutBounce();
    }
}

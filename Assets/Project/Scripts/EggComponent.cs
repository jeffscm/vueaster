using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggComponent : MonoBehaviour
{
    public int thisEggIdx;
    public Renderer render;
    // Start is called before the first frame update
    void Start()
    {
        CameraService.OnNewEggImage += (idx) =>
        {
            if (thisEggIdx == idx)
            {
                Debug.Log("Got Here");
                GetImage();
            }
        };

        GetImage();
    }

    private void GetImage()
    {
        var data = FilesService.GetImage(thisEggIdx);
        if (data != null)
        {
            var tempTex2D = new Texture2D(32, 32);
            tempTex2D.LoadImage(data, true);
            render.material.SetTexture("_MainTex", tempTex2D);
        }
    }
}

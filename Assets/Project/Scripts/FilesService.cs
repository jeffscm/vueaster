using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilesService : MonoBehaviour
{

    public static Action<int> OnSlotAvailable;
    public static bool[] Available = { false, false, false, false, false, false, false };
    static string path;
    // Start is called before the first frame update
    void Awake()
    {
        path = Application.persistentDataPath + "/egg";
       
        for(int i = 0; i < 7; i++)
        {
            if (File.Exists(path + i + ".png"))
            {
                Available[i] = true;
            }
        }
    }

    public static void SaveImage(int idx, byte[] data)
    {
        File.WriteAllBytes(path + idx + ".png", data);
    }

    public static byte[] GetImage(int idx)
    {
        if (File.Exists(path + idx + ".png"))
        {
            Debug.Log(path + idx + ".png");
            return File.ReadAllBytes(path + idx + ".png");
        }
        return null;
    }


}

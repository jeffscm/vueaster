using UnityEngine;

[ExecuteInEditMode]
public class SetAutoOrthographicSize : MonoBehaviour
{

    public float factor;

    void Update()
    {
        if (GetComponent<Camera>())
        {
            GetComponent<Camera>().orthographicSize = transform.lossyScale.y * factor;
        }
    }
}
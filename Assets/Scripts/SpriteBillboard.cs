using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    public Camera camera;
    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f);
    }
}

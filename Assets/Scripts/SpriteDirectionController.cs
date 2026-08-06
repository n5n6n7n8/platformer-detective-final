using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDirectionController : MonoBehaviour
{
    [SerializeField] float backAngle = 65f;
    [SerializeField] float sideAngle = 155f;
    [SerializeField] private Transform mainTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void LateUpdate()
    {
        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);
        float angle = Mathf.Abs(signedAngle);

        // if(angle < backAngle)
        // {
        //     //change to back animation
        // }
        // else if(angle < sideAngle)
        // {
        //     //change to side animation
        // }
        // else
        // {
        //     //change to front animation
        // }
    }
}

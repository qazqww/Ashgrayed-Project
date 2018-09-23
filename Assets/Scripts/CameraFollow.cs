using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 100f; //카메라 지연

    private static bool cameraExists;

    Vector3 offset;

    private void Awake()
    {
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); // 부드럽게 이동
    }

    private void Update()
    {
        try
        {
            target = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }
        catch
        {
            target = FindObjectOfType<PlayerTemp>().gameObject.transform;
        }
    }
}

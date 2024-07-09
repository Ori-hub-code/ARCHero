using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : Singleton<CameraMove>
{
    public GameObject player;

    public float offsetY;
    public float offsetZ;

    public Vector3 cameraPos;

    private void Awake()
    {
        cameraPos.x = player.transform.position.x;  
    }

    private void LateUpdate()
    {
        cameraPos.y = player.transform.position.y + offsetY;
        cameraPos.z = player.transform.position.z + offsetZ;

        transform.position = cameraPos;
    }
}

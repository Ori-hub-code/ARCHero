using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;

    public float offsetY;
    public float offsetZ;

    Vector3 cameraPos;

    private void LateUpdate()
    {
        cameraPos.x = player.transform.position.x;
        cameraPos.y = player.transform.position.y + offsetY;
        cameraPos.z = player.transform.position.z + offsetZ;

        transform.position = cameraPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerLine : MonoBehaviour
{
    TrailRenderer trail;
    public Vector3 endPostion;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();

        trail.startColor = new Color(1, 0, 0, 0.7f);
        trail.endColor = new Color(1, 0, 0, 0.7f);
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPostion, Time.deltaTime * 3.5f);
    }
}

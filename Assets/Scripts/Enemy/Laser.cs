using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject laser; // 2 초 후 발사할 진짜 레이저
    public LayerMask layerMask;

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.enabled = false;
        hitEffect.SetActive(false);
    }

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, layerMask);

            if (hit.transform != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                hitEffect.SetActive(true);
                hitEffect.transform.position = hit.point;

                if(hit.transform.CompareTag("Player"))
                {
                    Debug.Log("hit player");
                    PlayerHpBar.Instance.currentHp -= Time.deltaTime * 250f;
                }
            }
            else
            {
                hitEffect.SetActive(false);
            }
        }
    }
}

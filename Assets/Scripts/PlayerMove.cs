using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    Rigidbody rigid;

    [Header("# Move")]
    [SerializeField] float hAxis;
    [SerializeField] float vAxis;
    [SerializeField] float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        rigid.velocity += new Vector3(hAxis, 0, vAxis).normalized * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : Singleton<JoyStick>
{
    public GameObject smallStick;
    public GameObject bgStick;
    Vector3 joyStickFirsPos;
    Vector3 stickFirstPos;
    public Vector3 joyVec;
    float stickRadius;

    private void Awake()
    {
        stickRadius = bgStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        stickFirstPos = smallStick.transform.position;
        joyStickFirsPos = bgStick.transform.position;
    }

    // ��ġ�� �Ǿ��� ��
    public void PointDown()
    {
        bgStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPos = Input.mousePosition;
    }

    // �巡�װ� �Ǿ��� ��
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPos).normalized; // �巡�װ� �ǰ� �ִ� ��ġ - ó�� ��ġ ��ġ; -> �巡�� ����

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPos);

        // ���� ���̽�ƽ�� ��� �������� �����̰� ���� ����
        if(stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPos + joyVec * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPos + joyVec * stickRadius;
        }
    }

    // ����� �Ǿ��� ��
    public void Drop()
    {
        joyVec = Vector3.zero;
        bgStick.transform.position = joyStickFirsPos; // ��ġ�� �ʱ�ȭ
        smallStick.transform.position = joyStickFirsPos; // ��ġ�� �ʱ�ȭ
    }
}

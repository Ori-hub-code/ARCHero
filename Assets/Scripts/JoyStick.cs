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

    // 터치가 되었을 때
    public void PointDown()
    {
        bgStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPos = Input.mousePosition;
    }

    // 드래그가 되었을 때
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPos).normalized; // 드래그가 되고 있는 위치 - 처음 터치 위치; -> 드래그 방향

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPos);

        // 작은 조이스틱이 배경 위에서만 움직이게 범위 설정
        if(stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPos + joyVec * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPos + joyVec * stickRadius;
        }
    }

    // 드랍이 되었을 때
    public void Drop()
    {
        joyVec = Vector3.zero;
        bgStick.transform.position = joyStickFirsPos; // 위치값 초기화
        smallStick.transform.position = joyStickFirsPos; // 위치값 초기화
    }
}

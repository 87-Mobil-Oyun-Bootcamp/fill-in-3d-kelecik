using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mouseStartPos;
    Vector3 mouseCurrentPos;
    Vector3 movePos;

    public float sensitive;

    private void Update()
    {
        SwipeMove();
       // Debug.Log("mouseStartPos :" + mouseStartPos);
       // Debug.Log("mouseCurrentPos :" + mouseCurrentPos);
       //Debug.Log("movePos :" + movePos);
    }

    private void FixedUpdate()
    {

        PlayerMove();
    }
    void SwipeMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = new Vector3(MousePosCenter().x, 0, MousePosCenter().y);
        }

        if (Input.GetMouseButton(0))
        {
            mouseCurrentPos = new Vector3(MousePosCenter().x, 0, MousePosCenter().y) + mouseStartPos;
        }
    }
    void PlayerMove()
    {
        transform.position = mouseCurrentPos * Time.deltaTime;
    }

    private Vector3 MousePosCenter()
    {
        Vector3 pos = Input.mousePosition;
        return pos;
    }
}

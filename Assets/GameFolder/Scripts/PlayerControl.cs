using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float vertical, horizontal;
    public int speed;
    public Joystick joystick;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(GameManager.instance.gameStatus == GameStatus.PLAYING)
        {
            Movement();
        }
    }
    void Movement()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;

        if (vertical != 0 || horizontal != 0)
        {
            transform.forward = new Vector3(horizontal * speed, 0, vertical * speed) * Time.deltaTime;
            rb.velocity = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        }
    }
}

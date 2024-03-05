using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersMovement : MonoBehaviour
{
    public float speed = 1f;
    public float dir = 1f;
    private bool canSwitch = true;
    public float switchCooldown = 1f;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) Movement();
    }

  

    private void Movement()
    {
        transform.position += new Vector3(1, 0, 0) * speed * dir * Time.deltaTime;
    }

    public void SwitchDir()
    {
        if (canSwitch)
        {
            dir *= -1;
            canSwitch = false;
            Invoke("EnableSwitch", switchCooldown);
            GoDown();
        }
    }
    private void GoDown()
    {
        transform.position += new Vector3(0, -1, 0);
    }

    private void EnableSwitch()
    {
        canSwitch = true;
    }
}

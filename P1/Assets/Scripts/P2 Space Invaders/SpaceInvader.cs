using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum invaderType {SQUID, CRAB, OCTOPUS};

public class SpaceInvader : MonoBehaviour
{
    public invaderType tipo = invaderType.SQUID;
    public Animator animator;
    public float speed = 1f;
    public float dir = 1f;
    public float moveTime = 3f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > moveTime)
        {
            dir *= -1;
            timer = 0f;
            GoDown();
        }
        Movement();
    }

    private void Movement()
    {
        transform.position += new Vector3(1, 0, 0) * speed * dir * Time.deltaTime;
    }

    private void GoDown()
    {
        transform.position += new Vector3(0, -2, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePlayerBullet : MonoBehaviour
{
    public float speed = 1f;
    public SpacePlayer player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed, 0) * speed * Time.deltaTime;
    }
    private void OnDestroy()
    {
        player.canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SInvader")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.tag == "SBarrier")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.tag == "SBorder")
        {
            Destroy(gameObject);
        }
    }
}


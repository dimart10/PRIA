using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePlayerBullet : MonoBehaviour
{
    public float speed = 1f;
    public SpacePlayer player;
    public GameObject bulletExplosion;

    private SoundManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = SoundManager.instance;   
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
        if(collision.tag == "SBarrier")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        }
        else if (collision.tag == "SBorder")
        {
            Destroy(gameObject);
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        }
    }
}


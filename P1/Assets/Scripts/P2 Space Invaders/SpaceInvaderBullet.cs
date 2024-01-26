using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvaderBullet : MonoBehaviour
{

    public float speed = 1f;
    public GameObject bulletExplosion;
    public AudioClip bulletDestroyedSFX;

    private SoundManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = SoundManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, speed, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SpaceGameManager.instance.DamagePlayer();
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.tag == "SBarrier")
        {
            Destroy(collision.gameObject);
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(collision.tag == "SBorder")
        {
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Choque de balas");
        if (collision.gameObject.tag == "SPlayerBullet")
        {
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}

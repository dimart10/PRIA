using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvaderBullet : MonoBehaviour
{

    public float speed = 1f;

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
            Destroy(gameObject);
        }
        else if (collision.tag == "SBarrier")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.tag == "SBorder")
        {
            Destroy(gameObject);
        }
    }
}

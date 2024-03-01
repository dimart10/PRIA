using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInvaderBullet : MonoBehaviour
{
    public float speed = 3f;
    public GameObject bulletExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SBorder")
        {
            Destroy(gameObject);
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        }

        else if (collision.tag == "Player")
        {
            SGameManager.instance.DamagePlayer();
            Destroy(gameObject);
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        }
        else if (collision.tag == "SPlayerBullet")
        {
            Destroy(gameObject); // Destruyo esta bala
            Destroy(collision.gameObject); // Destruyo bala jugador
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);
        }
        else if (collision.tag == "SBarrier") // Choca con una barrera 
        {
            Destroy(gameObject); // Destruyo bala
            Destroy(collision.gameObject); // Destruyo barrera
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInvaderBullet : MonoBehaviour
{
    public float speed = 3f;


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
        }

        else if (collision.tag == "Player")
        {
            // GameManager.instance.dañaJugador p.ej.
            Debug.Log("Daño al player");
            Destroy(gameObject);
        }
        else if (collision.tag == "SPlayerBullet")
        {
            Destroy(gameObject); // Destruyo esta bala
            Destroy(collision.gameObject); // Destruyo bala jugador
        }
    }
}

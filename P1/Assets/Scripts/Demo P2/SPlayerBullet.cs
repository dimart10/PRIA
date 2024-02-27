using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SPlayerBullet : MonoBehaviour
{
    public float speed = 3;
    public SPlayer player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed, 0) * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SBorder") // Choca con una barrera
        {
            Destroy(gameObject); // Se destruye la bala
        }
        else if (collision.tag == "SInvader") // Choca con un alien
        {
            Destroy(gameObject); // Destruyo bala
            Destroy(collision.gameObject); // Desruyo alien
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Bala Destruida");
       
        // El jugador puede volver a disparar
        player.canShoot = true;
    }
}

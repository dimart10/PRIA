using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SPlayerBullet : MonoBehaviour
{
    public float speed = 3;
    [HideInInspector]
    public SPlayer player;
    public GameObject bulletExplosion;


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
            Instantiate(bulletExplosion, transform.position, Quaternion.identity); // Crea efecto de explosion
        }
        else if (collision.tag == "SInvader") // Choca con un alien
        {
            Destroy(gameObject); // Destruyo bala
            Destroy(collision.gameObject); // Desruyo alien
            SGameManager.instance.AlienDestroyed(); // Aviso al game manager de que se ha destruido un alien
        }
        else if(collision.tag == "SBarrier") // Choca con una barrera 
        {
            Destroy(gameObject); // Destruyo bala
            Destroy(collision.gameObject); // Destruyo barrera
            Instantiate(bulletExplosion, transform.position, Quaternion.identity); // Crea efecto de explosion
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Bala Destruida");
       
        // El jugador puede volver a disparar
        player.canShoot = true;
    }
}

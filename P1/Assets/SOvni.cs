using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOvni : MonoBehaviour
{
    public float speed = 3f; // Velocidad a la que se mueve
    public int points = 100; // Puntos que da al derrotarlo
    public int dir = 1; // Dirección del ovni (1 -> derecha, -1 -> izquierda)
    public float deathAnimTime = 1f; // Tiempo que hace la animación de muerte
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Desplazamiento horizontal
        transform.position += new Vector3(speed, 0, 0) * dir * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SBorder") // Borde de la pantalla
        {
            Destroy(gameObject); // Se destruye
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SPlayerBullet")
        {
            Destroy(collision.gameObject); // Destruyo la bala
            SGameManager.instance.AddScore(points); // Sumar puntos
            // Animacíon de destruirse
            animator.Play("OVNI_Death");
            speed = 0;

            Destroy(gameObject, deathAnimTime); // Destruyo el ovni en un tiempo
        }
    }

}

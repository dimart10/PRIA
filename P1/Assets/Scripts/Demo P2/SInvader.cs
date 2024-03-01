using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInvader : MonoBehaviour
{
    public GameObject particulaMuerte;
    public bool isQuitting = false;
    public SinvaderMovement padre;

    public GameObject invaderBullet;

    public float bulletSpawnYOffset = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot() // El alien dispara una bala
    {
        Vector3 aux = transform.position + new Vector3(0, bulletSpawnYOffset, 0); // Modificar posición spawn
        Instantiate(invaderBullet, aux, Quaternion.identity); // Spawnear la bala
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colision alien");
        if(collision.tag == "SBorder") // Choca con borde de pantalla
        {
            Debug.Log("Colision con borde");
            // LLamar a SwitchDirection para que se gire el padre
            padre.SwitchDirection();
        }
        else if(collision.gameObject.layer == 8)
        {
            SGameManager.instance.PlayerGamerOver();
        }
    }

    public void OnApplicationQuit() // Se llama al cerrar la aplicación, antes del OnDestroy
    {
        isQuitting = true;
    }


    private void OnDestroy() // Se llama al destruir el objeto
    {

        if (isQuitting == false) {
            GameObject particula = Instantiate(particulaMuerte, transform.position, Quaternion.identity);
            // Destroy(particula, 0.3f); Destruimos la particula dentro de 0,3f segundos
        }
    }
}

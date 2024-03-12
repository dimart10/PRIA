using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InvaderType {SQUID, CRAB, OCTOPUS};

public class SInvader : MonoBehaviour
{
    public InvaderType tipo = InvaderType.SQUID;

    public GameObject particulaMuerte;
    public bool isQuitting = false;
    public SinvaderMovement padre;

    public GameObject invaderBullet;

    public float bulletSpawnYOffset = -0.5f;

    public int puntosGanados = 10;

    public static int speed = 3;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            // LLamar a SwitchDirection para que se gire el padre
            padre.SwitchDirection();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("SGameOverBarrier"))
        {
            SGameManager.instance.PlayerGameOver();
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
            // Stun a los aliens (movimiento)
            padre.AlienDestroyedStun();
            // Suma puntos
            SGameManager.instance.AddScore(puntosGanados);
        }
    }

    public void MovementAnimation()
    {
        // Reproduce la animación idle según el tipo
        if(tipo == InvaderType.SQUID)
        {
            animator.Play("alien_1_idle");
        }
        else if(tipo == InvaderType.CRAB)
        {
            animator.Play("alien_2_idle");
        }
        else if (tipo == InvaderType.OCTOPUS)
        {
            animator.Play("alien_3_idle");
        }

        // animator.Play("alien_" + ((int)tipo+1).ToString() + "_idle");
    }

    public void StunAnimation()
    {
        animator.Play("alien_" + ((int)tipo + 1).ToString() + "_stun");
    }
}

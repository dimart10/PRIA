using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SPlayer : MonoBehaviour
{

    [Tooltip("Prefab de la bala")]
    public GameObject prefabBullet;

    [Tooltip("Velocidad del jugador en unidades de unity / segundo")]
    public float speed = 2; // Velocidad del jugador

    // Teclas para input configurable
    public KeyCode shootKey = KeyCode.Space;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    public Transform posDisparo;

    public bool canShoot = true;
    private bool canMove = true;

    // animator del jugador
    private Animator pAnimator;
    private Vector3 posInicial;

    // Valores límite de desplazamiento lateral
    public float limiteHorizontal = 8f;


    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        pAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            InputPlayer();
        }
    }

    private void InputPlayer()
    {
        if (canShoot && Input.GetKeyDown(shootKey))
        {
            // DISPARA
            Shoot();
        }
        else if (Input.GetKey(moveLeftKey))
        {
            // Voy a la izquierda
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            // Comprobar límites
            if(transform.position.x < -limiteHorizontal)
            {
                // Si me paso, lo pongo en la posición límite
                Vector3 aux = transform.position;
                aux.x = -limiteHorizontal;
                transform.position = aux;
            }
        }
        else if (Input.GetKey(moveRightKey))
        {
            // Voy a la derecha
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            // Comprobar límites
            if (transform.position.x > limiteHorizontal)
            {
                // Si me paso me pongo en la posición límite
                Vector3 aux = transform.position;
                aux.x = limiteHorizontal;
                transform.position = aux;
            }
        }
    }

    private void Shoot()
    {
        GameObject aux = Instantiate(prefabBullet, posDisparo.position, Quaternion.identity);
        SPlayerBullet bullet = aux.GetComponent<SPlayerBullet>();
        bullet.player = this;
        canShoot = false;
    }

    public void PlayerDamaged()
    {
        pAnimator.Play("player_death");
        canMove = false;
    }

    public void PlayerReset()
    {
        pAnimator.Play("player_idle");
        canMove = true;
        transform.position = posInicial;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool b)
    {
        canMove = b;
    }
}

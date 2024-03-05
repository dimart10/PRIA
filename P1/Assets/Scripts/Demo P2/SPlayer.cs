using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        }
        else if (Input.GetKey(moveRightKey))
        {
            // Voy a la derecha
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePlayer : MonoBehaviour
{
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode shootKey = KeyCode.Space;
    public float playerSpeed = 1f;
    public GameObject bulletPrefab;
    public bool canShoot = true;
    public AudioClip shootSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
    }

    private void InputPlayer()
    {
        if (Input.GetKey(leftKey)){
            this.transform.Translate(new Vector3(-playerSpeed, 0, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(rightKey))
        {
            this.transform.Translate(new Vector3(playerSpeed, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKeyDown(shootKey))
        {
            if (canShoot)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("DISPARO PLAYER");
        SpacePlayerBullet aux = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<SpacePlayerBullet>();
        aux.player = this;
        canShoot = false;
        //SoundManager.instance.PlaySFX(shootSFX);
    }
}

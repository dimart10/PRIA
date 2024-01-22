using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum invaderType {SQUID, CRAB, OCTOPUS};

public class SpaceInvader : MonoBehaviour
{
    public invaderType tipo = invaderType.SQUID;
    public Animator animator;
    public SpaceInvadersMovement father;
    public GameObject deathParticle;
    public GameObject invaderBullet;
    public float shotDistance = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SBorder")
        {
            father.SwitchDir();
        }
    }

    private void OnDestroy()
    {
        Instantiate(deathParticle,transform.position, Quaternion.identity);
    }

    public void Shoot()
    {
        Vector3 aux = transform.position;
        aux.y -= shotDistance;
        Instantiate(invaderBullet, aux, Quaternion.identity);
    }

}

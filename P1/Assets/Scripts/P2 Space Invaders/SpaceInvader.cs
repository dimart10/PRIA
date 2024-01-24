using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum invaderType {OCTOPUS, CRAB, SQUID};

public class SpaceInvader : MonoBehaviour
{
    public invaderType tipo = invaderType.SQUID;
    public Animator animator;
    public SpaceInvadersMovement father;
    public GameObject deathParticle;
    public GameObject invaderBullet;
    public float shotDistance = 0.5f;
    private bool quitting = false;
    public AudioClip deadInvaderSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SBorder")
        {
            father.SwitchDir();
        }
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void OnDestroy()
    {
        if (!quitting)
        {
            SoundManager.instance.PlaySFX(deadInvaderSFX);
            SpaceGameManager.instance.AddScore(((int)tipo + 1) * 10);
            SpaceGameManager.instance.UpdateTimeScale();
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
    }

    public void Shoot()
    {
        Vector3 aux = transform.position;
        aux.y -= shotDistance;
        Instantiate(invaderBullet, aux, Quaternion.identity);
    }
}

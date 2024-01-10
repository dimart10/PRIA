using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum invaderType {SQUID, CRAB, OCTOPUS};

public class SpaceInvader : MonoBehaviour
{
    public invaderType tipo = invaderType.SQUID;
    public Animator animator;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

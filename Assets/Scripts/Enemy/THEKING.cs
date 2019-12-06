using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class THEKING : Enemy
{
    public PlayerHandler playerHandler;
    // Start is called before the first frame update
    void Start()
    {
        self = gameObject;
        healthBarCanvas = self.GetComponentInChildren<Canvas>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        points = wayPointParent.GetComponentsInChildren<Transform>();
        playerHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        SeekPlayer();
    }

    void SUPERSLAP()
    {

    }
}

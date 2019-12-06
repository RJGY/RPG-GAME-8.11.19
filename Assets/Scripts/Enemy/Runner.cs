using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Runner : Enemy
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
        float currentDistance = Vector3.Distance(transform.position, player.position);

        if (currentDistance < setDistanceFromPlayer || playerFound)
        {
            playerFound = true;
            SeekPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void DamageBite()
    {
        Debug.Log("i did damage");
        playerHandler.DamagePlayer(10f);
    }
}

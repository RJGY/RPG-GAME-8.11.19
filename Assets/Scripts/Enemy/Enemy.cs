using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    // Navmesh
    [Header("Navmesh Properties")]
    public NavMeshAgent agent;
    public Transform wayPointParent;
    public Transform[] points;
    public float wayPointDistance = 1f;
    public int currentWayPoint = 1;
    public Transform player;
    public float setDistanceFromPlayer = 15f;
    public bool playerFound = false;

    // Enemy Statistics
    [Header("Enemy Stats")]
    public float enemySpeed = 3f;
    public float enemyHealth;
    public float enemyAttackRange = 4f;
    public float enemyAttackSpeed = 60f;
    public bool attackNotOnCooldown = true;

    public Animator anim;
    public Canvas healthBarCanvas;
    public Image enemyHealthImage;
    public GameObject self;
    public enum EnemyStates
    {
        Attack,
        Die,
        Patrol,
        SeekPlayer
    }
    public string state;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Patrol()
    {
        anim.SetBool("Walk", true);
        Transform currentPoint = points[currentWayPoint];
        float distance = Vector3.Distance(transform.position, currentPoint.position);
        agent.SetDestination(currentPoint.position);
        if (distance < wayPointDistance)
        {
            if (currentWayPoint < points.Length - 1)
            {
                currentWayPoint++;
            }

            else
            {
                currentWayPoint = 1;
            }
        }
    }

    public void Attack()
    {
        if (attackNotOnCooldown && player.GetComponent<PlayerHandler>().currentHealth > 0)
        {
            anim.SetTrigger("Bite Attack");
            attackNotOnCooldown = false;
            Invoke("AttackCooldown", enemyAttackSpeed / 60f);
        }

    }

    public void AttackCooldown()
    {
        attackNotOnCooldown = true;
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }

    public void SeekPlayer()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Run", true);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < enemyAttackRange)
        {
            Attack();
            return;
        }

        agent.SetDestination(player.position);

        
    }


}

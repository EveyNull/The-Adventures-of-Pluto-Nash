using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{

    public float lookRadius  = 10f;
    public Transform target;

    private Vector3 patrolPlace;
    private float patrolTimer;
    NavMeshAgent agent;

    public float hitBack = 0;
    public bool isDead = false;

    // Start is called before the first frame update
    private void Start()
    {
        isDead = false;
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        patrolPlace = transform.position + new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10));
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (!isDead)
        {
            if(hitBack >= 0)
            {
                hitBack -= Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, transform.position - (6f * transform.forward), Time.deltaTime * 3);
            }
            else if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                return;
            }

            if (patrolTimer > 0)
            {
                patrolTimer -= Time.deltaTime;
                agent.SetDestination(patrolPlace);
            }
            else
            {
                patrolPlace = transform.position + new Vector3(Random.Range(-10, 10), transform.position.y, Random.Range(-10, 10));
                patrolTimer = 5f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

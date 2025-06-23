using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PatrollingZombie : Zombie
{

    [SerializeField]
    public List<Vector3> patrolpositions;

    private int patrolindex = 0;
    override public void Start()
    {
        base.Start();
        agent.destination = patrolpositions[0];
    }
    override public void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, patrolpositions[patrolindex]) < 0.1)
        {
            patrolindex += 1;
            patrolindex %= patrolpositions.Count; 
            GetComponent<NavMeshAgent>().destination = patrolpositions[patrolindex];
        }
    }
}

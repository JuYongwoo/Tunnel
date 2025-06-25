using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Following : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = ManagerObject.Resource.GetorAddComponent<NavMeshAgent>(gameObject);
        agent.speed = 1.5f;

        GetComponent<Zombie>().moveStat = Zombie.MoveStat.Run;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = ManagerObject.Scene.ThisScenePlayer.transform.position;
    }
}

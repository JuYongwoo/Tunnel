using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{


    protected NavMeshAgent agent;
    protected Animator animator;
    float currentspeed;

    virtual public void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            SceneManager.LoadScene("GameOver");
    }

    virtual public void Update()
    {
        currentspeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", currentspeed);
    }
}

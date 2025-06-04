using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Ch3MovingZombie : MonoBehaviour
{
    public GameObject[] places;
    Animator anim;
    int i = 0;
    float nowtime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        nowtime += Time.deltaTime;

        if (nowtime >= 10)
        {
            if (places.Length-1 > i)
            {
                GetComponent<NavMeshAgent>().destination = places[i].transform.position;
                i += 1;
                nowtime = 0.0f;
            }
            else
            {
                GetComponent<NavMeshAgent>().destination = places[i].transform.position;
                i = 0;
                nowtime = 0.0f;
            }
        }

        anim.SetFloat("Speed", GetComponent<NavMeshAgent>().velocity.magnitude);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Chapter3Hit");
        }
    }
}

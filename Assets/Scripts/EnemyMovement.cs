using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject destination;
    public bool stop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //follow the player
        destination = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        agent.destination = destination.transform.position;
    }


}

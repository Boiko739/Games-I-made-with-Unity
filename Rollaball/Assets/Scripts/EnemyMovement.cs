using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private PlayerController playerController;
    private NavMeshAgent navMeshAgent;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.playerStartedToPlay && player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}

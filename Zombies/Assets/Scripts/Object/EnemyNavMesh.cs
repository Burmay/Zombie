using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;
    PlayerInteractor playerI;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        playerI = Game.GetInteractor<PlayerInteractor>();
    }

    void Update()
    {
        agent.destination = playerI.GetTransformInstance().position;
    }
}

using UnityEngine;
using UnityEngine.AI;

public interface IWalkableAI
{
    float lookRadius { get; set; } 
    NavMeshAgent agent { get; set; }

    float DistanceToTarget (Transform target);
    void GoToTarget (Transform target);
}

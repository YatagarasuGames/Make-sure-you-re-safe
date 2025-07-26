using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Chase : StateMachineBehaviour
{
    [Inject] private FirstPersonMovement _playerMovement;
    private Transform _player;
    private NavMeshAgent _agent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = _playerMovement.gameObject.transform;
        _agent = animator.GetComponent<NavMeshAgent>();
    }

override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    if (_agent == null || !_agent.isActiveAndEnabled || !_agent.isOnNavMesh)
        return;

    Vector3 targetPosition = _player.position;
    NavMeshPath path = new NavMeshPath();
    
    _agent.CalculatePath(targetPosition, path);
    
    if (path.status == NavMeshPathStatus.PathComplete)
    {
        _agent.SetDestination(targetPosition);
    }
    else
    {
        Vector3 directionToPlayer = (_player.position - _agent.transform.position).normalized;
        float searchRadius = Mathf.Max(_agent.radius, 1f);

        for(int i = 0; i < 5; i++)
        {
            Vector3 searchPoint = _player.position - directionToPlayer * (i * 0.5f);
            NavMeshHit hit;
            
            if (NavMesh.SamplePosition(searchPoint, out hit, searchRadius, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
                return;
            }
        }
        
        NavMesh.SamplePosition(_player.position, out var fallbackHit, searchRadius * 2, NavMesh.AllAreas);
        _agent.SetDestination(fallbackHit.position);
    }
}
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

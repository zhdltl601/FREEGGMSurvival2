using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour,IAgentComponent
{
    public Agent _agent { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public Transform[] patrols;
    public int currentPatrolIndex;
    
    [SerializeField] private float _knockBackThreshold;
    [SerializeField] private float _maxKnockBackTime;

    private float _knockBackTime; //현재 넉백시간을 저장
    private bool _isKnockBack; //현재 넉백중인지를 저장

    public float stopOffset;

    public bool IsArrived => !NavMeshAgent.isPathStale && NavMeshAgent.remainingDistance < NavMeshAgent.stoppingDistance + stopOffset;
    
    public void Initialize(Agent agent)
    {
        _agent = agent;
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    #region Knockback

    public void GetKnockBack(Vector3 force)
    {
        StartCoroutine(ApplyKnockBack(force));
    }

    private IEnumerator ApplyKnockBack(Vector3 force)
    {
        NavMeshAgent.enabled = false; //네브메시가 자꾸 제자리로 갈려고 해
        Rigidbody.useGravity = true;
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(force, ForceMode.Impulse);
        _knockBackTime = Time.time; //넉백 시작타임을 기록하고

        if (_isKnockBack)
        {
            yield break; //코루틴 종료
        }

        _isKnockBack = true;
        yield return new WaitForFixedUpdate(); //물리 프레임만큼 대기

        yield return new WaitUntil(CheckKnockBackEnd);
        DisableRigidbody();

        NavMeshAgent.Warp(transform.position);
        _isKnockBack = false;

        if(!_agent.IsDead)
        {
            NavMeshAgent.enabled = true;
        }
    }

    public void DisableMovementAgent()
    {
        StopAllCoroutines();
        DisableRigidbody();
        NavMeshAgent.enabled = false;
    }
    
    private bool CheckKnockBackEnd()
    {
        return Rigidbody.velocity.magnitude < _knockBackThreshold
               || Time.time > _knockBackTime + _maxKnockBackTime;
    }
    
    #endregion
    
    private void DisableRigidbody()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.useGravity = false;
        Rigidbody.isKinematic = true;
    }
    public void SetDestination(Vector3 destination)
    {
        if (NavMeshAgent.enabled == false) return;
        
        NavMeshAgent.isStopped = false;
        NavMeshAgent.SetDestination(destination);
    }
    public void SetStopped(bool active)
    {
        if (NavMeshAgent.enabled == false) return;
        
        NavMeshAgent.isStopped = active;
    }
    public void SetSpeed(float speed)
    {
        NavMeshAgent.speed = speed;
    }
    public Vector3 GetNextPathPoint()
    {
        NavMeshPath path = NavMeshAgent.path;

        if (path.corners.Length < 2)
        {
            return NavMeshAgent.destination;
        }

        for (int  i = 0;  i < path.corners.Length; i++)
        {
            float distance = Vector3.Distance(NavMeshAgent.transform.position , path.corners[i]);

            if (distance < 1 && i < path.corners.Length -1)
            {
                return path.corners[i + 1];
            }
        }

        return NavMeshAgent.destination;
    }

    public Vector3 GetNextPatrolPoint()
    {
        Vector3 target = patrols[(currentPatrolIndex++ % patrols.Length)].position;
        return target;
    }
}

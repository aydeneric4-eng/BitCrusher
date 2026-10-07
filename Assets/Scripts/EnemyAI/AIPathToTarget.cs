using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using UnityEditor.U2D.Sprites;
using UnityEngine;

[RequireComponent(typeof(AIGoToPoint))]
public class AIPathToTarget : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] AStarManager pathfindingManager;
    [SerializeField] LayerMask obstaclesLayer;

    [SerializeField] float navigationMinDistToPoint = 0.5f;
    [SerializeField] float chaseMinDistToTarget = 0.5f;
    [SerializeField] float maxDistanceFromTarget = 0.5f;
    //[SerializeField] bool requireSight;
    public bool isActive = true;

    [SerializeField] float minTimeBetweenStateRechecks = 0.5f;
    private float randStateCheckTimeOffset = 0.2f;
    private float timeOfLastStateCheck;

    //[SerializeField] int aStarPathNodesUntilRecheck;
    //private int pathNodesSinceLastCheck = 0;
    private List<Vector3> pathNodes = new List<Vector3>();

    private AIGoToPoint pointTravel;
    private enum PathingStates
    {
        idle,
        directChase,
        aStarNavigation,
    }
    private PathingStates pathingState = PathingStates.idle;

    private void Awake()
    {
        pointTravel = GetComponent<AIGoToPoint>();
        timeOfLastStateCheck += Random.Range(-randStateCheckTimeOffset, randStateCheckTimeOffset);
    }
    private void Start()
    {
        pointTravel.reachedTarget += OnReachedPoint;
    }

    private void OnReachedPoint()
    {
        if (pathNodes.Count > 0)
        {
            pointTravel.GoToPoint(pathNodes[0]);
            pathNodes.RemoveAt(0);
        }
        else
        {
            ChangeState(PathingStates.idle);
        }
    }

    private RaycastHit2D GetLineToTarget()
    {
        return Physics2D.Linecast(transform.position, targetTransform.position, obstaclesLayer);
    }

    private void ChangeState(PathingStates state)
    {
        switch (state)
        {
            case (PathingStates.idle):
                {
                    pointTravel.StopMoving();
                    break;
                }

            case (PathingStates.directChase):
                {
                    pointTravel.minDistanceToTarget = chaseMinDistToTarget;
                    pointTravel.ChaseTarget(targetTransform);
                    break;
                }

            case (PathingStates.aStarNavigation):
                {
                    pathNodes = pathfindingManager.GetPath(transform.position, targetTransform.position);
                    if (pathNodes == null)
                    {
                        ChangeState(PathingStates.directChase); // Default to chase
                        return;
                    }
                    pointTravel.minDistanceToTarget = navigationMinDistToPoint;
                    pointTravel.GoToPoint(pathNodes[0]);
                    pathNodes.RemoveAt(0);
                    break;
                }

            default:
                {
                    Debug.LogWarning("NOT VALID Pathing STATE BRU WAT?");
                    break;
                }
        }
        pathingState = state;
    }

    private void FixedUpdate()
    {
        if (targetTransform == null)
        {
            pointTravel.StopMoving();
            if (GameManager.Instance.playerIntance)
                targetTransform = GameManager.Instance.playerIntance.transform;
        }
        if (!pathfindingManager)
        {
            if (GameManager.Instance.pathfindingInstance)
                pathfindingManager = GameManager.Instance.pathfindingInstance;
        }

        if (!isActive || !targetTransform || !pathfindingManager || !CustomUtilities.HasTimeElapsed(timeOfLastStateCheck, minTimeBetweenStateRechecks))
            return;

        //Debug.Log("StateCheck");
        timeOfLastStateCheck = Time.time;
        float distFromTarget = (transform.position - targetTransform.position).magnitude;
        if ((distFromTarget < chaseMinDistToTarget || (distFromTarget < maxDistanceFromTarget && pathingState == PathingStates.idle)) && !GetLineToTarget())
        {
            ChangeState(PathingStates.idle);
        }
        else if (GetLineToTarget())
        {
            ChangeState(PathingStates.aStarNavigation);
        }
        else
        {
            ChangeState(PathingStates.directChase);
        }
    }

    private bool drawGizmos = false;
    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;
        if (pathingState == PathingStates.aStarNavigation)
        {
            if (pathNodes == null)
                return;
            if (pathNodes.Count < 2)
                return;

            Gizmos.color = Color.red;

            for (int i = 1; i < pathNodes.Count; i++)
            {
                //Debug.Log("DrawL");
                //Debug.Log(i);
                //Debug.Log(path.Count);

                if (i < 0 || i > pathNodes.Count)
                {
                    Debug.LogWarning("wtf i ???");
                    return;
                }
                //Debug.Log("DRAW COORDS");
                //Debug.Log(path[i]);
                //Debug.Log(path[i - 1]);
                Gizmos.DrawLine(pathNodes[i - 1], pathNodes[i]);
            }
        }
        else if (pathingState == PathingStates.directChase)
        {
            Gizmos.DrawLine(transform.position, targetTransform.position);
        }
    }
}

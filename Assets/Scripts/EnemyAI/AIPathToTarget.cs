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
    }
    private void Start()
    {
        pointTravel.reachedTarget += OnReachedPoint;
    }

    private void OnReachedPoint()
    {

    }

    private void FixedUpdate()
    {
        if (!isActive || targetTransform == null)
            return;

        switch(pathingState)
        {
            case (PathingStates.idle):
                {
                    RaycastHit2D hitData = Physics2D.Linecast(transform.position, targetTransform.position, obstaclesLayer);
                    break;
                }

            case (PathingStates.directChase):
                {
                    break;
                }

            case (PathingStates.aStarNavigation):
                {
                    break;
                }

            default:
                {
                    Debug.LogWarning("NOT VALID Pathing STATE BRU WAT?");
                    break;
                }
        }
    }

}

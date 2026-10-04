using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(RBMovement))]
public class AIGoToPoint : MonoBehaviour
{
    [SerializeField] float maxVectComponentDifference = 0.05f;
    [SerializeField] public float minDistanceToTarget = 0.5f;
    [SerializeField] bool active = false;

    private bool isChasing = false;

    private RBMovement selfRBM;

    private Vector3 targetPosition;
    private Transform chaseTransform;

    public event Action reachedTarget;

    private void Awake()
    {
        selfRBM = GetComponent<RBMovement>();
    }

    private float IsValueOutOfRange(float value, float maxDeviance, float inRangeReturn = 0f)
    {
        if (Mathf.Abs(value) < maxDeviance)
            return inRangeReturn;
        else
            return value;
    }

    public void StopMoving()
    {
        isChasing = false;
        active = false;
        selfRBM.inputMovementVector = Vector3.zero;
    }

    public void GoToPoint(Vector3 position)
    {
        //Debug.Log("Now going to point");
        isChasing = false;
        targetPosition = position;
        active = true;
    }

    public void ChaseTarget(Transform targetTransform)
    {
        //Debug.Log("Now chasing target");
        chaseTransform = targetTransform;
        isChasing = true;
        active = true;
    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            targetPosition = chaseTransform.position;
        }

        if ((transform.position - targetPosition).magnitude < minDistanceToTarget && active)
        {
           if (!isChasing)
                active = false;
            selfRBM.inputMovementVector = Vector3.zero;
            reachedTarget.Invoke();
        }

        if (active)
        {
            Vector3 inputVect = (Vector3)CustomUtilities.GetVectorByAngleAndDistance(1, CustomUtilities.GetAngleOf2DVect((Vector2)transform.position, (Vector2)targetPosition));
            inputVect = new Vector3(IsValueOutOfRange(inputVect.x, maxVectComponentDifference), IsValueOutOfRange(inputVect.y, maxVectComponentDifference), 0);
            selfRBM.inputMovementVector = inputVect;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, targetPosition);
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitAnchorAndAimAtTarget : MonoBehaviour
{
    [SerializeField] bool isTargetMouse = false;
    [SerializeField] Transform targetTransform;
    [SerializeField] bool rotateTowardsTarget;

    [SerializeField] Transform orbitAnchorTransform;
    [SerializeField] bool orbitTowardsTarget;
    [SerializeField] float orbitDistance = 0.1f;

    private Transform selfTransform;
    private Vector2 targetPosition;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (!isTargetMouse && targetTransform == null)
        {
            selfTransform.localPosition = Vector3.zero;
            return;
        }


        if (isTargetMouse)
        {
            if (Mouse.current != null)
            { //Ai
                Vector2 screenPosition = Mouse.current.position.ReadValue();
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                worldPosition.z = 0f; //Ai
                targetPosition = worldPosition;
            }
        }
        else
        {
            targetPosition = CustomUtilities.Vec3ToVec2(targetTransform.position);
        }
        Vector2 selfPos = CustomUtilities.Vec3ToVec2(selfTransform.position);
        float angleToTarget = CustomUtilities.GetAngleOf2DVect(orbitAnchorTransform.position, targetPosition);

        if (rotateTowardsTarget)
        {
            transform.eulerAngles = new Vector3(0,0,angleToTarget);
        }
        if (orbitTowardsTarget)
        {
            selfTransform.position = orbitAnchorTransform.position + CustomUtilities.GetVectorByAngleAndDistance(orbitDistance,angleToTarget);
        }

    }
}

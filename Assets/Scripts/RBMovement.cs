using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RBMovement : MonoBehaviour, IReceivesKnockback
{
    [SerializeField] float maxMoveSpeed = 5f;
    [SerializeField] float maxOverspeed = 100f;
    [SerializeField] float acceleration = 2.5f;
    [SerializeField] float deacceleration = 1f;
    [SerializeField] float overspeedDeacceleration = 5f;

    private Rigidbody2D selfRigidBody;

    public Vector3 inputMovementVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        selfRigidBody = GetComponent<Rigidbody2D>();
    }

    public void ResetVelocity()
    {
        velocity = Vector3.zero;
        selfRigidBody.linearVelocity = velocity;
    }

    private float GetNewVelocityValue(float current, float input) // UGLY!!!!
    {
        float inputSign = Mathf.Sign(input);
        float currentSign = Mathf.Sign(current);

        float absInput = Mathf.Abs(input);
        float absCurrent = Mathf.Abs(current);

        float clampMax = maxMoveSpeed;
        float clampMin = 0;

        float modifyingValue = 0f;
        float modifySign = 1f;

        float finalValue = 0f;
        float finalSign = 1f;


        if (current == 0 && input == 0)
            return 0f;

        if (absCurrent > maxMoveSpeed)
        {
            modifyingValue = overspeedDeacceleration;
            modifySign = -1f;
            clampMax = maxOverspeed;
        }
        else if ((inputSign == currentSign || current == 0) && input != 0)
        {
            modifyingValue = acceleration;
            modifySign = 1f;
        }
        else if (inputSign != currentSign && current != 0 && acceleration > deacceleration && input != 0)
        {
            modifyingValue = acceleration;
            modifySign = -1f;
        }
        else if (input == 0 && current != 0)
        {
            modifyingValue = deacceleration;
            modifySign = -1f;
        }

        if (input != 0 && inputSign != currentSign && absCurrent < modifyingValue) // No input + moving opp of current + change greater than current
            finalSign = -currentSign;
        else
            finalSign = currentSign;

        finalValue = Mathf.Max(Mathf.Min(absCurrent + modifyingValue * modifySign, clampMax), clampMin) * finalSign;

        //Debug.Log("signs:");
        //Debug.Log(input);
        //Debug.Log(inputSign);
        //Debug.Log(currentSign);
        //Debug.Log("clamps:");
        //Debug.Log(clampMax);
        //Debug.Log(clampMin);
        //Debug.Log("mod:");
        //Debug.Log(modifyingValue);
        //Debug.Log(modifySign);
        //Debug.Log("finalValue");
        //Debug.Log(finalValue);
        //Debug.Log(finalSign);
        return finalValue;
    }

    [SerializeField] AudioClip KBSFX;

    private Vector3 queuedKB = Vector3.zero;
    public void ReceiveKnockback(Vector3 impulse)
    {
        if (KBSFX)
            GameManager.Instance.audioManager.PlaySFX(KBSFX);
        queuedKB += impulse;
    }

    private void FixedUpdate()
    {
        velocity += queuedKB;
        queuedKB = Vector3.zero;

        velocity = new Vector3(GetNewVelocityValue(velocity.x, inputMovementVector.x), GetNewVelocityValue(velocity.y, inputMovementVector.y), 0);//new Vector3(GetNewVelocityValue(velocity.x,inputMovementVector.x), GetNewVelocityValue(velocity.y, inputMovementVector.y), 0);

        velocity = velocity.normalized * velocity.magnitude;

        selfRigidBody.linearVelocity = velocity;

        //Debug.Log("CustomUtilities.AbsMin(0, 0)");
        //Debug.Log(CustomUtilities.AbsMin(-10, -5));
        //Debug.Log(CustomUtilities.AbsMax(-10, -5));
        //Debug.Log("CustomUtilities.AbsMin(0, 0)");
    }
}

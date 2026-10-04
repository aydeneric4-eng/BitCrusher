using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(RBMovement))]
public class PlayerMovement : MonoBehaviour
{
    private RBMovement RBMover;

    private void Awake()
    {
        RBMover = GetComponent<RBMovement>();
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 inputMovementVector = CustomUtilities.Vec2ToVec3(ctx.ReadValue<Vector2>());
        inputMovementVector = new Vector2(CustomUtilities.Sign(inputMovementVector.x), CustomUtilities.Sign(inputMovementVector.y)); // UNnormalizes
        RBMover.inputMovementVector = inputMovementVector;
        /*
        Debug.Log("##### Movement #####");
        Debug.Log("XVOMP");
        Debug.Log(Mathf.Sign(inputMovementVector.x));
        Debug.Log("YCOMP");
        Debug.Log(Mathf.Sign(inputMovementVector.y));
        Debug.Log("##### END #####");
        */
    }
}

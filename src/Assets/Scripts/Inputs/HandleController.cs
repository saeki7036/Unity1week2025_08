using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleController : MonoBehaviour
{
    [SerializeField]
    RectTransform HandleRectTransform;

    [SerializeField]
    float HandleRadiusSize = 210f;

    [SerializeField]
    float RotateClampLimit_Z = 200f;

    [SerializeField]
    float AdjustmentLimit = 15f;

    [SerializeField]
    float ClampSpeedLimit = 5f;

    float InputUIPos_X;
    bool InsideInputflag;

    float MoveValue;

    public float GetMoveValue() => MoveValue;

    SR_System system => SR_System.instance;

    public void InputRegister(InputManager input)
    {
        input.LeftDownEvent += InputPosCheck;
        input.LeftDlagEvent += MoveValueSetting;
        input.LeftUpEvent += ResetMoveValue;
    }

    bool IsPointInsideHandle(Vector2 screenPoint)
    {
        Vector2 HandlePoint = RectTransformUtility.WorldToScreenPoint
            (null,HandleRectTransform.position);

        return Vector2.Distance(screenPoint, HandlePoint) < HandleRadiusSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        InsideInputflag = false;
        MoveValue = 0;
    }

    void InputPosCheck(Vector3 downInput)
    {
        InsideInputflag = IsPointInsideHandle(new Vector2(downInput.x, downInput.y));

        if (!InsideInputflag)
            return;

        InputUIPos_X = downInput.x;    
    }

    void MoveValueSetting(Vector3 dragInput)
    {
        if (!InsideInputflag)
            return;

        if (system.IsMainGamePlay() == false) return;

        float currentValue = (dragInput.x - InputUIPos_X) / AdjustmentLimit;

        float ClampValue = Mathf.Clamp(currentValue, -ClampSpeedLimit, ClampSpeedLimit);

        MoveValue = ClampValue;

        float rotateValue_Z = ClampValue * (RotateClampLimit_Z / ClampSpeedLimit);

        HandleRectTransform.rotation = Quaternion.Euler(0, 0, -rotateValue_Z);
    }

    void ResetMoveValue(Vector3 upInput) 
    {
        MoveValue = 0;
        HandleRectTransform.rotation = Quaternion.identity; 
    }
}

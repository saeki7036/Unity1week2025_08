using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    Animator KeyHolderAnimator;

    [SerializeField]
    Transform CarTransform;

    [SerializeField]
    HandleController handleController;

    [SerializeField]
    float VelocityDiameter = 0.02f;

    [SerializeField]
    float MoveLimitMin = -20f;

    [SerializeField]
    float MoveLimitMax = 20f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveValue = handleController.GetMoveValue();

        KeyHolderAnimator.SetFloat("CarSpeed", moveValue);

        Vector3 carPos = CarTransform.position;

        float currentMoveSpeed = moveValue * VelocityDiameter;

        carPos.x = Mathf.Clamp(carPos.x + currentMoveSpeed, MoveLimitMin, MoveLimitMax);

        CarTransform.position = carPos;
    }
}

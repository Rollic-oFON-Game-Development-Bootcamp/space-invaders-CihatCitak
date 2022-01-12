using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputSettings inputSettings;
    [SerializeField] Transform sideMovementRoot, leftLimitTransform, rightLimitTransform;
    [SerializeField] float sideMovementSensitivity;
    [SerializeField] float rotationSpeed;

    float leftLimitX, rightLimitX;

    private void Start()
    {
        leftLimitX = leftLimitTransform.localPosition.x;
        rightLimitX = rightLimitTransform.localPosition.x;
    }

    private void Update()
    {
        HandleSideMovement();
    }

    private void HandleSideMovement()
    {
        var localPos = sideMovementRoot.localPosition;
        localPos += Vector3.right * inputSettings.InputDrag.x * sideMovementSensitivity * Time.deltaTime;

        localPos.x = Mathf.Clamp(localPos.x, leftLimitX, rightLimitX);

        sideMovementRoot.localPosition = localPos;

        var moveDirection = Vector3.forward * 0.5f;
        moveDirection += sideMovementRoot.right * inputSettings.InputDrag.x * sideMovementSensitivity;

        moveDirection.Normalize();

        var targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        sideMovementRoot.rotation = Quaternion.Lerp(sideMovementRoot.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}

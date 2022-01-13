using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputSettings inputSettings;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject laserGreenPrefab;
    [SerializeField] Transform sideMovementRoot, leftLimitTransform, rightLimitTransform;
    [SerializeField] float sideMovementSensitivity;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fireRate = 5f;

    float leftLimitX, rightLimitX, lastFireTime;

    private void Start()
    {
        leftLimitX = leftLimitTransform.localPosition.x;
        rightLimitX = rightLimitTransform.localPosition.x;
    }

    private void Update()
    {
        if(GameManagement.Instance.GameState == GameManagement.GameStates.START)
        {
            HandleSideMovement();

            if (CanFire())
                Fire();
        }
        
    }

    #region Movement
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
    #endregion

    #region Fire
    public void Fire()
    {
        Destroy(Instantiate(laserGreenPrefab, sideMovementRoot.position, Quaternion.identity), 5f);

        lastFireTime = Time.time;
    }

    private bool CanFire()
    {
        if ((lastFireTime + fireRate) < Time.time)
        {
            return true;
        }
        else
            return false;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyLaser"))
        {
            GameManagement.Instance.LoseTheGame();

            Destroy(collision.gameObject);

            Destroy(Instantiate(explosionPrefab, sideMovementRoot.position, Quaternion.identity), 1f);

            Destroy(gameObject);
        }
    }
}

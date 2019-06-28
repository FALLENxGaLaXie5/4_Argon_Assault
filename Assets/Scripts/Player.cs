using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In m/s")][SerializeField] float xSpeed = 10f;
    [Tooltip("In m")] [SerializeField] float xRange = 2f;

    [Tooltip("In m/s")] [SerializeField] float ySpeed = 10f;
    [Tooltip("In m")] [SerializeField] float yRange = 2f;

    [SerializeField] float positionPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 10f;

    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }
    
    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime; // amount to move this frame in x direction
        float clampedXPos = Mathf.Clamp(xOffset + transform.localPosition.x, -xRange, xRange);

        float yOffset = yThrow * ySpeed * Time.deltaTime; // amount to move this frame in y direction
        float clampedYPos = Mathf.Clamp(yOffset + transform.localPosition.y, -yRange, yRange);



        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + (yThrow * controlPitchFactor);
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}

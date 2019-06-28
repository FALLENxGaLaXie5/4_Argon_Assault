using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    //todo figure out why sometimes slow on first play of scene

    [Header("General")]
    [Tooltip("In m/s")][SerializeField] float xControlSpeed = 10f;
    [Tooltip("In m")] [SerializeField] float xRange = 2f;
    [SerializeField] GameObject[] guns;

    [Tooltip("In m/s")] [SerializeField] float yControlSpeed = 10f;
    [Tooltip("In m")] [SerializeField] float yRange = 2f;

    [Header("Screen-Position Based")]
    [SerializeField] float positionPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 10f;

    [Header("Control Throw Based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;
    bool controlsEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if (controlsEnabled)
        {
            if (CrossPlatformInputManager.GetButton("Fire"))
            {
                ActivateGuns();
            }
            else
            {
                DeactivateGuns();
            }
        }
    }
    
    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            if (gun.GetComponent<ParticleSystem>().isStopped)
            {
                gun.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            if (gun.GetComponent<ParticleSystem>().isPlaying)
            {
                gun.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    private void ProcessMovement()
    {
        if (controlsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }        
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xControlSpeed * Time.deltaTime; // amount to move this frame in x direction
        float clampedXPos = Mathf.Clamp(xOffset + transform.localPosition.x, -xRange, xRange);

        float yOffset = yThrow * yControlSpeed * Time.deltaTime; // amount to move this frame in y direction
        float clampedYPos = Mathf.Clamp(yOffset + transform.localPosition.y, -yRange, yRange);



        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void OnPlayerDeath() //called by string reference
    {
        controlsEnabled = false;
    }
    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + (yThrow * controlPitchFactor);
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}

 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PipeSystem PlayerPipeSystem;
    public float rotationVelocity;
    public MainMenu Mainmenu;
    public float startVelocity;
    public float[] accelerations;
    public Hud Playerhud;

    private float acceleration, velocity;
    private Pipe currentpipe;

    private float distancetravelled;
    private float systemRotation,avatarRotation;
    private float deltaToRotation;
    private Transform world,rotator;
    private float worldRotation;

    private void Start()
    {
        world = PlayerPipeSystem.transform.parent;
        rotator = transform.GetChild(0);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        float delta = velocity * Time.deltaTime;
        distancetravelled += delta;
        systemRotation += delta * deltaToRotation;
        velocity += acceleration * Time.deltaTime;

        if(systemRotation>=currentpipe.CurveAngle)
        {
            delta = (systemRotation - currentpipe.CurveAngle) / deltaToRotation;
            currentpipe = PlayerPipeSystem.SetUpNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }

        PlayerPipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);
        UpdateAvatarRotation();
        Playerhud.SetValue(distancetravelled, velocity);
    }


    public void  StartGame(int accelerationMode)
    {
        distancetravelled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        currentpipe = PlayerPipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        acceleration = accelerations[accelerationMode];
        velocity=startVelocity;
        Playerhud.SetValue(distancetravelled, velocity);
        gameObject.SetActive(true);
    }

    public void Die()
    {
        Mainmenu.EndGame(distancetravelled);
        gameObject.SetActive(false);
    }

    private void UpdateAvatarRotation()
    {
        avatarRotation += rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");

        if(avatarRotation<0f)
        {
            avatarRotation += 360f;
        }
        else if(avatarRotation>=360f)
        {
            avatarRotation -= 360f;
        }
        rotator.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentpipe.CurveRadius);
        worldRotation += currentpipe.RelativeRotation;

        if (worldRotation < 0f)
            worldRotation += 360f;
        else if (worldRotation >= 360f)
            worldRotation = 360f;

        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }
}

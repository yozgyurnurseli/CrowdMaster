using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineController : MonoBehaviour
{
    SplineFollower sf;

    bool isRushing = false;
    bool isRunning = false;

    void Start()
    {
        sf = GetComponent<SplineFollower>();
        sf.onEndReached += OnFinish;
    }

    void OnFinish(double a)
    {
        EventManager.Invoke_OnAnimationChange(false, false, true);
    }

    public void GoToFinish()
    {
        sf.followSpeed = 4.2f;
        EventManager.Invoke_OnFinishGame();
    }

    public void Rush()
    {
        isRushing = true;
        sf.followSpeed = 4.2f;
        EventManager.Invoke_OnAnimationChange(true, true);
    }

    public void StopRush()
    {
        isRushing = false;

        if(isRunning)
            EventManager.Invoke_OnAnimationChange(true, false);
        else
        {
            EventManager.Invoke_OnAnimationChange(false, false);
            sf.followSpeed = 0f;
        }
    }

    void Run()
    {
        if (!isRushing)
        {
            sf.followSpeed = 4.2f;
            EventManager.Invoke_OnAnimationChange(true, false);
        }
        
        isRunning = true;
    }

    void Stop()
    {
        if (!isRushing)
        {
            sf.followSpeed = 0f;
            EventManager.Invoke_OnAnimationChange(false,false);
        }

        isRunning = false;
    }

    private void OnEnable()
    {
        EventManager.OnInputTouch += Run;
        EventManager.OnInputTouchCanceled += Stop;
    }

    private void OnDisable()
    {
        EventManager.OnInputTouch -= Run;
        EventManager.OnInputTouchCanceled -= Stop;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static event Action OnInputTouch;
    public static void Invoke_OnInputTouch()
    {
        OnInputTouch?.Invoke();
    }

    public static event Action<float> OnInputDrag;
    public static void Invoke_OnInputDrag(float delta)
    {
        OnInputDrag?.Invoke(delta);
    }

    public static event Action OnInputTouchCanceled;
    public static void Invoke_OnInputTouchCanceled()
    {
        OnInputTouchCanceled?.Invoke();
    }

    public static event Action<bool, bool, bool> OnAnimationChange;
    public static void Invoke_OnAnimationChange(bool run1, bool run2, bool dance = false)
    {
        OnAnimationChange?.Invoke(run1, run2, dance);
    }

    public static event Action OnFinishGame;
    public static void Invoke_OnFinishGame()
    {
        OnFinishGame?.Invoke();
    }
}

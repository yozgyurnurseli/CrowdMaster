using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    bool touched = false;
    bool isGameStarted = false;
    bool isGameFinished = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (!isGameStarted || isGameFinished)
            return;

        if (Input.touchCount > 0)
        {
            if (!touched)
            {
                EventManager.Invoke_OnInputTouch();
                touched = true;
            }

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                EventManager.Invoke_OnInputDrag(touch.deltaPosition.x);
            }
        }
        else
        {
            if (touched)
            {
                touched = false;
                EventManager.Invoke_OnInputTouchCanceled();
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(WaitALittleBit());
    }

    IEnumerator WaitALittleBit()
    {
        yield return new WaitForSeconds(0.1f);
        isGameStarted = true;
    }

    public void FinishGame()
    {
        isGameFinished = true;
    }
}

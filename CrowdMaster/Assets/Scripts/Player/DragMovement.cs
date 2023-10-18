using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMovement : MonoBehaviour
{
    [SerializeField]
    float border;
    [SerializeField]
    float dragSpeed;

    float middleTime = 1f;

    public void SetBorder(bool increase)
    {
        if(increase)
            border += 0.22f;
        else
            border -= 0.22f;
    }

    public void ToMiddle()
    {
        StartCoroutine(GoToMiddle());
    }

    IEnumerator GoToMiddle()
    {
        Vector3 startingPos = transform.localPosition;
        Vector3 finalPos = startingPos;
        finalPos.x = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < middleTime)
        {
            transform.localPosition = Vector3.Lerp(startingPos, finalPos, (elapsedTime / middleTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void DragMove(float delta)
    {
        if (transform.localPosition.x < border && delta > 0f)
        {
            transform.Translate(delta * Time.deltaTime * dragSpeed, 0f, 0f);
        }
        else if (transform.localPosition.x > -border && delta < 0f)
        {
            transform.Translate(delta * Time.deltaTime * dragSpeed, 0f, 0f);
        }
    }

    private void OnEnable()
    {
        EventManager.OnInputDrag += DragMove;
    }

    private void OnDisable()
    {
        EventManager.OnInputDrag -= DragMove;
    }
}

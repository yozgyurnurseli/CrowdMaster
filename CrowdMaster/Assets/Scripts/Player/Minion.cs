using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField]
    MyStack stack;

    Animator animator;
    float positionTime = 1f;

    [SerializeField]
    bool mainChar;
    bool inStack;

    bool lastParam1;
    bool lastParam2;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (mainChar)
            inStack = true;
    }

    public void CirclePos(Vector3 pos, MyStack myStack = null, bool first = false, bool stackUpgrade = false)
    {
        inStack = true;
        if (stackUpgrade)
        {
            stack = myStack;
        }
        else if (first)
        {
            PlayAnim(lastParam1, lastParam2);
        }

        StartCoroutine(GotoPos(pos));
    }

    private IEnumerator GotoPos(Vector3 pos)
    {
        Vector3 startingPos = transform.localPosition;
        Vector3 finalPos = pos;
        float elapsedTime = 0f;

        while (elapsedTime < positionTime)
        {
            transform.localPosition = Vector3.Lerp(startingPos, finalPos, (elapsedTime / positionTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Obstacle))
        {
            stack.MinionDied(this);
            Vibrate.instance.TryVib();
            Destroy(gameObject);
        }
        if (other.CompareTag(Tags.Gold))
        {
            GameManager.instance.EarnGold();
            Destroy(other.gameObject);
        }
    }

    void PlayAnim(bool run1, bool run2, bool dance = false)
    {
        
        if (inStack)
        {
            if (dance)
                animator.SetTrigger(AnimParams.Dance);
            else
            {
                animator.SetBool(AnimParams.Run1, run1);
                animator.SetBool(AnimParams.Run2, run2);
            }
        }
        else
        {
            lastParam1 = run1;
            lastParam2 = run2;
        } 
    }

    private void OnEnable()
    {
        EventManager.OnAnimationChange += PlayAnim;
    }

    private void OnDisable()
    {
        EventManager.OnAnimationChange -= PlayAnim;
    }
}

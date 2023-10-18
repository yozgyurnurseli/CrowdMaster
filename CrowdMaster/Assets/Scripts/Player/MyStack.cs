using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStack : MonoBehaviour
{
    List<Minion> minions;
    Transform myTransform;
    SplineController sc;
    DragMovement dm;

    [SerializeField]
    GameObject minionPrefab;
    [SerializeField]
    CinemachineVirtualCamera vcam;

    #region CircleCalculation
    float radius = 0.24f;
    int radiusLayer = 1;
    
    int radiusLayerIndex = 0;
    int radiusLayerCapStart = 5;
    int radiusLayerCap;
    int radiusLayerCapIncrease = 2;

    Vector3 pos;
    int dieIndex = 0;
    #endregion

    int stackCount = 0;
    bool canTrigger = true;
    bool gameFinished = false;

    void Awake()
    {
        minions = new List<Minion>();
        myTransform = transform;
        pos = new Vector3();
        sc = transform.parent.GetComponent<SplineController>();
        dm = GetComponent<DragMovement>();
        radiusLayerCap = radiusLayerCapStart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Stack) && canTrigger)
        {
            List<Minion> collected = other.GetComponent<CollectableStack>().CollectedMinions();
            foreach (Minion item in collected)
            {
                if (GameManager.instance.GetMaxStack() == stackCount)
                    Destroy(item.gameObject);
                else
                {
                    item.transform.parent = myTransform;
                    minions.Add(item);
                    stackCount++;

                    PositionNewMinion(item);
                } 
            }
            Destroy(other.gameObject);
            UIManager.instance.SetStackCount(stackCount);

            if(GameManager.instance.GetMaxStack() == stackCount)
                sc.Rush();

            canTrigger = false;
            StartCoroutine(WaitForTrigger());
        }
        else if (other.CompareTag(Tags.Finish) && !gameFinished)
        {
            gameFinished = true;
            PlayerInput.instance.FinishGame();
            sc.GoToFinish();
            dm.ToMiddle();
        }
    }

    private IEnumerator WaitForTrigger()
    {
        yield return null;
        canTrigger = true;
    }

    void PositionNewMinion(Minion minion, bool stackUpgrade = false, MyStack stack = null)
    {
        float radians = 2 * Mathf.PI / radiusLayerCap * radiusLayerIndex;

        float vertical = Mathf.Sin(radians);
        float horizontal = Mathf.Cos(radians);

        pos.x = horizontal;
        pos.z = vertical;

        pos *= radiusLayer * radius;

        minion.CirclePos(pos,stack, true, stackUpgrade);

        radiusLayerIndex++;
        if(radiusLayerIndex == radiusLayerCap)
        {
            radiusLayerIndex = 0;
            radiusLayer++;
            radiusLayerCap += radiusLayerCapIncrease;

            dm.SetBorder(false);
        }
    }

    public void MinionDied(Minion minion)
    {
        minions.Remove(minion);
        if (GameManager.instance.GetMaxStack() == stackCount)
            sc.StopRush();

        stackCount--;
        if (radiusLayerIndex > 0)
        {
            radiusLayerIndex--;
        }
        else
        {
            radiusLayer--;
            radiusLayerCap -= radiusLayerCapIncrease;
            radiusLayerIndex = radiusLayerCap-1;
            dm.SetBorder(true);
        }

        UIManager.instance.SetStackCount(stackCount);
        dieIndex++;
        StartCoroutine(Reposition(dieIndex));
    }

    IEnumerator Reposition(int index)
    {
        yield return new WaitForSeconds(1f);
        if(dieIndex == index)
        {
            dieIndex = 0;

            int layerCap = radiusLayerCapStart;
            int layerIndex = 0;
            int layer = 1;

            foreach (Minion item in minions)
            {
                float radians = 2 * Mathf.PI / layerCap * layerIndex;

                float vertical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                pos.x = horizontal;
                pos.z = vertical;

                pos *= layer * radius;

                item.CirclePos(pos);

                layerIndex++;
                if (layerIndex == layerCap)
                {
                    layerIndex = 0;
                    layer++;
                    layerCap += radiusLayerCapIncrease;
                }
            }
        }
    }

    public void StackUpgrade()
    {
        Minion minion = Instantiate(minionPrefab, myTransform.position, Quaternion.identity, myTransform).GetComponent<Minion>();

        minions.Add(minion);
        stackCount++;

        PositionNewMinion(minion, true, this);
    }

    void GameFinished()
    {
        StartCoroutine(DestroyAllMinions());
    }

    IEnumerator DestroyAllMinions()
    {
        foreach (Minion item in minions)
        {
            yield return new WaitForSeconds(0.07f);
            
            if(item != null)
            {
                Vibrate.instance.TryVib();
                Destroy(item.gameObject);
            }  
        }

        vcam.Priority = 11;
        yield return new WaitForSeconds(4f);
        UIManager.instance.FinishGame();
    }

    private void OnEnable()
    {
        EventManager.OnFinishGame += GameFinished;
    }

    private void OnDisable()
    {
        EventManager.OnFinishGame -= GameFinished;
    }
}

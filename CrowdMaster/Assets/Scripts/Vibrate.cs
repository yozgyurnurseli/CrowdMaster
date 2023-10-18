using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour
{
    public static Vibrate instance;
    bool isPlaying = false;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void TryVib()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            Vibration.Vibrate(15);
            StartCoroutine(Constr());
        }
    }

    private IEnumerator Constr()
    {
        yield return new WaitForSeconds(0.03f);
        isPlaying = false;
    }
}

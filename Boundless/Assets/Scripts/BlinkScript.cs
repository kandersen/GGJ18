using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    public SpriteRenderer Eyes;

    public IEnumerator Start()
    {
        var timeToNextBlink = Random.value * 8.0f;
        while (true)
        {
            timeToNextBlink -= Time.deltaTime;
            if (timeToNextBlink < 0f)
            {
                Eyes.enabled = false;
                yield return new WaitForSeconds(0.1f);
                Eyes.enabled = true;
                timeToNextBlink = Random.value * 8.0f;
            }
            yield return null;
        }
    }
}
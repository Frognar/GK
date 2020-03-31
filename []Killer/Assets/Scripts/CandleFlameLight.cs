using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlameLight : MonoBehaviour
{

    public int minIntensity = 30;
    public int maxIntensity = 40;

    private int minRange = 22;
    private int maxRange = 25;

    private int frames = 0;

    public Light candleFlameLight;
    // Update is called once per frame
    void Update()
    {
        if(++frames > 10)
        {
            candleFlameLight.intensity = Random.Range(minIntensity, maxIntensity);
            candleFlameLight.range = Random.Range(minRange, maxRange);
            
            frames = 0;
        }
    }
}

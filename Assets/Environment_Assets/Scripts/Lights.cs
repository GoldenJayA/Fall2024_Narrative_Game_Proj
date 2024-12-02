using UnityEngine;

public class Lights : MonoBehaviour
{
    public Light pointLight;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float speed = 2f;

    void Update()
    {
        if (pointLight != null)
        {
            pointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * speed, 1));
        }
    }
}


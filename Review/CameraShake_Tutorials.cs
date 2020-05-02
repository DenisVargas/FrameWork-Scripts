using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Brackeys
//https://www.youtube.com/watch?v=9A9yj8KnM8c
public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;

            transform.localPosition = new vector3(x,y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}

//Renaissance Coders
//https://www.youtube.com/watch?v=GDatJi6HSYE
public class CameraShaker : MonoBehaviour
{
    public float power = 0.7f;
    public float duration = 1.0f;
    public transform camera;
    public float slowDownAmount = 1.0f;
    public bool shouldShake = float;

    Vector3 startPosition;
    float initialDuration;

    void Start()
    {
        camera = Camera.main.transform;
        startPosition = camera.localPosition;
        initialDuration = duration;
    }

    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                camera.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                camera.localPosition = startPosition;
            }
        }
    }

    //DitzelGames
    //https://www.youtube.com/watch?v=7noMEjDJ-_Q
    

}

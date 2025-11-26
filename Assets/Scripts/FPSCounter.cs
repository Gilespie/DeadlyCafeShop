using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    float smoothedFPS;

    void Update()
    {
        float currentFPS = 1f / Time.deltaTime;
        smoothedFPS = Mathf.Lerp(smoothedFPS, currentFPS, 0.1f);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 40), "FPS: " + Mathf.RoundToInt(smoothedFPS));
    }
}
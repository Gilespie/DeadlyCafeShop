using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] MeshRenderer _lampRenderer;
    [SerializeField] AnimationCurve _flashingCurve;
    [SerializeField] float _flashInterval = 0.5f;
    [SerializeField] float _intensity = 1f;
    Light _light;

    void Awake()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        float time = Time.time % _flashInterval;
        float curveValue = _flashingCurve.Evaluate(time / _flashInterval);
        _light.intensity = curveValue * _intensity;
        _lampRenderer.material.SetColor("_EmissionColor", _lampRenderer.material.color * curveValue);
    }
}
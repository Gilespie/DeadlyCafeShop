using UnityEngine;

public class GuardSFX : ObserverSFX
{
    public override void UpdateData(params object[] values)
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        base.UpdateData(values);
    }
}
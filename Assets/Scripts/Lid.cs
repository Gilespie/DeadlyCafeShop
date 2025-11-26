using UnityEngine;

public class Lid : InteractableObject
{
    public void AttachToCup(Cup cup)
    {
        transform.SetParent(cup.LidPosition, worldPositionStays: false);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        cup.PutLid();
    }
}
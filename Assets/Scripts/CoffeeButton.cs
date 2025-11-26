using UnityEngine;

public class CoffeeButton : MonoBehaviour, IInteractable
{
    [SerializeField] Color _pressedColor = Color.cyan;
    [SerializeField] Color _defaultColor = Color.red;
    [SerializeField] float _intensityColor = 2f;
    [SerializeField] CoffeeMachine _coffeeMachine;
    MeshRenderer _meshRenderer;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.EnableKeyword("_EMISSION");
        SetEmission(_defaultColor);
    }

    public bool Interact(Raycasting interactor)
    {
        if(!_coffeeMachine.CanActivate) return false;

        SetEmission(_pressedColor);
        _coffeeMachine.ActivateMachine();

        return true;
    }

    public void ResetButton()
    {
        SetEmission(_defaultColor);
    }

    private void SetEmission(Color c)
    {
        _meshRenderer.material.SetColor("_EmissionColor", c * _intensityColor);
    }

}
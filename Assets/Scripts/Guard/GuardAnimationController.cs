using UnityEngine;

public class GuardAnimationController
{
    string _speedName = "speedMagnitude";
    string _triggerName = "onDrink";
    Animator _animator;

    public GuardAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void CallTrigger()
    {
        _animator.SetTrigger(_triggerName);
    }

    public void UpdateSpeed(float speed)
    {
        _animator.SetFloat(_speedName, speed);
    }
}
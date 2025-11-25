using UnityEngine;

public class RandomIdle : StateMachineBehaviour
{
    [SerializeField] private int _maxIndex = 2;
    [SerializeField] private int _currentRandomIndex = 0;
    [SerializeField] string _idleStateParameter = "IdleState";

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentRandomIndex = Random.Range(0, _maxIndex);

        animator.SetFloat(_idleStateParameter, _currentRandomIndex);    
    }
}
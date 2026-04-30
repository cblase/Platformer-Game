using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehavior : StateMachineBehaviour
{
    public string boolName;
    public bool updateOnState, updateOnStateMachine;
    public bool valueOnEnter, valueOnExit;

    //controls animator paramtetrs based on the state of the animation
    //set boolean parameters when animation starts
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    //set boolean parameters when animation ends
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }

    //set boolean parameters when entering this statemachine
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    //set boolean parameters when exiting this statemachine
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }
}

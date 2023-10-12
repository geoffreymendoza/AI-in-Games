using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    LessonAIFollowPatrol controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponentInParent<LessonAIFollowPatrol>();
        controller.agent.speed = 4.5f;
        controller.MoveToRandomWaypoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!controller.agent.pathPending)
        {
            // returns the distance of agent if it is the same of stopping distance
            if (controller.agent.remainingDistance <= controller.agent.stoppingDistance)
            {
                //if agent has no path or agent is standing still
                if (!controller.agent.hasPath || controller.agent.velocity.sqrMagnitude == 0f)
                {
                    controller.MoveToRandomWaypoint();
                    // animator.SetBool("isIdle", true);
                    // animator.SetBool("isPatrol", false);
                }
            }
        }

        if (controller.distanceToPlayer < controller.followRadius)
        {
            animator.SetBool("isPatrol", false);
            animator.SetBool("isChasing", true);

        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

using UnityEngine;
// using UnityEngine.AI;
using System.Threading.Tasks;

public class IdleState : StateMachineBehaviour {
    // NavMeshAgent agent;
    // float idleDuration = 5f;

    [SerializeField] int idleDuration = 5000;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public async void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // agent = animator.GetComponent<NavMeshAgent>();

        await Task.Delay(idleDuration);
        animator.SetBool("isIdle", false);
        animator.SetBool("isPatrol", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public async void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // idleDuration -= Time.deltaTime;
        // if(idleDuration < 0) {
        //     animator.SetBool("isIdle", false);
        //     animator.SetBool("isPatrol", true);
        // }
    // }

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

using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>().Play();

        animator.Play("Scroling");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("Outro");
    }
}

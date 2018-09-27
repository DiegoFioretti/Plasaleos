using UnityEngine;

public class Death : MonoBehaviour, IState {

    public void StateUpdate(out IState nextState) {
        //triggerAnimation
        GetComponent<Animator>().SetBool("Dead", true);
        Invoke("Die", 1f /*animationDuration*/ );
        nextState = this;
    }

    public void StateFixedUpdate() { }

    void Die() {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            LevelManager.instance.AliveAliens--;
        }
    }
}
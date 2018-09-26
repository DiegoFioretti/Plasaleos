using UnityEngine;

public class Death : MonoBehaviour, IState {

    public void StateUpdate(out IState nextState) {
        //triggerAnimation
        Invoke("Die", 1f /*animationDuration*/ );
        nextState = this;
    }

    public void StateFixedUpdate() { }

    void Die() {
        gameObject.SetActive(false);
    }
}
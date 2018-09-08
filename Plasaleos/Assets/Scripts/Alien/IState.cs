public interface IState {
    void StateUpdate(ref IState currState);
    void StateFixedUpdate();
}
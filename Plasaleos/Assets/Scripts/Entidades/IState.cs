public interface IState {
    void StateUpdate(out IState nextState);
    void StateFixedUpdate();
}
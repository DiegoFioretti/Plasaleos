public interface IState {
    IState StateUpdate();
    void StateFixedUpdate();
}
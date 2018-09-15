public interface IState {
<<<<<<< HEAD:Plasaleos/Assets/Scripts/Alien/IState.cs
    void StateUpdate(ref IState currState);
=======
    void StateUpdate(out IState nextState);
>>>>>>> Alien:Plasaleos/Assets/Scripts/Entidades/IState.cs
    void StateFixedUpdate();
}
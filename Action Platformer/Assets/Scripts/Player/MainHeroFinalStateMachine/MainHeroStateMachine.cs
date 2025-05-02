public class MainHeroStateMachine
{
    public MainHeroState CurrentState { get; private set; }

    public void Initialize(MainHeroState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(MainHeroState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

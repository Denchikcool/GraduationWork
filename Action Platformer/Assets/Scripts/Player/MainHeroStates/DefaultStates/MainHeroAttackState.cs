using Denchik.Weapon;

public class MainHeroAttackState : MainHeroAbilityState
{
    private Weapon _weapon;

    private int _inputIndex;

    public MainHeroAttackState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName, Weapon weapon, CombatInput input) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
        this._weapon = weapon;
        _inputIndex = (int)input;

        _weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();

        _weapon.Enter();
    }

    private void ExitHandler()
    {
        mainHero.PlayerInputHandler.ChangeAttackInput(_inputIndex);

        AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _weapon.CurrentInput = mainHero.PlayerInputHandler.AttackInput[_inputIndex];
    }
}

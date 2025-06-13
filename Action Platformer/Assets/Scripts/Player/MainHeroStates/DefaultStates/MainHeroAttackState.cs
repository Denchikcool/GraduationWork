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
        _weapon.OnUseInput += HandleUseInput;
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }

    private void HandleUseInput()
    {
        mainHero.PlayerInputHandler.ChangeAttackInput(_inputIndex);
    }

    public override void Enter()
    {
        base.Enter();

        _weapon.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _weapon.CurrentInput = mainHero.PlayerInputHandler.AttackInput[_inputIndex];
        SoundEffectManager.PlaySound("Attack");
    }
}

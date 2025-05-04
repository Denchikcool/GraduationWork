namespace Denchik.Weapon.Components
{
    public class KnockbackData : ComponentData<AttackKnokback>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Knockback);
        }
    }
}

namespace Denchik.Weapon.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Damage);
        }
    }
}

namespace Denchik.Weapon.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        public DamageData()
        {
            ComponentDependency = typeof(Damage);
        }
    }
}

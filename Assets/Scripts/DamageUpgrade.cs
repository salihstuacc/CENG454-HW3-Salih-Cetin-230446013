public class DamageUpgrade : WeaponDecorator
{
    public DamageUpgrade(IWeapon weapon) : base(weapon) { }
    public override float GetDamage() => wrappedWeapon.GetDamage() + 5f; 
}
public class SpeedUpgrade : WeaponDecorator
{
    public SpeedUpgrade(IWeapon weapon) : base(weapon) { }
    public override float GetCooldown() => wrappedWeapon.GetCooldown() * 0.8f; 
}
public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon wrappedWeapon;

    public WeaponDecorator(IWeapon weapon)
    {
        wrappedWeapon = weapon;
    }

    public virtual float GetDamage() => wrappedWeapon.GetDamage();
    public virtual float GetCooldown() => wrappedWeapon.GetCooldown();
}
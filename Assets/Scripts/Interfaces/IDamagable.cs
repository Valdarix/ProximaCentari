
public interface IDamagable
{
    int Health { get; set; }
    void Damage(int damageAmount);
    bool IsAlive { get; set; }
}


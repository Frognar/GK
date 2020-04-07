public interface IKillable<T>
{
    T maxHealth { get; set; }
    T health { get; set; }

    void TakeDamage(T damage);
    void Die();
}

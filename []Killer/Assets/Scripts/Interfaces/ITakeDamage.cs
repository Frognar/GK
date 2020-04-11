public interface ITakeDamage<T>
{
    T MaxHealth { get; }
    T Health { get; }

    void TakeDamage(T damage);
    void Die();

}

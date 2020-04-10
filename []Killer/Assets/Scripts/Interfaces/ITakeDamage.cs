public interface ITakeDamage<T>
{
    T health { get; set; }
    T maxHealth { get; }

    void TakeDamage(T damage);
    void Die();

}

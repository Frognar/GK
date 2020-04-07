public interface IShotable<T>
{
    
    T damage { get; set; }
    float range { get; set; }
    float fireRate { get; set; }
    float impactForce { get; set; }

    void Shot();

}

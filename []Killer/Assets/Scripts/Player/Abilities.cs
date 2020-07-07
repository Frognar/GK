using UnityEngine;

/**
 * Author:          Mateusz Chłopek
 * Date:            23.05.2020
 * Collaborators:   
 */
public abstract class Ability
{
    //czas wyrażony w sekundach
    protected float cooldown = 0.0f;
    protected float leftTime = 0.0f;
    protected Player player;

    public Ability(Player player)
    {
        this.player = player;
    }

    public abstract void UseAbility();
    public abstract void Update(float deltaTime);
}

public class RecoverHealth : Ability
{
    private int healthRecovered = 20;
    private int pixelCost = 30;

    public RecoverHealth(Player player) : base(player)
    {
        this.cooldown = 10;
    }

    public override void Update(float deltaTime)
    {
        if (leftTime > 0)
        {
            leftTime -= deltaTime;
        }
        else
        {
            leftTime = 0.0f;
        }
        player.healthRecoverCdText.text = Mathf.Round(leftTime).ToString() + "s";

    }

    public override void UseAbility()
    {
        if (leftTime == 0.0f && player.Pixels > pixelCost)
        {
            leftTime = cooldown;

            player.Pixels -= pixelCost;
            player.Health += healthRecovered;
        }
    }
}

public class RecoverPixels : Ability
{
    private int pixelsRecovered = 20;
    private int healthCost = 30;

    public RecoverPixels(Player player) : base(player)
    {
        this.cooldown = 10;
    }

    public override void Update(float deltaTime)
    {
        if (leftTime > 0)
        {
            leftTime -= deltaTime;
        }
        else
        {
            leftTime = 0.0f;
        }
        player.ammoRecoverCdText.text = Mathf.Round(leftTime).ToString() + "s";

    }

    public override void UseAbility()
    {
        if (leftTime == 0.0f && player.Health > healthCost)
        {
            leftTime = cooldown;

            player.Health -= healthCost;
            player.Pixels += pixelsRecovered;
        }
    }
}
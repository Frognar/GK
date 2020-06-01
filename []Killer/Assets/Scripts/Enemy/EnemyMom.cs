using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author:         Anna Mach
 * Collaborators:   
 */

public class EnemyMom : Enemy
{
    public Spawner babiesSpawner;
    public DataToSaveLoad data;

    protected override void Start()
    {
        healthBar.SetMaxValue(maxHealth);
        Health = maxHealth;
        soundPlayer = GetComponent<SoundPlayer>();
        if (soundPlayer == null)
            Debug.LogWarning("No soundPlayer in NPC [" + this.name + "]");

        babiesSpawner = transform.parent.GetComponent<MomEnemyData>().babySpawner.GetComponent<Spawner>();
        data = transform.parent.GetComponent<MomEnemyData>().data.GetComponent<DataToSaveLoad>();
    }

    protected override void Die()
    {
        CreateDeathEffect();
        CreatePixelsToPick();
        GiveExp();

        soundPlayer?.PlaySoundEvent("EnemyDie");
        transform.position = new Vector3(0f, 0f, 0f);

        babiesSpawner.GetComponent<Spawner>().enabled = true;
        data.WasBaby = 1;

        Destroy(gameObject, .1f);
    }
}

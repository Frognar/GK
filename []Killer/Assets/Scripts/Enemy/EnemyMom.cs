using System;
using UnityEngine;
using UnityEngine.Rendering;

/**
 * Author:         Anna Mach
 * Collaborators:  Sebastian Przyszlak - usuwanie obiektu, kiedy nie powinien być na mapie
 */

public class EnemyMom : Enemy
{
    public static bool shouldBeDead = false;
    private Spawner babiesSpawner;
    private DataToSaveLoad data;
    private UnityEngine.Video.VideoPlayer vidPlayer;
    private GameObject player;
    private GameObject cam;

    private void Awake()
    {
        if (shouldBeDead) Destroy(gameObject);
        SaveLoad.OnLoadData += SaveLoadOnOnLoadData;
    }

    private void SaveLoadOnOnLoadData(bool obj)
    {
        if (obj) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        shouldBeDead = true;
    }

    protected override void Start()
    {

        babiesSpawner = transform.parent.GetComponent<MomEnemyData>().babySpawner.GetComponent<Spawner>();
        data = transform.parent.GetComponent<MomEnemyData>().data.GetComponent<DataToSaveLoad>();
        player = transform.parent.GetComponent<MomEnemyData>().player;
        cam = transform.parent.GetComponent<MomEnemyData>().cam;

        vidPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        vidPlayer.targetCamera = cam.GetComponent<Camera>();
        //vidPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        vidPlayer.loopPointReached += EndReached;
        vidPlayer.Prepare();

        healthBar.SetMaxValue(maxHealth);
        Health = maxHealth;
        soundPlayer = GetComponent<SoundPlayer>();
        if (soundPlayer == null)
            Debug.LogWarning("No soundPlayer in NPC [" + this.name + "]");
    }

    protected override void Die()
    {
        CreateDeathEffect();
        GiveExp();
        soundPlayer?.PlaySoundEvent("EnemyDie");

        // Disable player movement
        player.GetComponent<MouseInput>().enabled = false;
        player.GetComponent<KeyboardInput>().enabled = false;

        // Disable camera effects
        cam.GetComponent<Volume>().weight = 0;
        // Cutscene start
        vidPlayer.Play();

    }

    // Cutscene end
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        cam.GetComponent<Volume>().weight = 1;

        CreatePixelsToPick();
        transform.position = new Vector3(0f, 0f, 0f);

        //Player can move again
        player.GetComponent<MouseInput>().enabled = true;
        player.GetComponent<KeyboardInput>().enabled = true;

        babiesSpawner.GetComponent<Spawner>().enabled = true;
        data.WasBaby = true;        

        Destroy(gameObject, .1f);
    }
}

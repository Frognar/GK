using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    #region Singleton
    public static GameManager instance;

    void Awake () {
        if (instance == null)
            instance = this;
        else {
            Destroy (gameObject);
            return;
        }

    }
    #endregion

    public GameObject deathPanel;
    [SerializeField] private Transform playerSpawn;
    private GameObject playerGO;
    private Player player;
    private bool respawning = false;

    //MusicChanger
    public static bool inBattle = false;

    void Start () {
        playerGO = PlayerManager.instance.player;
        playerGO.transform.position = playerSpawn.position;
        player = playerGO.GetComponent<Player> ();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update () {
        HandleMusicTheme ();

        if (!player.IsAlive && !respawning)
            Respawn ();
    }

    void HandleMusicTheme () {
        bool battleMusic = SoundManager.instance.IsSoundPlaying ("BattleTheme");

        if (inBattle && !battleMusic)
            SoundManager.instance.ChangeMusicInitializeCoroutine ("GameTheme", "BattleTheme");

        if (!inBattle && battleMusic)
            SoundManager.instance.ChangeMusicInitializeCoroutine ("BattleTheme", "GameTheme");
    }

    void Respawn () {
        StartCoroutine (PlayerRespawn ());
        respawning = true;
    }

    IEnumerator PlayerRespawn () {
        deathPanel.SetActive (true);
        yield return new WaitForSeconds (2f);
        playerGO.transform.position = playerSpawn.position;
        yield return new WaitForSeconds (.2f);
        player.ResetPlayer ();
        deathPanel.SetActive (false);
        respawning = false;
    }

}

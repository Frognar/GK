using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public GameObject deathPanel;
    [SerializeField] private Transform playerSpawn;
    private GameObject player;
    private bool respawning = false;

    //MusicChanger
    private bool playingBattleMusicNow = false;
    public static bool inBattle = false;


    void Start()
    {
        player = PlayerManager.instance.player;
        player.transform.position = playerSpawn.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (!player.GetComponent<Player>().IsAlive && !respawning)
        {
            StartCoroutine(PlayerRespawn());
            respawning = true;
        }

        if (inBattle && !playingBattleMusicNow)
        {
            SoundManager.instance.ChangeMusicInicjalizeCoroution("GameTheme", "BattleTheme");
            playingBattleMusicNow = true;
        }

        else if (!inBattle && playingBattleMusicNow)
        {
            SoundManager.instance.ChangeMusicInicjalizeCoroution("BattleTheme", "GameTheme");
            playingBattleMusicNow = false;
        }
    }

    IEnumerator PlayerRespawn()
    {
        deathPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        player.transform.position = playerSpawn.position;
        yield return new WaitForSeconds(.2f);
        player.GetComponent<Player>().ResetPlayer();
        deathPanel.SetActive(false);
        respawning = false;
    }

}

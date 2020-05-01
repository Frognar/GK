using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform playerCamera;
    public GUIBar healthBar;
    public GameObject bar;

    void Start()
    {
        playerCamera = PlayerManager.instance.player.transform.Find("Camera");
    }

    void Update()
    {
        if (healthBar.IsMaxValue())
            bar.SetActive(false);
        else
            bar.SetActive(true);
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + playerCamera.forward);
    }
}

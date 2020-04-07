using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{

    public Transform cam;
    public HealthBar healthBar;
    public GameObject bar;

    void Update()
    {
        if (healthBar.IsMaxValue())
            bar.SetActive(false);
        else
            bar.SetActive(true);
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}

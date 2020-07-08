using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaby : MonoBehaviour
{
    private void Start()
    {
        SaveLoad.OnLoadData += SaveLoadOnOnLoadData;
    }

    private void SaveLoadOnOnLoadData(bool obj)
    {
        if (!obj) Destroy(gameObject);
    }
}

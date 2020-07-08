using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/**
 * Author:          Anna Mach
 * Date:            17.05.2020
 * Collaborators:   
 */
public class DataToSaveLoad : MonoBehaviour
{
    enum DataInfo
    {
        NoOfEnemyAttacking = 0,
        WasBabyKwadrat = 1,
        Level = 2,
        Experience = 3
    }

    //Source of data
    public GameObject player;
    public List<GameObject> spawnersEnemiesAttacking = new List<GameObject>();
    public GameObject babySpawner;
    public GameObject mommySpawner;

    //What to save
    private int level;
    private int experience;

    private int noOfEnemyAttacking = 0;
    private int wasBaby = 0;

    public bool WasBaby { get; set; }

    public void ResetBaby()
    {
        wasBaby = 0;
        babySpawner.GetComponent<Spawner>().enabled = false;
        mommySpawner.GetComponent<Spawner>().enabled = true;
    }

    public void RefreshStoredData()
    {
        level = player.GetComponent<Player>().Level;
        experience = player.GetComponent<Player>().Exp;
    }

    public void PutIntoGameAllData()
    {
        player.GetComponent<Player>().Level = level;
        player.GetComponent<Player>().Exp = experience;

        if (wasBaby == 1)
        {
            babySpawner.GetComponent<Spawner>().enabled = true;
            mommySpawner.GetComponent<Spawner>().enabled = false;
        }
        else
        {
            babySpawner.GetComponent<Spawner>().enabled = false;
            mommySpawner.GetComponent<Spawner>().enabled = true;
        }

        foreach (GameObject spawner in spawnersEnemiesAttacking)
        {
            spawner.GetComponent<Spawner>().maxObjectsSpawned = noOfEnemyAttacking;
        }
    }

    public void IncreaseMaxEnemiesAttacking()
    {
        noOfEnemyAttacking++;

        foreach (GameObject spawner in spawnersEnemiesAttacking)
        {
            spawner.GetComponent<Spawner>().maxObjectsSpawned = noOfEnemyAttacking;
        }
    }

    public Dictionary<int, int> GetAllDataAsDictionary()
    {
        Dictionary<int, int> dictionary = new Dictionary<int, int>();

        dictionary.Add((int)DataInfo.NoOfEnemyAttacking, noOfEnemyAttacking);
        dictionary.Add((int)DataInfo.WasBabyKwadrat, wasBaby);
        dictionary.Add((int)DataInfo.Level, level);
        dictionary.Add((int)DataInfo.Experience, experience);

        return dictionary;
    }

    public void SetAllDataFromDictionary(Dictionary<int, int> dataToGet)
    {
        foreach (KeyValuePair<int, int> item in dataToGet)
        {
            DataInfo key = (DataInfo)item.Key;

            switch (key)
            {
                case DataInfo.NoOfEnemyAttacking:
                    noOfEnemyAttacking = item.Value;
                    break;
                case DataInfo.WasBabyKwadrat:
                    wasBaby = item.Value;
                    break;
                case DataInfo.Level:
                    level = item.Value;
                    break;
                case DataInfo.Experience:
                    experience = item.Value;
                    break;
            }
        }
    }
}

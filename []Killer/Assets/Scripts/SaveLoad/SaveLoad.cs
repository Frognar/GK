using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/**
 * Author:          Anna Mach
 * Date:            22.05.2020
 * Collaborators:   
 */
public class SaveLoad : MonoBehaviour
{
    public static event Action<bool> OnLoadData = delegate {  };
    private string filePath = "68656c6c6f7468657265.dat";
    public DataToSaveLoad data;

    private void Start()
    {
        //Load();
    }

    public void Save()
    {
        File.WriteAllText(filePath, string.Empty);

        StreamWriter file = new StreamWriter(filePath, true);
        if(file != null)
        {

            Dictionary<int, int> dataSave = data.GetAllDataAsDictionary();
            foreach (KeyValuePair<int, int> item in dataSave)
            {
                file.WriteLine(item.Key.ToString() + ' ' + item.Value.ToString());
            }

            file.Close();
        }
        else
        {
            Debug.LogError("Couldn't open file.");
        }
    }

    public void Load()
    {
        StreamReader file = new StreamReader(filePath);

        if(file != null)
        {
            string line;
            string[] splittedLine;
            int key, value;
            Dictionary<int, int> dataLoad = new Dictionary<int, int>();

            while((line = file.ReadLine()) != null)
            {
                splittedLine = line.Split();

                if (splittedLine[0] != null && splittedLine[1] != null)
                {
                    if(Int32.TryParse(splittedLine[0], out key) && Int32.TryParse(splittedLine[1], out value))
                    {
                        dataLoad.Add(key, value);
                    }
                }
            }

            data.SetAllDataFromDictionary(dataLoad);
            data.PutIntoGameAllData();
            Debug.Log(data.WasBaby);
            OnLoadData(data.WasBaby);
        }
        else
        {
            Debug.LogError("Couldn't open file.");
        }

        file.Close();
    }
}

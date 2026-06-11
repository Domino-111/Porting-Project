using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    void Start()
    {
        //Try to load the game info on start
        foreach (var item in FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersist>())
        {
            item.Load();
        }
    }

    void OnApplicationQuit()
    {
        //Set all of our saved values to PlayerPrefs
        foreach (var item in FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersist>())
        {
            item.Save();
        }

        //Save changes to PlayerPrefs
        PlayerPrefs.Save();
    }
}

public interface IDataPersist
{
    public void Save();
    public void Load();
}

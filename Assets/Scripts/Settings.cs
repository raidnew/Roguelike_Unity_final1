using UnityEngine;

public class Settings
{
    public static void Save(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
    }

    public static int Load(string name)
    {
        return PlayerPrefs.GetInt(name);
    }

}
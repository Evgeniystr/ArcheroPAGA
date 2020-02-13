using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SettingsLoader
{
    public static T LoadSettings<T>()
    {
        T settingsData = default(T);

        var path = Path.Combine(Application.streamingAssetsPath, "Settings", typeof(T).ToString() + ".json");

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            settingsData = JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            Debug.LogError($"Cant find settings file: {path}");
        }

        return settingsData;
    }

}

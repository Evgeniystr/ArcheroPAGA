using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class SettingsLoader
{
    public static T LoadSettings<T>()
    {
        var settingsData = default(T);

        var path = Path.Combine(Application.streamingAssetsPath, "Settings", typeof(T).ToString() + ".json");
        var json = string.Empty;

        if(Application.platform == RuntimePlatform.Android)
        {
            var reader = new WWW(path);
            while (!reader.isDone){}
            json = reader.text;
        }
        else
        {
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            else
            {
                Debug.LogError($"Cant find settings file: {path}");
            }
        }

        settingsData = JsonConvert.DeserializeObject<T>(json);
        return settingsData;
    }
}

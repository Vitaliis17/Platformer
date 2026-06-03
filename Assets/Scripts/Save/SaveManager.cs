using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string _path;

    private void Awake()
        => _path = Application.persistentDataPath + "/Save.json";

    public void Save()
    {
        SaveData data = GetData();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);
    }

    private SaveData GetData()
    {
        Player player = FindFirstObjectByType<Player>();
        Key key = FindFirstObjectByType<Key>();

        bool isDoorOpen = key == null || key.gameObject.activeSelf == false;

        Vector2 playerPosition = player == null ? Vector2.zero : player.transform.position;
        Vector2 keyPosition = key == null ? Vector2.zero : key.transform.position;

        return new(playerPosition, keyPosition, isDoorOpen);
    }
}
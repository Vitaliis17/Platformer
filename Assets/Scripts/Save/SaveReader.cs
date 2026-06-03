using System.IO;
using UnityEngine;

public class SaveReader : MonoBehaviour
{
    private string _path;

    private void Awake()
        => _path = Application.persistentDataPath + "/Save.json";

    private void Start()
    {
        SaveData data = Read();

        if (data == null)
            return;

        Set(data);
    }

    private SaveData Read()
    {
        if (File.Exists(_path) == false)
            return null;

        string json = File.ReadAllText(_path);

        return JsonUtility.FromJson<SaveData>(json);
    }

    private void Set(SaveData data)
    {
        FindFirstObjectByType<Player>().transform.position = data.PlayerPosition;

        Key key = FindFirstObjectByType<Key>();

        if (data.IsDoorOpen)
        {
            FindFirstObjectByType<CloseDoor>().gameObject.SetActive(false);
            FindFirstObjectByType<OpenDoor>(FindObjectsInactive.Include).gameObject.SetActive(true);

            key.gameObject.SetActive(false);
        }
        else
        {
            key.transform.position = data.KeyPosition;
        }
    }
}

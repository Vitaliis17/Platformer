using System.IO;
using UnityEngine;

public class SaveRemover : MonoBehaviour
{
    private string _path;

    private void Awake()
        => _path = Application.persistentDataPath + "/Save.json";

    public void Remove()
    {
        if (File.Exists(_path) == false)
            return;

        File.Delete(_path);
    }
}

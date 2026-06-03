using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public Vector2 PlayerPosition;
    public Vector2 KeyPosition;

    public bool IsDoorOpen;

    public SaveData(Vector2 playerPosition, Vector2 keyPosition, bool isDoorOpen)
    {
        PlayerPosition = playerPosition;
        KeyPosition = keyPosition;

        IsDoorOpen = isDoorOpen;
    }
}
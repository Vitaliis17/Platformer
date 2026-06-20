using UnityEngine;

public class Quiter : MonoBehaviour, IQuiter
{
    public void Quit()
        => Application.Quit();
}
using UnityEngine;

public class Quiter : IQuiter
{
    public void Quit()
        => Application.Quit();
}
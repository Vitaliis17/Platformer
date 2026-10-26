using UnityEngine;
using Zenject;

public class Factory<T> : PlaceholderFactory<T> where T : Component
{
}
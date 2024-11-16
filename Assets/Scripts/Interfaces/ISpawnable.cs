using System;
using UnityEngine;

public interface ISpawnable
{
    event Action<GameObject> OnDestroyed;
}
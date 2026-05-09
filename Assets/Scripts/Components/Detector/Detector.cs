using UnityEngine;

public abstract class Detector : ScriptableObject
{
    public abstract Unit[] Search(Vector3 pos, Vector3 size, Quaternion rotation);
}

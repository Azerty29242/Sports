using UnityEngine;

public static class Extensions
{
    public static GameObject[] Pop(this GameObject[] objects, int index)
    {
        for (int i = index + 1; i < objects.Length; i++)
        {
            objects[i - 1] = objects[i];
        }
        return objects;
    }
}
using UnityEngine;

public class ElevatorSong : MonoBehaviour
{
    void Awake()
    {
        GameObject[] elevatorSong = GameObject.FindGameObjectsWithTag("MainMusic");

        if (elevatorSong.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}

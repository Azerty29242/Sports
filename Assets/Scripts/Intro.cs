using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>().Pause();

        VideoPlayer video = GetComponent<VideoPlayer>();

        video.loopPointReached += EndReached;
    }

    private void EndReached(VideoPlayer video)
    {
        SceneManager.LoadScene("Credits");
    }
}
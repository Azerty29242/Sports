using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject[] targets;
    public TMP_Text scoreText;
    public TMP_Text lifeCountText;
    public AudioSource CrowdCheers;
    public AudioSource RefereeWhistle;
    private bool keysEnabled = true;
    private float pauseTime = 0;

    void Start()
    {
        GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>().Pause();

        PlayerStats.score = 0;
        PlayerStats.lifeCount = 3;

        UpdateTargets();
    }

    void Update()
    {
        if (pauseTime <= 0)
        {
            if (keysEnabled)
            {
                if (Input.GetKeyUp("w"))
                {
                    keysEnabled = false;
                    Goal(0);
                }
                else if (Input.GetKeyUp("d"))
                {
                    keysEnabled = false;
                    Goal(1);
                }
                else if (Input.GetKeyUp("a"))
                {
                    keysEnabled = false;
                    Goal(2);
                }
                else if (Input.GetKeyUp("s"))
                {
                    keysEnabled = false;
                    Goal(3);
                }
                else if (Input.GetKeyUp("g"))
                {
                    keysEnabled = false;
                    Goal(4);
                }
                else if (Input.GetKeyUp("space"))
                {
                    keysEnabled = false;
                    Miss();
                }
            }
        }
        else
        {
            pauseTime -= Time.deltaTime;
        }
    }

    void Goal(int targetIndex)
    {
        if (targets[targetIndex].activeSelf)
        {
            PlayerStats.score += 1;

            CrowdCheers.Play();
            StartCoroutine(WaitForEndOfAudio(CrowdCheers, UpdateTargets));
        }
        else
        {
            Miss();
            UpdateTargets();
        }

        UpdateStats();
    }

    void Miss()
    {
        PlayerStats.lifeCount -= 1;
        if (PlayerStats.lifeCount == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            UpdateTargets();
            UpdateStats();
        }
    }

    void UpdateTargets()
    {
        GameObject[] enabledTargets = new GameObject[5];
        for (int index = 0; index < targets.Length; index++)
        {
            targets[index].SetActive(true);
        }
        targets.CopyTo(enabledTargets, 0);
        for (int index = 0; index < 2; index ++)
        {
            int shiftIndex = Random.Range(0, enabledTargets.Length);
            GameObject removedObject = enabledTargets[shiftIndex];
            removedObject.SetActive(false);
            enabledTargets = enabledTargets.Pop(shiftIndex);
        }

        RefereeWhistle.Play();
        StartCoroutine(WaitForEndOfAudio(RefereeWhistle, () =>
        {
            keysEnabled = true;
            pauseTime = 1;
        }));
    }

    void UpdateStats()
    {
        scoreText.text = "Score : " + PlayerStats.score.ToString();
        lifeCountText.text = "Lives : " + PlayerStats.lifeCount.ToString();
    }

    IEnumerator WaitForEndOfAudio(AudioSource audio, System.Action callback)
    {
        Debug.Log(audio.name + " has begun to play.");
        while (CrowdCheers.isPlaying)
        {
            yield return null;
        }

        Debug.Log(audio.name + " has finished to play.");
        callback();
    }
}
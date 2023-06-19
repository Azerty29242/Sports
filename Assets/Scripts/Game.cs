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

    void Start()
    {
        GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>().Pause();

        PlayerStats.score = 0;
        PlayerStats.lifeCount = 3;

        UpdateTargets();
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            Goal(0);
        }
        else if (Input.GetKeyDown("d"))
        {
            Goal(1);
        }
        else if (Input.GetKeyDown("a"))
        {
            Goal(2);
        }
        else if (Input.GetKeyDown("s"))
        {
            Goal(3);
        }
        else if (Input.GetKeyDown("g"))
        {
            Goal(4);
        }
        else if (Input.GetKeyDown("space"))
        {
            Miss();
        }
    }

    void Goal(int targetIndex)
    {
        if (targets[targetIndex].activeSelf)
        {
            PlayerStats.score += 1;

            CrowdCheers.Play();
            StartCoroutine(WaitForEndOfCrowdCheers());
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
    }

    void UpdateStats()
    {
        scoreText.text = "Score : " + PlayerStats.score.ToString();
        lifeCountText.text = "Lives : " + PlayerStats.lifeCount.ToString();
    }

    IEnumerator WaitForEndOfCrowdCheers()
    {
        while (CrowdCheers.isPlaying)
        {
            yield return null;
        }

        UpdateTargets();
    }
}

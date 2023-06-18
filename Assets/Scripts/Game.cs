using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject[] targets;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public AudioSource CrowdCheers;
    public AudioSource RefereeWhistle;
    private int score = 0;
    private int lives = 3;

    void Start()
    {
        GameObject.FindGameObjectWithTag("MainMusic").GetComponent<AudioSource>().Pause();
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
            score += 1;

            CrowdCheers.Play();
            StartCoroutine(WaitForEndOfCrowdCheers());
        }
        else
        {
            Miss();
            UpdateTargets();
        }

        UpdateInfo();
    }

    void Miss()
    {
        lives -= 1;
        if (lives == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            UpdateTargets();
            UpdateInfo();
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

    void UpdateInfo()
    {
        scoreText.text = "Score : " + score.ToString();
        livesText.text = "Lives : " + lives.ToString();
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

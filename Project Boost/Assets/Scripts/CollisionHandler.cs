using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelayDuration;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip levelFinishClip;

    [SerializeField] ParticleSystem crashParticleEffect;
    [SerializeField] ParticleSystem levelFinishParticleEffect;

    AudioSource audioSource;
    bool isTransitioning, debugCollisionActive;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        checkCollisionDebugPressed();
        checkNextLevelDebugPressed();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friend!");
                break;
            case "Finish":
                StartLoadNextLevelSequence();
                break;
            default:
                if (!debugCollisionActive)
                {
                    StartReloadSequence();
                }
                break;
        }
    }

    //! debug based functions
    void checkCollisionDebugPressed()
    {
        if (Input.GetKey(KeyCode.L))
            debugCollisionActive = !debugCollisionActive;
    }

    void checkNextLevelDebugPressed()
    {
        if (Input.GetKey(KeyCode.C))
            StartLoadNextLevelSequence();
    }


    void StartReloadSequence()
    {
        isTransitioning = true;
        crashParticleEffect.Play();
        audioSource.Stop();
        //! this stops all audio present in that scene
        audioSource.PlayOneShot(crashClip);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelayDuration);


    }

    void StartLoadNextLevelSequence()
    {
        isTransitioning = true;
        levelFinishParticleEffect.Play();
        audioSource.Stop();
        //! this stops all audio present in that scene
        audioSource.PlayOneShot(levelFinishClip);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelayDuration);

    }
    void ReloadLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentLevel);
    }
    void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevel + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}

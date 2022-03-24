using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelayDuration;
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friend!");
                break;
            case "Finish":
                StartLoadNextLevelSequence();
                break;
            default:
                StartReloadSequence();
                break;
        }
    }
    void StartReloadSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelayDuration);
    }

    void StartLoadNextLevelSequence()
    {
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

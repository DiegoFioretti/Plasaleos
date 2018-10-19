using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolderUIManager : MonoBehaviour {
    private void Awake() {
        Screen.sleepTimeout = 15;
    }

    public void LoadLevel1() {
        SceneManager.LoadScene("Levelb01");
    }

    public void LoadLevel2() {
        SceneManager.LoadScene("Levelb02");
    }

    public void LoadLevel3() {
        SceneManager.LoadScene("Levelb03");
    }

    public void LoadLevel4() {
        SceneManager.LoadScene("Levelb04");
    }

    public void LoadLevel5() {
        SceneManager.LoadScene("Levelb05");
    }

    public void LoadLevel6() {
        SceneManager.LoadScene("Levelb06");
    }

    public void LoadLevel7() {
        SceneManager.LoadScene("testLevel_06");
    }

    public void LoadLevel8() {
        SceneManager.LoadScene("testLevel_07");
    }

    public void LoadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void NextLevel() {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
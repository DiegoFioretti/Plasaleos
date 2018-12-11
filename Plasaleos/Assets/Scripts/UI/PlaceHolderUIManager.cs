using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolderUIManager : MonoBehaviour {
    private void Awake() {
        Screen.sleepTimeout = 15;
        Time.timeScale = 1f;
    }

    public void LoadLevel1() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_00");
    }

    public void LoadLevel2() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_01");
    }

    public void LoadLevel3() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_02");
    }

    public void LoadLevel4() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_03");
    }

    public void LoadLevel5() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_04");
    }

    public void LoadLevel6() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_05");
    }

    public void LoadLevel7() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_06");
    }

    public void LoadLevel8() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_07");
    }

    public void LoadLevel9() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_08");
    }

    public void LoadLevel10() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_09");
    }

    public void LoadLevel11() {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_10");
    }

    public void LoadLevel12()
    {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_11");
    }

    public void LoadLevel13()
    {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene("testLevel_12");
    }

    public void LoadLevel(string level) {
        GameManager.instance.resetLevel = false;
        SceneManager.LoadScene(level);
    }

    public void ResetLevel() {
        GameManager.instance.resetLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
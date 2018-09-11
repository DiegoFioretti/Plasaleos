using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolderUIManager : MonoBehaviour {

    public void LoadLevel1() {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2() {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3() {
        SceneManager.LoadScene("Level3");
    }

}
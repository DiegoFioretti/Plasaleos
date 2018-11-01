using UnityEngine;

public class TimeStop : MonoBehaviour {

    public void Stop(bool state) {
        if (state) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

}
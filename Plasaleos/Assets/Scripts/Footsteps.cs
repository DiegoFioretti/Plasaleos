using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    [SerializeField] private float timePerSteps;
    private float timer = 0;

    public string defaultSound;

    private Movement mov;

    private void Start()
    {
        mov = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update () {
        if (mov.IsGrounded())
        {
            timer += Time.deltaTime;
            if (timer > timePerSteps)
            {
                AkSoundEngine.PostEvent(defaultSound, gameObject);
                timer = 0;
            }
        }
	}
}

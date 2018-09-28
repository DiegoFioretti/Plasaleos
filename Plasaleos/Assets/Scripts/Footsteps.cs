using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

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
            GetComponent<Animator>().SetBool("Falling", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Falling", true);
        }
	}

    public void Step()
    {
        AkSoundEngine.PostEvent(defaultSound, gameObject);
    }
}

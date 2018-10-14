using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    public string defaultSound;

    private Entity entity;

    private void Start() {
        entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update() {
        if (entity.Grounded) {
            GetComponent<Animator>().SetBool("Falling", false);
        } else {
            GetComponent<Animator>().SetBool("Falling", true);
        }
    }

    public void Step() {
        AkSoundEngine.PostEvent(defaultSound, gameObject);
    }
}
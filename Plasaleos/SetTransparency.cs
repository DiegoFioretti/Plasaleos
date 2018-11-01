using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildTransparency : MonoBehaviour {

    [SerializeField] private float value = 0.5f;

    // Use this for initialization
    void Start()
    {
        Image[] image = GetComponentsInChildren<Image>();
        for (int i = 0; i < image.Length; i++)
        {
            image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, value);
        }
    }
}

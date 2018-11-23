using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockLevel : MonoBehaviour {

    private Button button;
    //[SerializeField] private Text text;
    [SerializeField] private int unlock;
    [SerializeField] private int requiredPieces;

    [SerializeField] private Color locked = Color.gray;
    private Color unlocked = new Color(1,1,1,1);


    // Use this for initialization
    void Start () {
        button = gameObject.GetComponent<Button>();
        if(unlock > GameManager.instance.AlienCount)
        {
            button.enabled = false;
            button.image.color = locked;
            //text.text = "Save " + unlock + " Aliens";
        }
        else if(requiredPieces > GameManager.instance.PieceCount)
        {
            button.enabled = false;
            button.image.color = locked;
            //text.text = "Get " + requiredPieces + " ship pieces";
        }
        else
        {
            button.image.color = unlocked;
        }
    }
}

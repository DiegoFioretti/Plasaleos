using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfLocked : MonoBehaviour {

    [SerializeField] private int unlock;
    [SerializeField] private int requiredPieces;

    // Use this for initialization
    void Start () {
        if (unlock <= GameManager.instance.AlienCount && requiredPieces <= GameManager.instance.PieceCount)
        {
            gameObject.SetActive(false);
        }
    }
}

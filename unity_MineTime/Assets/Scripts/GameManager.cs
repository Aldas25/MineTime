using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //public GameObject playerPrefab; // instantiate once - when instantiated, make it dontdestroyonload
    //public GameObject player; // and save it here

    void Awake () {
        DontDestroyOnLoad(this);
    }
}

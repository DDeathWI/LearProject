﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    

    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("health"));
    }
    
}

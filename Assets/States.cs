using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

//[InitializeOnLoad]
public static class States
{
    public static int InitialLives = 9;
    public static int Lives {get;set;}
    static States(){
        Lives = InitialLives;
    }
}

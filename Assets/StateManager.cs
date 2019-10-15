using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    [SerializeField] private Text LivesText;
    [SerializeField] private Text LevelText;

    // Start is called before the first frame update
    void Start()
    {   
        if(LivesText != null)
        {
            LivesText.color = Color.white;
            LivesText.text = "Lives: " + States.Lives;
        }
        
        if(LevelText!= null && SceneManager.GetActiveScene() != null){
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  
            LevelText.color = Color.white;
            LevelText.text = "Level: " + (currentSceneIndex + 1);     
        }

    }

    // Update is called once per frame
    void Update()
    {   
        
    }
}

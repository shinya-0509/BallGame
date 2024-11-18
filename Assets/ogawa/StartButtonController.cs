using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButtonController : MonoBehaviour
{

    public void OnChangeSceneButtonClick()
    {
        SceneManager.LoadScene("WatakiScene");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

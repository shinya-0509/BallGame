using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xButtonController : MonoBehaviour
{
    public GameObject HowtoplayCanvas;
    public GameObject RankingCanvas;

    public void ShowHowToPlay()
    {
        HowtoplayCanvas.SetActive(false);
    }

    public void ShowRanking()
    {
        RankingCanvas.SetActive(false);
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

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameStarted;
    public bool GameFinished;
    public PlayerController PlayerController;

    public static GameManager instance;
    private InGameRanking ig;
    private GameObject[] runner;
    List<RankingSystem> sortArray = new List<RankingSystem>();
    

    public void Awake()
    {
        instance = this;
        GameStarted = false;

        runner = GameObject.FindGameObjectsWithTag("runner");
        ig= FindObjectOfType<InGameRanking>();

    }
    public void Start()
    {
        for(int i=0; i<runner.Length; i++)
        {
            sortArray.Add(runner[i].GetComponent<RankingSystem>());
        }
    }

    
    public void Update()
    {
        CalculateRanking();
        if (Input.GetMouseButtonDown(0))
        {
            GameStarted = true;
           
        }
    }


    void CalculateRanking()
    {
        sortArray= sortArray.OrderBy(x => x.distance).ToList();
        sortArray[0].rank = 1;
        sortArray[1].rank = 1;
        sortArray[2].rank = 1;
        sortArray[3].rank = 1;
        sortArray[4].rank = 1;

        ig.a = sortArray[4].name;
        ig.b = sortArray[3].name;
        ig.c = sortArray[2].name;
        ig.d = sortArray[1].name;
        ig.e = sortArray[0].name;
    }
}

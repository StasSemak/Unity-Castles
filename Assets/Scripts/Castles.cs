using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode { idle, playing, levelEnd }

public class Castles : MonoBehaviour
{
    static private Castles instance;

    [Header("Set in Inspector")]
    public Text textLevel;
    public Text textShots;
    public Text textButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    void Start()
    {
        instance = this;

        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if(castle != null) Destroy(castle);

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject go in gos)
        {
            Destroy(go);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        ProjectileLine.S.Clear();

        Goal.goalMet = false;
        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI()
    {
        textLevel.text = "Level " + (level + 1) + "of " + levelMax;
        textShots.text = "Shots taken: " + shotsTaken;
    }

    public void SwitchView(string eView = "")
    {
        if (eView == "") eView = textButton.text;

        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCamera.POI = null;
                textButton.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCamera.POI = instance.castle;
                textButton.text = "Show Both";
                break;
            case "Show Both":
                FollowCamera.POI = GameObject.Find("ViewBoth");
                textButton.text = "Show Slingshot";
                break;
            default:
                break;
        }
    }

    void Update()
    {
        UpdateGUI();

        if((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke(nameof(NextLevel), 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if(level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public static void ShotFired()
    {
        instance.shotsTaken++;
    }
}

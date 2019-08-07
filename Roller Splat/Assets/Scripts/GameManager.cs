using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;

    private GroundPiece[] allGroundPieces;

    private void Start()
    {
        SetUpNewLevels();
    }


    private void SetUpNewLevels()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
    }

}

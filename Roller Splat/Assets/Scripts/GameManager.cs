﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /**********************************************************************************************/
    /* Singleton                                                                            */
    /**********************************************************************************************/

    #region Singleton

    public static GameManager singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion //Singleton

    /**********************************************************************************************/
    /* Private fields                                                                             */
    /**********************************************************************************************/

    #region Private fields

    private GroundPiece[] allGroundPieces;

    #endregion // Private fields

    /**********************************************************************************************/
    /* MonoBehaviour                                                                              */
    /**********************************************************************************************/

    #region MonoBehaviour

    private void Start()
    {
        SetUpNewLevel();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    #endregion // MonoBehaviour


    /**********************************************************************************************/
    /* Private methods                                                                            */
    /**********************************************************************************************/

    #region Private methods

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetUpNewLevel();
    }

    private void SetUpNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
    }

    private void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)//Check if all levels have been completed
        {
            SceneManager.LoadScene(0);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    #endregion // Private methods

    /**********************************************************************************************/
    /* Public methods                                                                             */
    /**********************************************************************************************/

    #region Public methods

    public void CheckComplete()
    {
        bool isFinished = true;

        for (int i = 0; i < allGroundPieces.Length; i++)
        {
            if (allGroundPieces[i].isColored == false)
            {
                isFinished = false;
                break;
            }
        }
        if (isFinished)
        {
            NextLevel();
        }
    }

    #endregion // Public methods
}

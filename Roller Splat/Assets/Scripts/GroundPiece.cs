using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
    /**********************************************************************************************/
    /* Private fields                                                                             */
    /**********************************************************************************************/

    #region Private fields

    [HideInInspector]
    public bool isColored = false;

    #endregion // Private fields


    /**********************************************************************************************/
    /* Public methods                                                                             */
    /**********************************************************************************************/

    #region Public methods

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;

        GameManager.singleton.CheckComplete();
    }


    #endregion // Public methods
}

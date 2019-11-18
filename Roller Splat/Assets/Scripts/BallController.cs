using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	/**********************************************************************************************/
	/* Private fields                                                                             */
	/**********************************************************************************************/

	#region Private fields

	public static GameObject ball;
    private Rigidbody rb;

	[SerializeField]
	public Color color;
	public ParticleSystem splashParticle;
	ParticleSystem.MainModule ma;
	[SerializeField]
    private float speed;

    private bool isTravelling;

    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;

    [SerializeField]
    private int minSwipeRecognition = 500;

    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    private Color solveColor;

    #endregion //Private fields


    /**********************************************************************************************/
    /* MonoBehaviour                                                                              */
    /**********************************************************************************************/

    #region MonoBehaviour


    public void Start()
    {

		ma = splashParticle.main;

		ball = this.gameObject;
		solveColor = color;
        GetComponent<MeshRenderer>().material.color = solveColor;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
		ma.startColor = color;
		if (isTravelling)
        {
           rb.velocity = speed*travelDirection;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.04f);

        int i = 0;
        while (i<hitColliders.Length)
        {
            GroundPiece ground = hitColliders[i].transform.GetComponent<GroundPiece>();

            if (ground && !ground.isColored)
            {
                ground.ChangeColor(solveColor);
            }

            i++;
        }

        if (nextCollisionPosition !=Vector3.zero)
        {
            if (Vector3.Distance(transform.position,nextCollisionPosition)<1)
            {
                isTravelling = false;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            }
        }
        if (isTravelling)
            return;
        

        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLastFrame !=Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                if (currentSwipe.sqrMagnitude<minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();
                
                if (currentSwipe.x>-0.5f && currentSwipe.x<0.5f)
                {
                    //Go UP/Down

                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }
                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    //Go Left/Right
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }
            swipePosLastFrame = swipePosCurrentFrame;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
    }

    #endregion // MonoBehaviour

    /**********************************************************************************************/
    /* Private methods                                                                            */
    /**********************************************************************************************/

    #region Private methods

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;

        RaycastHit hit;

        if (Physics.Raycast(transform.position,direction,out hit,10f))
        {
            nextCollisionPosition = hit.point;
        }

        isTravelling = true;
    }

    #endregion // Private methods
}

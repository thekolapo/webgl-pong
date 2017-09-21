using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketControllerScript : MonoBehaviour {
	private Rigidbody2D rigidBody;
	private float axisValue;
	public float speed;
	public string axis;
	private Transform ball;
	public bool isAI;
	private static bool aiOpponentCanMove;
	public static RacketControllerScript main;
	private static bool canMove;
	public static bool freezeLeftRacketMovement;
	
	void Awake(){
		main = this;
		rigidBody = GetComponent<Rigidbody2D> ();
		ball = GameObject.Find ("Ball").transform;
	}

	// Use this for initialization
	void Start () {
		axisValue = 0;
		canMove = true;
		aiOpponentCanMove = true;
	}

	void FixedUpdate(){
		if(isAI){
			return;
		}

		rigidBody.velocity = new Vector2 (0, axisValue) * speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(!canMove){
			return;
		}

		switch(isAI){
		case false:
			axisValue = Input.GetAxisRaw (axis);
			break;

		case true:
			switch(transform.name){
			case "Left Racket":
				if(freezeLeftRacketMovement){
					return;
				}
				
				if(ball.position.x < 0){
					MoveRacketWithRespectToBallPosition();
				}
				break;

			case "Right Racket":
				if(ball.position.x > 0){
					MoveRacketWithRespectToBallPosition();
				}
				break;

			}

			break;

		}

	}

	private void MoveRacketWithRespectToBallPosition(){
		Vector2 thePos = transform.position;
		thePos.y = Mathf.Lerp (thePos.y, ball.position.y, speed * Time.deltaTime);
		thePos.y = Mathf.Clamp (thePos.y, -3, 3);
		transform.position = thePos;
	}

	public void MoveRacketToYaxisCenter(){
		iTween.MoveTo (gameObject, new Vector2(transform.position.x, 0), 0.8f);
		//transform.position = new Vector2(transform.position.x, 0);
		canMove = false;
		speed = 0;
		StartCoroutine(WaitFor(1.6f));
	}

	private IEnumerator WaitFor(float t){
		yield return new WaitForSeconds (t);
		canMove = true;

		if(SceneManagementScript.main.twoPlayerMode){
			speed = 8.4f;
		}
		else{
			speed = (transform.name.Equals("Left Racket"))? 12f : 8.4f;
		}
		
	}
		
}

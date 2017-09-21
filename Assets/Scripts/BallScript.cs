using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
	public static BallScript main;
	private Rigidbody2D rigidBody;
	public float speed;
	private float originalSpeed;
	public float speedIncreaseFactor;
	public float timeIntervalForSpeedIncrease;
	private float ballResetPosition;
	public Transform [] rackets;
	private float ballFadeoutTime = 0.3f;

	void Awake(){
		main = this;
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Start(){
		originalSpeed = speed;
		float x = Random.Range(0, 2) == 0? -7 : 7;
		transform.position = new Vector2(x, 0);
		x = (x < 0)? 1 : -1; 
		rigidBody.velocity = new Vector2(x, Random.Range(-0.3f, 0.3f)) * speed;
		StartCoroutine (IncreaseSpeedAfter(timeIntervalForSpeedIncrease));
	}
	
	void OnCollisionEnter2D(Collision2D col){
		switch(col.transform.tag){
		case "Racket":
			float x = 1, y = 1;
			Vector2 direction = Vector2.zero;

			switch(col.transform.name){
			case "Left Racket":
				x = 1;
				break;

			case "Right Racket":
				x = -1;
				break;
			}

			y = (transform.position.y - col.transform.position.y) / col.collider.bounds.size.y;
			direction = new Vector2 (x, y).normalized;
			rigidBody.velocity = direction * speed;
			
			break;

		case "Border":
			switch(col.transform.name){
			case "Border Left":
				ScoreScript.main.UpdateScore ("Right Racket");
				ballResetPosition = -7f;
				break;

			case "Border Right":
				ScoreScript.main.UpdateScore ("Left Racket");
				ballResetPosition = 7f;
				break;
			}

			ResetBallPosition(false);
			foreach (var racket in rackets)
			{
				racket.GetComponent<RacketControllerScript>().MoveRacketToYaxisCenter();
			}
			
			break;
		}
	
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.name.Equals("The Fuck You Are")){
			if(transform.position.x <  other.transform.position.x){
				//Doing this to allow some slack on the left racket ai so it can lose sometimes
				RacketControllerScript.freezeLeftRacketMovement = (Random.Range(0, Random.Range(2, 4)) == 1)? true : false;
				
				if(RacketControllerScript.freezeLeftRacketMovement){
					StartCoroutine(UnfreezeLeftRacketAfter(0.9f));
				}
			}
			else{
				RacketControllerScript.freezeLeftRacketMovement = false;
			}
		}
	}

	private IEnumerator UnfreezeLeftRacketAfter(float t){
		yield return new WaitForSeconds (t);
		RacketControllerScript.freezeLeftRacketMovement = false;
	}

	public void ResetBallPosition(bool chooseRandomly){
		if(chooseRandomly){
			ballResetPosition = Random.Range(0, 2) == 0? -7 : 7;
		}

		rigidBody.velocity = Vector2.zero;
		iTween.FadeTo(gameObject, 0, ballFadeoutTime);
		StartCoroutine (MoveBallAfter(1.5f));
	}

	private IEnumerator MoveBallAfter(float t){
		yield return new WaitForSeconds (ballFadeoutTime);
		transform.position = new Vector2(ballResetPosition, 0);
		iTween.FadeTo(gameObject, 1, ballFadeoutTime);
		yield return new WaitForSeconds (t - ballFadeoutTime);
		rigidBody.velocity = (transform.position.x > 0)? new Vector2(1, Random.Range(-0.3f, 0.3f)) * -speed : new Vector2(1, Random.Range(-0.3f, 0.3f)) * speed; 
	}

	private IEnumerator IncreaseSpeedAfter(float t){
		yield return new WaitForSeconds (t);
		if(speed < 16){
			speed += speedIncreaseFactor;
			StartCoroutine (IncreaseSpeedAfter (t));
		}

	}

	public void FadeOutAndResetBallPos(){
		rigidBody.velocity = Vector2.zero;
		iTween.FadeTo(gameObject, 0, ballFadeoutTime);
		ballResetPosition =  Random.Range(0, 2) == 0? 7 : -7;
		StartCoroutine(MoveBallAfter(1.7f));

	}

	public void ResetSpeed(){
		if(speed >= 16){
			speed = originalSpeed;
			StartCoroutine (IncreaseSpeedAfter(timeIntervalForSpeedIncrease));
		}

		speed = originalSpeed;

	}

}

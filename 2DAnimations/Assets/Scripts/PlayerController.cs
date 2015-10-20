using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 1;

	bool _isGrounded = true;

	Animator animator;

	bool _isPlaying_crouch = false;
	bool _isPlaying_hadooken = false;


	const int STATE_IDLE = 0;
	const int STATE_WALK = 1;
	const int STATE_CROUCH = 2;
	const int STATE_JUMP = 3;
	const int STATE_HADOOKEN = 4;
	
	string _currentDirection = "left";
	int _currentAnimationState = STATE_IDLE;
	
	void Start() {

		animator = this.GetComponent<Animator>();
	}

	void FixedUpdate() {

		if (Input.GetKeyDown (KeyCode.Space))
			changeState(STATE_HADOOKEN);	

		else if (Input.GetKey(KeyCode.UpArrow) && !_isPlaying_hadooken && !_isPlaying_crouch) {

			if (_isGrounded) {

				_isGrounded = false;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));
				changeState(STATE_JUMP);
			}

		} else if (Input.GetKey(KeyCode.DownArrow))
			changeState(STATE_CROUCH);	

		else if (Input.GetKey(KeyCode.RightArrow) && !_isPlaying_hadooken) {

			changeDirection("right");
			transform.Translate(Vector3.left * walkSpeed * Time.fixedDeltaTime);

			if (_isGrounded)
				changeState(STATE_WALK);
			
		} else if (Input.GetKey(KeyCode.LeftArrow) && !_isPlaying_hadooken) {

			changeDirection("left");
			transform.Translate(Vector3.left * walkSpeed * Time.fixedDeltaTime);

			if (_isGrounded)
				changeState(STATE_WALK);
			
		} else if (_isGrounded)
			changeState(STATE_IDLE);

		_isPlaying_crouch = animator.GetCurrentAnimatorStateInfo(0).IsName("ken_crouch");
		_isPlaying_hadooken = animator.GetCurrentAnimatorStateInfo(0).IsName("ken_hadooken");
	}

	void changeState(int state) {

		if (_currentAnimationState == state)
			return;

		animator.SetInteger("state", state);

		_currentAnimationState = state;
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.name == "Floor") {
			_isGrounded = true;
			changeState(STATE_IDLE);
		}
	}
	
	void changeDirection(string direction) {

		if (_currentDirection != direction) {

			if (direction == "right")
				transform.Rotate (0, 180, 0);

			else if (direction == "left")
				transform.Rotate (0, -180, 0);

			_currentDirection = direction;
		}
		
	}

}

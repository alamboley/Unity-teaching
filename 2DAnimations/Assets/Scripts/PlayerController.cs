using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float walkSpeed;

	Animator animator;
	Rigidbody2D rigidBody;

	bool isGrounded = true;

	bool goingRight = false;
	public bool GoingRight {

		get { return goingRight; }

		set {

			if (goingRight == value)
				return;

			goingRight = value;
			Vector3 tmpScale = transform.localScale;
			tmpScale.x = goingRight ? -1 : 1;
			transform.localScale = tmpScale;
		}
	}

	bool isWalking = false;
	public bool IsWalking {

		get { return isWalking; }

		set {

			if (isWalking == value)
				return;

			animator.SetBool ("isWalking", isWalking = true);
		}
	}

	bool isCrouching = false;
	public bool IsCrouching {

		get { return isCrouching; }

		set {

			if (isCrouching == value)
				return;

			animator.SetBool("isCrouching", isCrouching = value);
		}
	}

	bool isJumping = false;
	public bool IsJumping {

		get { return isJumping; }

		set {

			if (isJumping == value)
				return;

			animator.SetBool("isJumping", isJumping = value);

			if (isJumping && isGrounded) {

				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));

				isGrounded = false;
			}
		}
	}

	bool isHadookening = false;
	public bool IsHadookening {

		get { return isHadookening; }

		set {

			if (isHadookening == value)
				return;

			animator.SetBool("isHadookening", isHadookening = value);
		}
	}

	string direction = "";
	public string Direction {

		get { return direction; }

		set {

			if (direction == value)
				return;

			direction = value;

			bool isMoving = direction != "";

			if (isMoving) 
				GoingRight = direction == "right";

			animator.SetBool("isWalking", isMoving);
		}
	}
	

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update() {

		IsHadookening = Input.GetKey (KeyCode.Space);

		IsJumping = Input.GetKey (KeyCode.UpArrow);

		IsCrouching = Input.GetKey (KeyCode.DownArrow);

		if (Input.GetKey (KeyCode.LeftArrow))
			Direction = "left";
		else if (Input.GetKey (KeyCode.RightArrow))
			Direction = "right";
		else
			Direction = "";
	}

	void FixedUpdate() {

		Vector2 tmpVelocity = rigidBody.velocity;

		if (Direction != "" && !IsCrouching && !IsHadookening)
			tmpVelocity.x = walkSpeed * (GoingRight ? 1 : -1) * Time.fixedDeltaTime;

		rigidBody.velocity = tmpVelocity;
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.transform.name == "background") {

			isGrounded = true;
			IsJumping = false;
		}
	}
}

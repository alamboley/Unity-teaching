using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float walkSpeed;

	Animator animator;
	Rigidbody2D rigidBody;

	bool isGrounded = true;

	bool isWalking = false;
	public bool IsWalking {

		get { return isWalking; }

		set {

			if (isWalking == value)
				return;

			animator.SetBool ("isWalking", isWalking = value);
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

				rigidBody.AddForce(new Vector2(0, 250));
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

	int direction = 0;
	public int Direction {

		get { return direction; }

		set {

			if (direction == value)
				return;

			direction = value;
			
			GetComponent<SpriteRenderer>().flipX = direction == 1;
		}
	}
	

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update() {

		IsHadookening = Input.GetKey(KeyCode.Space);

		IsJumping = Input.GetKey(KeyCode.UpArrow);

		IsCrouching = Input.GetKey(KeyCode.DownArrow);

		if (Input.GetKey (KeyCode.LeftArrow))
			Direction = -1;
		else if (Input.GetKey (KeyCode.RightArrow))
			Direction = 1;
		else
			Direction = 0;
	}

	void FixedUpdate() {

		Vector2 tmpVelocity = rigidBody.velocity;

		if (Direction != 0 && !IsCrouching && !IsHadookening) {
			
			tmpVelocity.x = walkSpeed * Direction * Time.fixedDeltaTime;
			
			IsWalking = true;
			
		} else
			IsWalking = false;

		rigidBody.velocity = tmpVelocity;
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.transform.name == "background") {

			isGrounded = true;
			IsJumping = false;
		}
	}
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
		[Header("Movement")]
		[SerializeField]
		private float moveSpeed;

		[SerializeField]
		private float groundDrag;

		[SerializeField]
		private float airDrag;

		[SerializeField]
		private float jumpForce;

		[SerializeField]
		private float jumpCooldown;

		[SerializeField]
		private float airMultiplier;

		private bool canJump;

		[Header("Ground stuff")]
		[SerializeField]
		private LayerMask groundLayer;

		[SerializeField]
		private float playerHeight;

		private bool isGrounded;

		[Header("Orientation")]
		[SerializeField]
		private Transform orientation;

		private Vector2 keyboardInput;
		private Vector3 moveDirection;

		private Rigidbody rb;

		// Start is called before the first frame update
		void Start()
		{
				rb = GetComponent<Rigidbody>();
				rb.freezeRotation = true;
				ResetJump();
		}

		// Update is called once per frame
		void Update()
		{
				// Check for ground
				isGrounded = Physics.Raycast(
					transform.position,
					Vector3.down,
					playerHeight * 0.5f + 0.2f,
					groundLayer
				);



				// Handle input
				keyboardInput.x = Input.GetAxisRaw("Horizontal");
				keyboardInput.y = Input.GetAxisRaw("Vertical");

				if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
				{
						canJump = false;
						Jump();
						Invoke(nameof(ResetJump), jumpCooldown); // Louis veut pas qu'on utilise Invoke
				}

				// Handle velocity
				Vector3 currentVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

				if (currentVelocity.magnitude > moveSpeed)
				{
						Vector3 limitedVelocity = currentVelocity.normalized * moveSpeed;
						rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
				}

				// Apply drag
				if (isGrounded)
				{
						rb.drag = groundDrag;
				}
				else
				{
						rb.drag = airDrag;
				}
		}

		private void FixedUpdate()
		{
				// Move player
				moveDirection = orientation.forward * keyboardInput.y + orientation.right * keyboardInput.x;
				if (isGrounded)
				{
						rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
				}
				else
				{
						rb.AddForce(
							moveDirection.normalized * moveSpeed * 10f * airMultiplier,
							ForceMode.Force
						);
				}
		}

		private void Jump()
		{
				// Reset y velocity, makes jump height constant
				rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

				rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
		}

		private void ResetJump()
		{
				canJump = true;
		}
}

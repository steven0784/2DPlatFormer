using UnityEngine;

public class EnemyBehaviour : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.
	public int hp = 10;
	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	
	
	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;	
	Transform rightWallCheck;
	Transform leftWallCheck;	// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
	bool grounded = false;		
	bool seenPlayer = false;
	bool rightWallChecking = false;
	bool leftWallChecking = false;// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.
	Transform playerGraphics;
	Transform rayPoint;
	Transform rayPoint2;
	float x = 1;
	float y = 1;
	int counter = 0;
	int counter2 = 10;
	float cooldown = 0;
	public Rigidbody2D bulletPrefab;
	public LayerMask whatToHit;
	RaycastHit2D hit3;

	void Awake()
	{
		// Setting up references.
		rayPoint = transform.FindChild ("RayTest");
		rayPoint2 = transform.FindChild ("RayTest2");
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		rightWallCheck = transform.Find("rightWallCheck");
		leftWallCheck = transform.Find("leftWallCheck");
		anim = GetComponent<Animator>();
		playerGraphics = transform.FindChild ("Graphics");
		if (playerGraphics == null) {
			Debug.LogError ("No Graphics for player");
		}
	}
	void Update()
	{
		Vector2 rayPointPos = new Vector2 (rayPoint.position.x,rayPoint.position.y);
		Vector2 rayPointPos2 = new Vector2 (rayPoint.position.x,rayPoint.position.y - 5);
		Vector2 rayPoint2Pos = new Vector2 (rayPoint2.position.x,rayPoint2.position.y);
		Vector2 rayPoint2Pos2 = new Vector2 (rayPoint2.position.x,rayPoint2.position.y - 5);
		RaycastHit2D hit = Physics2D.Raycast (rayPointPos,-Vector3.up,5);
		RaycastHit2D hit2 = Physics2D.Raycast (rayPoint2Pos,-Vector3.up,5);

		if (counter > 0) {
				counter--;
		}
		if(seenPlayer){
			x = 0;
		}
		else if(!seenPlayer && x == 0 && counter == 0){
			if(facingRight){
				x = 1;
			}
			else{
				x = -1;
			}
			counter = 10;
		}
		if (hit2.collider == null && !facingRight && x != 0) {
			x = 1;
		}
		else if (hit.collider == null && facingRight && x != 0) {
			x = -1;
		}
		//Debug.Log (hit.collider.tag);
		Move (x, false, false);
		Vector2 newRayPointPos;
		Vector2 newRayPointPos2;
		if (facingRight) {
				newRayPointPos = new Vector2 (rayPoint.position.x, rayPoint.position.y);
				newRayPointPos2 = new Vector2 (rayPoint.position.x + 5, rayPoint.position.y);
			} 
		else {
			newRayPointPos = new Vector2 (rayPoint2.position.x, rayPoint2.position.y);
			newRayPointPos2 = new Vector2 (rayPoint2.position.x - 5, rayPoint2.position.y);
		}
		if(facingRight){
			hit3 = Physics2D.Raycast (newRayPointPos,transform.right,10,whatToHit);
		}
		else{
			hit3 = Physics2D.Raycast (newRayPointPos,-transform.right,10,whatToHit);
		}
		if (hit3.collider != null) {
			seenPlayer = true;
			if (cooldown == 0){
				Shoot();
				counter2 = 10;
				cooldown = 10;
			}
			else{
				cooldown --;
			}
		}
		else {
			if (counter2 < 0){
				seenPlayer = false;
			}

		}
		counter2 --;
	}
	void OnTriggerEnter2D(Collider2D Colls)
	{
		if (Colls.tag == "BulletTrail") {
			Destroy(Colls.gameObject);
			hit (1);
		}
	}
	void hit (int damage)
	{
		audio.Play ();
		hp -= damage;
		if (hp <= 0) {
			Destroy (this.gameObject);
		}
	}
	void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		rightWallChecking = Physics2D.OverlapCircle(rightWallCheck.position, groundedRadius, whatIsGround);
		leftWallChecking = Physics2D.OverlapCircle(leftWallCheck.position, groundedRadius, whatIsGround);
		if (facingRight && rightWallChecking) {
						x = x * -1;
		} else if (!facingRight && leftWallChecking) {
						x = x * -1;
		}
		anim.SetBool("Ground", true);
		
		// Set the vertical animation
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

	}
	
	
	public void Move(float move, bool crouch, bool jump)
	{
		
		
		// If crouching, check to see if the character can stand up
		if(!crouch && anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}
		
		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);
		
		//only control the player if grounded or airControl is turned on
		if(grounded || airControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move * crouchSpeed : move);
			
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));
			
			// Move the character
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				Flip();
		}
		
		// If the player should jump...
		if (grounded && jump) {
			// Add a vertical force to the player.
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
		}
	}
	
	void Shoot(){
		Rigidbody2D bPrefab = Instantiate(bulletPrefab, transform.position, transform.rotation) as Rigidbody2D;
		if (facingRight == true)
		{
			bPrefab.rigidbody2D.AddForce(Vector3.right * 1000);
		}
		else
		{
			bPrefab.rigidbody2D.AddForce(Vector3.left * 1000);
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = playerGraphics.localScale;
		theScale.x *= -1;
		playerGraphics.localScale = theScale;
	}
}
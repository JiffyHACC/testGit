using UnityEngine;
using System.Collections;

//Script will always require PlayerPhysics.cs
[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : Entity {

	
	
	//Player handling variables
	public float gravity = 20;
	public float jumpHeight = 12;
	
	public float walkSpeed = 8;
	public float runSpeed = 12;
	public float acceleration = 30;
	
	
	private float animationSpeed;
	private float currentSpeed;
	private float targetSpeed;
	
	
	private bool jumping;
	
	
	//Don't need vector3, this is in 2D
	private Vector2 amountToMove;
	
	//Reference to the PlayerPhysics.cs script
	private PlayerPhysics playerPhysics;
	
	//Reference to animator
	private Animator animator;
	
	//Reference to GameManager.cs script
	private GameManager manager;
	
	
	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animator = GetComponent<Animator>();
		manager = Camera.main.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
				
		if (playerPhysics.movementStopped){
			targetSpeed = 0;
			currentSpeed = 0;
		}
		
		
		//Jumping
		if (playerPhysics.grounded){
			
			amountToMove.y = 0;
			
			if(jumping){
				jumping = false;
				animator.SetBool("Jumping", false);
			}
			
			
			if(Input.GetButtonDown("Jump")){
				amountToMove.y = jumpHeight;
				jumping = true;
				animator.SetBool("Jumping", true);
			}
			
		}
		animationSpeed = IncrementTowards(animationSpeed, Mathf.Abs(targetSpeed), acceleration);
		animator.SetFloat("Speed", animationSpeed);
		
		
		//Input for movement
		float speed = (Input.GetButtonDown("Run"))? runSpeed: walkSpeed;
		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
		
		
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);
		
		
		//Face direction
		float moveDir = Input.GetAxisRaw("Horizontal"); 
		
		if(moveDir != 0){
			
			transform.eulerAngles = (moveDir > 0)?  Vector3.zero : Vector3.up * 180;
			
		} 
		
	}
	
	
	void OnTriggerEnter(Collider c){
		
		if (c.tag == "Checkpoint"){
			manager.SetCheckpoint(c.transform.position);
			
		}
		
		if (c.tag == "Finish"){
			manager.EndLevel();
			
		}
	}
	
	//Increase n towards target by speed
	private float IncrementTowards(float n, float target, float acc){
		if (n == target){
			
			return n;
			
		}else{
			
			float dir = Mathf.Sign(target - n); // Must n be increased or decreased to get closer to the target
			n += acc * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target - n))? n : target; //If n has now passed target then return target, otherwise return n
			
		}
			
	}
	
}

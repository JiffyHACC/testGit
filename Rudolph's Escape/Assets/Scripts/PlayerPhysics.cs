using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask collisionMask;
	
	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;
	
	private Vector3 originalSize;
	private Vector3 originalCenter;
	private float colliderScale;
	
	
	private int collisionDivisionsX = 3;
	private int collisionDivisionsY = 10;
	
	private float skin = .005f;
	
	//On the ground
	[HideInInspector]
	public bool grounded;
	
	//Stop movement
	[HideInInspector]
	public bool movementStopped;
	
	private Transform platform;
	private Vector3 platformPositionOld;
	private Vector3 deltaPlatformPos;
	
	
	Ray ray;
	RaycastHit hit;
	
	
	void Start(){
		collider = GetComponent<BoxCollider>();
		colliderScale = transform.localScale.x;
		
		
		originalSize = collider.size;
		originalCenter = collider.center;
		SetCollider(originalSize, originalCenter);
	}
	

	public void Move(Vector2 moveAmount){
		
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		
		Vector2 position = transform.position;
		
		if (platform){
			
			deltaPlatformPos = platform.position - platformPositionOld;
			
		} else {
			
			deltaPlatformPos = Vector3.zero;
		}
		
		
		#region Vertical collisions
		//Check for collisions above and below
		grounded = false;
		
		for (int i = 0; i < collisionDivisionsX; i++){
			float dir = Mathf.Sign(deltaY);
			float x = (position.x + center.x - size.x/2) + size.x/(collisionDivisionsX - 1) * i; //Left, centre and rightmost point of collider
			float y = position.y + center.y + size.y/2 * dir; // Bottom of collider
			
			//Raycasting
			ray = new Ray(new Vector2(x,y), new Vector2(0, dir));
			//Debug.DrawRay(ray.origin, ray.direction);
			
			
			if(Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask)){
				
				platform = hit.transform;
				platformPositionOld = platform.position;
				
				//Get distance between player and ground
				float distance = Vector3.Distance(ray.origin,hit.point);
				
				//Stop player's downwards movement after coming within skin width of a collider
				if(distance > skin){
					
					deltaY = distance * dir - skin * dir;
					
				} else {
					
					deltaY = 0;
				}
				grounded = true;
				break;
				
				
			} else {
				
				platform = null;
				
			}
			
			
		}
		#endregion
		
		#region Sideways sollisions
		//Check for collisions left and right
		movementStopped = false;
		
		for (int i = 0; i < collisionDivisionsY; i++){
			float dir = Mathf.Sign(deltaX);
			float x = position.x + center.x + size.x/2 * dir; 
			float y = (position.y + center.y - size.y/2) + size.y/(collisionDivisionsY - 1) * i; 
			
			//Raycasting
			ray = new Ray(new Vector2(x,y), new Vector2(dir, 0));
			Debug.DrawRay(ray.origin, ray.direction);
			
			
			if(Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin, collisionMask)){
				
				//Get distance between player and ground
				float distance = Vector3.Distance(ray.origin,hit.point);
				
				
				//Stop player's horizontal movement after coming within skin width of a collider
				if(distance > skin){
					
					deltaX = distance * dir - skin * dir;
					
				} else {
					
					deltaX = 0;
				}
				movementStopped = true;
				break;
			}
			
			
		}
		#endregion
		
		#region Diagonal collisions
		//Collisions diagonally
		if(!grounded && !movementStopped){
			
			Vector3 playerDir = new Vector3(deltaX, deltaY);
			Vector3 origin = new Vector3( ( position.x + center.x + size.x/2 * Mathf.Sign(deltaX) ), ( position.y + center.y + size.y/2 * Mathf.Sign(deltaY) ) );
			
			
			Debug.DrawRay(origin,playerDir.normalized);
			ray = new Ray(origin, playerDir.normalized);
			
			
			if(Physics.Raycast(ray, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask ) ){
				grounded = true;
				deltaY = 0;
				
			}
							
		}
		#endregion
		
		Vector2 finalTransform = new Vector2(deltaX + deltaPlatformPos.x, deltaY);
		
		transform.Translate(finalTransform, Space.World);
	}
	public void SetCollider(Vector3 size1, Vector3 center1){
		collider.size = size;
		collider.center = center;
		
		size = size1 * colliderScale;
		center = center1 * colliderScale;
	}
}

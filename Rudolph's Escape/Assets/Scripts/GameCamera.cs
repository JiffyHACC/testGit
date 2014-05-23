using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	private Transform target;
	private float trackSpeed = 10;
		
	//Set the camera's target
	public void SetTarget (Transform t) {
		target = t;
		transform.position = new Vector3(t.position.x, t.position.y, transform.position.z);
	}
	
	void LateUpdate(){
		
		if(target){
			
			float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTowards(transform.position.y, target.position.y, trackSpeed);
			transform.position = new Vector3(x,y,transform.position.z);
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

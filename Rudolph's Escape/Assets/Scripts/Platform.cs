using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	public float speed = 2;
	
	public Vector3 position1;
	public Vector3 position2;
	
	public bool turn;
	
	
	// Update is called once per frame
	void Update () {
		
		Direction();
				
		if(!turn){
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			
		}
		
		else if (turn){
			
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}
		
	}
	
	
	
	void Direction(){
		
		if(transform.position.x <= position1.x){
			turn = false;
			
		}
		
		else if (transform.position.x >= (position1.x + 10)){
			turn = true;
		}
		
		
	}
	
	
	/*void OnTriggerEnter(Collider c){
		
		if(c.tag == "Player"){
			
			direction = true;
		}
		
	}*/
}

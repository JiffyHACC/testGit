using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

	public bool direction = false;
	public float speeds = 50;
	public float timeLeft = 3;
	
	
	
	
	
	
	// Update is called once per frame
	void Update () {
		if(direction){
			timeLeft -= Time.deltaTime;
			
			
			if (timeLeft <= 0){
				transform.Translate (Vector3.down * speeds * Time.deltaTime);
				
			}
			
			if (timeLeft <= -2) {
				Die();
			}
			
			
		}
	}	
	
	void OnTriggerEnter(Collider c){
		
		if(c.tag == "Player"){
			
			direction = true;
		}
		
	}
	
	
	
	
	public void Die(){
		Destroy(this.gameObject);
	}
}


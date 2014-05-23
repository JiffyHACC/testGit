using UnityEngine;
using System.Collections;

public class GroundDeath : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		
		if(c.tag == "Player"){
			
			//Debug.Log("DIE!");
			c.GetComponent<Entity>().TakeDamage(10);
		}
		
	}
}

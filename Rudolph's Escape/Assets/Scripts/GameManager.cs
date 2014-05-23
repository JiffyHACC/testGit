using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	//Current player and camera
	public GameObject player;
	private GameCamera cam;
	
	private GameObject currentPlayer;
	
	
	
	
	//Falling platforms
	public GameObject platform;
	private GameObject currentPlatform;
	public Vector3 spawnPoint;
	public float numbPlatforms;
		
	
	private Vector3 checkpoint;
	
	public int levelCount = 3;
	public static int currentLevel = 1;
	
		
	// Use this for initialization
	void Start () {
		
		//Platform
		if(numbPlatforms != 0){
			for (int i = 0; i < numbPlatforms; i++){
				SpawnPlatform(spawnPoint);
				spawnPoint.x += 5;
			}
		}
		
		
		cam = GetComponent<GameCamera>();
		
		
		if (GameObject.FindGameObjectWithTag("Spawn")){
			checkpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
			
		}
	
		SpawnPlayer(checkpoint);
		
	}
	
	// Spawn the player on the screen
	private void SpawnPlayer (Vector3 spawnPos) {
		currentPlayer = Instantiate(player, spawnPos, Quaternion.identity) as GameObject;
		cam.SetTarget(currentPlayer.transform);
		
	}
	
	
	//Spawn platform
	private void SpawnPlatform(Vector3 sp){
		
		currentPlatform = Instantiate(platform, sp, Quaternion.identity) as GameObject;
	}
	
		
	
	private void Update(){
		
		if (!currentPlatform && numbPlatforms != 0){
			for(int i = 0; i < numbPlatforms; i++){
				spawnPoint.x -= 5;
			}
			for(int i = 0; i < numbPlatforms; i++){
				SpawnPlatform(spawnPoint);
				spawnPoint.x += 5;
			}
			
		}
		
		
		if (!currentPlayer){
			
			if(Input.GetButtonDown("Respawn")){
				SpawnPlayer(checkpoint);
			}
			
		}
		
		
	}
	
	public void SetCheckpoint(Vector3 cp){
		checkpoint = cp;
		
	}
	
	public void EndLevel(){
		if(currentLevel < levelCount){
			currentLevel++;
			Application.LoadLevel("Level " + currentLevel);
			
		} else {
			
			Debug.Log("Game Over");
		
		}
		
	}
}

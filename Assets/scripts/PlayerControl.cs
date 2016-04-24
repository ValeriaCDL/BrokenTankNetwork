using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour {


	public float firerate=1;
	float nextfire=1;

	public float rotationSpeed=100;

	public GameObject bala;
	public Transform barril;
	public Material material;

	void Update(){

		if (!isLocalPlayer) {return;}

		float hkeys = -(Input.GetAxis ("Horizontal") * Time.deltaTime * rotationSpeed);
		float hacceleration = -(Input.acceleration.x * Time.deltaTime * rotationSpeed); 

		transform.Rotate(0, 0, hkeys);  
		transform.Rotate(0, 0,hacceleration);

		if ( (Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space)) && Time.time > nextfire) {
			nextfire = Time.time + firerate;
			CmdFire (); //este cliente le dice al servidor q lo haga
		} 


	}

	[Command]
	void CmdFire(){

		GameObject bullet = Instantiate (bala, barril.position, barril.rotation) as GameObject;

		//Le dice al servidor q lo instance en los clientes
		NetworkServer.Spawn(bullet);

	}

	public override void OnStartLocalPlayer(){
		base.OnStartLocalPlayer ();

		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer> ()) {
			mr.material = material;
		}
			

	}
		
}

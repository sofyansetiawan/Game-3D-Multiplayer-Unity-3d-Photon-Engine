using UnityEngine;
using System.Collections;

public class csParticleMake : MonoBehaviour {

	public Transform[] Particles;			//Particle that you want to make
	public int ParticleMakeNumber =1;		//Maked Particle Count
	public int Radious = 1;

	float StandardAngle;					//Standard Angle via ParticleMakeNumber		
	float MakeAngle;						//Particle Make Angle

	void Awake() 
	{
		StandardAngle = (360 / ParticleMakeNumber) * (Mathf.PI / 180); //convert angle to circular measure
		MakeAngle = StandardAngle; //Save angle to StnadardAngle first time.

		int ParticleOrder = 0; //Order
		for (int i = 0; i< ParticleMakeNumber; i++) //Make Particle Object via ParticleMakeNumber
		{

			//Set Particle via Particles count.
			//if ParticleMakeNumber is 4, Particles count is 2,
			//make particles object like 1,2,1,2
			//if ParticleMakeNumber is 5, Particles count is 3,
			//make particles object like 1,2,3,1,2
			//------------------------------------------
			Transform _Particles;

			if(Particles.Length > 1)
			{
				if(ParticleOrder >= Particles.Length) 										 
					ParticleOrder = 0;

				_Particles = Particles[ParticleOrder];
				ParticleOrder += 1;
			}
			else
				_Particles = Particles[0];
			//------------------------------------------


			Transform Obj = Instantiate(_Particles,this.transform.position,this.transform.rotation) as Transform;  // Make Object
			Obj.transform.parent = this.transform; //Set particle's parent to this root.

			Obj.transform.position = new Vector3(Obj.position.x+Mathf.Cos(MakeAngle)*Radious, //Make particle via trigonometric function on X,Z coordinate
			                                     Obj.position.y,
			                                     Obj.position.z+Mathf.Sin(MakeAngle)*Radious);
			MakeAngle += StandardAngle; //add standardAngle to MakeAngle.
		}
	
	}

	void ColorSubmit()
	{

	}
}

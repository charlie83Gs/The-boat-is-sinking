using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle {

    public float Height = 0;
    public float Speed = 0;
    public float Position, Velocity;

    public WaterParticle(float TargetHeight) {
        Height = TargetHeight;
    }

    public void simulate(float TargetHeight, float dampening) {
        const float k = 0.025f; // adjust this value to your liking
        float x = Height - TargetHeight;
        float acceleration = -k * x -dampening*Speed;

        //Position += Speed;
        Height += Speed;
        Speed += acceleration;
        //Debug.Log(Position);
        
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController: MonoBehaviour
{ 

    public Queue<CardAnimation> animationQueue = new Queue<CardAnimation>();
    public float animationRate = 2f; // animation per second
    private float timeSinceLastAnimation = 0;

    void Update() {
        timeSinceLastAnimation += Time.deltaTime;
        if (timeSinceLastAnimation > (1 / animationRate)) {
            playNext();
        }
    }
    
    public void add(CardAnimation animation) {
        animationQueue.Enqueue(animation);
    }

    public void playNext() {
        if (animationQueue.Count > 0) {
            Debug.Log("Playing animation");
            animationQueue.Dequeue().animate();
        }
    }
}

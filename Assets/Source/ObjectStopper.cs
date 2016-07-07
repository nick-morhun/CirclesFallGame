using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ObjectStopper : MonoBehaviour
{

    public event UnityAction<ObjectStoppedEventArgs> ObjectStopped = delegate { };

    private void OnTriggerEnter2D(Collider2D other)
    {
        var fallingObject = other.GetComponent<FallingObject>();
        if (fallingObject)
        {
            fallingObject.enabled = false;
            ObjectStopped(new ObjectStoppedEventArgs { FallingObject = fallingObject });
        }
    }
}

public class ObjectStoppedEventArgs
{
    public FallingObject FallingObject { get; set; }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class InputManager : MonoBehaviour
{
    public event UnityAction<TouchEventArgs> Touch = delegate { };

    private void Update()
    {
        Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown(Configuration.Instance.ActionAxisName))
        {
            //Debug.Log("Touch started at world coordinates " + pointerWorldPosition);
            Touch(new TouchEventArgs { PointerWorldPosition = pointerWorldPosition });
            return;
        }
    }
}

public class TouchEventArgs
{
    public Vector3 PointerWorldPosition { get; set; }
}

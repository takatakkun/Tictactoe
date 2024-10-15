using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewBehaviourScript : MonoBehaviour
{
    public float RotateSpeed = 0.1f;
    public float UpDownSpeed = 0.01f;

    void Update()
    {
        int touchCount = Input.touches
             .Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
        if (touchCount == 1)
        {
            //Debug.Log("a");
            Touch t = Input.touches.First();
            switch (t.phase)
            {
                case TouchPhase.Moved:

                    //à⁄ìÆó 
                    float xDelta = t.deltaPosition.x * RotateSpeed;
                    float yDelta = t.deltaPosition.y * UpDownSpeed;

                    //ç∂âEâÒì]
                    this.transform.Rotate(0, xDelta, 0, Space.World);
                    //è„â∫à⁄ìÆ
                    this.transform.position += new Vector3(0, -yDelta, 0);

                    break;
            }
        }
    }
}
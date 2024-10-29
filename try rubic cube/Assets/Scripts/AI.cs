using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] GameObject[] Quad = new GameObject[54] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,  null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
    List<GameObject> XPlist = new List<GameObject>();

    public void AIPlayer()
    {

    }

    public void Judgement()
    {
        for (int i = 0; i < Quad.Length; i++)
        {
            Vector3 tmp = Quad[i].transform.position;
            float x = tmp.x;
            float y = tmp.y;
            float z = tmp.z;
            if( x > 1.3f && x < 1.7f)
            {
                XPlist.Add(Quad[i]);
            }
            /*else
            {
                Debug.Log("ŽG‹›‰³");
            }*/
        }
    }
}

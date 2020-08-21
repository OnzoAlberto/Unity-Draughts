using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public static GameObject objSelected;
    public GameObject lightPrefab,kingPrefab;
    public static GameObject cursor,king;
    private static GameObject Destination,Sx = null,Dx = null;
    public static bool eating = false;

    public static int turn = 8; // 8 = bianco //9 = nero

    private void Start()
    {
        cursor = lightPrefab;
        king = kingPrefab;
    }

    public static void resetLight()
    {
        var reset = GameObject.FindGameObjectsWithTag("light");
            foreach (GameObject light in reset)
                Destroy(light);
    }

    public static bool cellOccupated(GameObject obj)
    {
        bool occupied = false;

        var forward = 0f;
        if (objSelected.layer.Equals(8)) //istanzia luce angolo dx e sx --- bianchi
        {
            if (!(obj.transform.position.x + 1 > 7))
            {
                forward = obj.transform.position.x + 1;
                if (!(obj.transform.position.z + 1 > 7))
                    Sx = Instantiate(cursor, new Vector3(obj.transform.position.x + 1, 0, obj.transform.position.z + 1), Quaternion.Euler(90, 0, 0));
                if (!(obj.transform.position.z - 1 < 0))
                    Dx = Instantiate(cursor, new Vector3(obj.transform.position.x + 1, 0, obj.transform.position.z - 1), Quaternion.Euler(90, 0, 0));

            }
            else
                return false;
            
        }
        else //istanzia luce angolo dx e sx---- neri
        {
            if (!(obj.transform.position.x - 1 < 0))
            {
                forward = obj.transform.position.x - 1;

                if (!(obj.transform.position.z + 1 > 7))
                    Sx = Instantiate(cursor, new Vector3(obj.transform.position.x - 1, 0, obj.transform.position.z + 1), Quaternion.Euler(90, 0, 0));
                if (!(obj.transform.position.z - 1 < 0))
                    Dx = Instantiate(cursor, new Vector3(obj.transform.position.x - 1, 0, obj.transform.position.z - 1), Quaternion.Euler(90, 0, 0));
            }
            else
                return false;
            
        }

       

        var list = GameObject.FindGameObjectsWithTag("Man");
        
        foreach (GameObject c in list)
        {
            if (Sx == null)
                return false;

            if ( c.transform.position.x.Equals(Sx.transform.position.x) && c.transform.position.z.Equals(Sx.transform.position.z))
            {// controllo se angolo sx c'è nemico o amico

                if (c.layer.Equals(objSelected.layer)) // è amico
                {
                    Sx.GetComponentInChildren<Light>().color = Color.blue;
                    Sx.GetComponent<BoxCollider>().enabled = false;

                }
                else // nemico
                {
                    Sx.GetComponentInChildren<Light>().color = Color.red;
                    Sx.GetComponent<BoxCollider>().enabled = false;
                    if (!c.name.Contains("king"))
                    {
                        if (objSelected.layer == 8)
                            canJump(forward + 1, Sx.transform.position.z + 1);
                        else
                            canJump(forward - 1, Sx.transform.position.z + 1);
                    }
                }

                occupied = true;
            }

            if (Dx == null)
                return false;
            if (c.transform.position.x.Equals(Dx.transform.position.x) && c.transform.position.z.Equals(Dx.transform.position.z))
            {
                if (c.layer.Equals(obj.layer)) // amico 
                {
                    Dx.GetComponentInChildren<Light>().color = Color.blue;
                    Dx.GetComponent<BoxCollider>().enabled = false;

                }
                else  //  nemico
                {
                    Dx.GetComponentInChildren<Light>().color = Color.red;
                    Dx.GetComponent<BoxCollider>().enabled = false;
                    if (!c.name.Contains("king"))
                    {
                        if (objSelected.layer == 8)
                            canJump(forward + 1, Dx.transform.position.z - 1);
                        else
                            canJump(forward - 1, Dx.transform.position.z - 1);
                    }
                }

                occupied = true;
            }
        }
        return occupied;
    }

    public static bool canJump(float x, float z)
    {
        if (x > 7 || x < 0 || z > 7 || z < 0)
            return false;

        var list = GameObject.FindGameObjectsWithTag("Man");

        foreach (GameObject c in list)
        {
            
            if (c.transform.position.x.Equals(x) && c.transform.position.z.Equals(z))
            {
              
                return false;
            }
        }

        Destination = Instantiate(cursor, new Vector3(x, 0, z), Quaternion.Euler(90, 0, 0));
        Destination.GetComponentInChildren<Light>().color = Color.yellow;
        return true;
    }
/*
    public static bool Double(GameObject obj)
    {
        resetLight();
        cellOccupated(obj);
        return canJump(obj.transform.position.x, obj.transform.position.z);
    }
*/
    public static void BecomeKing(GameObject man)
    {
        Instantiate(king, man.transform.position, Quaternion.Euler(-90, -90, 0));
        Destroy(man);
    }


    public static void findAndDestroy(float x,float z)
    {
        var chess = GameObject.FindGameObjectsWithTag("Man");

        foreach (GameObject c in chess)
        {
          if (c.transform.position.x.Equals(x) && c.transform.position.z.Equals(z))
             Destroy(c);  
        }
    }

}

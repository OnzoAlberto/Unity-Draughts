using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingMovement : MonoBehaviour
{
    private GameObject Destination, A_Sx = null, A_Dx = null,P_Sx = null, P_Dx = null;
    float A_forward, P_forward;

    private void OnMouseDown()
    {
        manager.resetLight();
        manager.objSelected = this.gameObject;

        if (manager.turn.Equals(this.gameObject.layer))
        {
            cellOccupated(this.gameObject);
        }



    }


    public  bool cellOccupated(GameObject obj)
    {
        bool occupied = false;

            if (!(obj.transform.position.x + 1 > 7)) // angoli anteriori
            {
            A_forward = obj.transform.position.x + 1;
                if (!(obj.transform.position.z + 1 > 7))
                    A_Sx = Instantiate(manager.cursor, new Vector3(obj.transform.position.x + 1, 0, obj.transform.position.z + 1), Quaternion.Euler(90, 0, 0));
                if (!(obj.transform.position.z - 1 < 0))
                    A_Dx = Instantiate(manager.cursor, new Vector3(obj.transform.position.x + 1, 0, obj.transform.position.z - 1), Quaternion.Euler(90, 0, 0));
            }
            

            if (!(obj.transform.position.x - 1 < 0))// angoli posteriori
            {
            P_forward = obj.transform.position.x - 1;

                if (!(obj.transform.position.z + 1 > 7))
                    P_Sx = Instantiate(manager.cursor, new Vector3(obj.transform.position.x - 1, 0, obj.transform.position.z + 1), Quaternion.Euler(90, 0, 0));
                if (!(obj.transform.position.z - 1 < 0))
                    P_Dx = Instantiate(manager.cursor, new Vector3(obj.transform.position.x - 1, 0, obj.transform.position.z - 1), Quaternion.Euler(90, 0, 0));
            }
           
        GameObject[] list = GameObject.FindGameObjectsWithTag("Man");
        
        foreach (GameObject c in list)
        {
            if (A_Sx != null)
            {

                if (c.transform.position.x.Equals(A_Sx.transform.position.x) && c.transform.position.z.Equals(A_Sx.transform.position.z))
                {// controllo se angolo anteriore sx c'è nemico o amico

                    if (c.layer.Equals(manager.objSelected.layer)) // è amico
                    {
                        A_Sx.GetComponentInChildren<Light>().color = Color.blue;
                        A_Sx.GetComponent<BoxCollider>().enabled = false;

                    }
                    else // nemico
                    {
                        A_Sx.GetComponentInChildren<Light>().color = Color.red;
                        A_Sx.GetComponent<BoxCollider>().enabled = false;
                        canJump(A_forward + 1, A_Sx.transform.position.z + 1);
                    }

                    occupied = true;
                }
            }
            if (P_Sx != null)
            {
                if (c.transform.position.x.Equals(P_Sx.transform.position.x) && c.transform.position.z.Equals(P_Sx.transform.position.z))
                {// controllo se angoli sx c'è nemico o amico

                    if (c.layer.Equals(manager.objSelected.layer)) // è amico
                    {
                        P_Sx.GetComponentInChildren<Light>().color = Color.blue;
                        P_Sx.GetComponent<BoxCollider>().enabled = false;

                    }
                    else // nemico
                    {
                        P_Sx.GetComponentInChildren<Light>().color = Color.red;
                        P_Sx.GetComponent<BoxCollider>().enabled = false;

                        canJump(P_forward - 1, P_Sx.transform.position.z + 1);
                    }

                    occupied = true;
                }
            }
            if (A_Dx != null)
            {
                if (c.transform.position.x.Equals(A_Dx.transform.position.x) && c.transform.position.z.Equals(A_Dx.transform.position.z))
                {
                    if (c.layer.Equals(obj.layer)) // amico 
                    {
                        A_Dx.GetComponentInChildren<Light>().color = Color.blue;
                        A_Dx.GetComponent<BoxCollider>().enabled = false;

                    }
                    else //  nemico
                    {
                        A_Dx.GetComponentInChildren<Light>().color = Color.red;
                        A_Dx.GetComponent<BoxCollider>().enabled = false;
                        canJump(A_forward + 1, A_Dx.transform.position.z - 1);
                    }

                    occupied = true;
                }
            }
            if (P_Dx != null)
            {
                if (c.transform.position.x.Equals(P_Dx.transform.position.x) && c.transform.position.z.Equals(P_Dx.transform.position.z))
                {
                    if (c.layer.Equals(obj.layer)) // amico 
                    {
                        P_Dx.GetComponentInChildren<Light>().color = Color.blue;
                        P_Dx.GetComponent<BoxCollider>().enabled = false;

                    }
                    else //  nemico
                    {
                        P_Dx.GetComponentInChildren<Light>().color = Color.red;
                        P_Dx.GetComponent<BoxCollider>().enabled = false;
                        canJump(P_forward - 1, P_Dx.transform.position.z - 1);
                    }

                    occupied = true;
                }
            }
        }
        return occupied;
    }

    public bool canJump(float x, float z)
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

        Destination = Instantiate(manager.cursor, new Vector3(x, 0, z), Quaternion.Euler(90, 0, 0));
        Destination.GetComponentInChildren<Light>().color = Color.yellow;
        return true;
    }
}

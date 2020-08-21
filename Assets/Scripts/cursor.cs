using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    float tempX;

    public void OnMouseDown()
    {
        manager.resetLight();

        if (this.GetComponentInChildren<Light>().color.Equals(Color.yellow))
        {

            if (manager.objSelected.layer == 8)
                tempX = this.gameObject.transform.position.x - 1;
            else
                tempX = this.gameObject.transform.position.x + 1;

            if (manager.objSelected.transform.position.z < this.gameObject.transform.position.z)
                manager.findAndDestroy(tempX, this.gameObject.transform.position.z - 1);
            else
                manager.findAndDestroy(tempX, manager.objSelected.transform.position.z - 1);

            //(manager.Double(manager.objSelected))
        }
        GameObject.Find(manager.objSelected.name.ToString()).transform.position = this.gameObject.transform.position;

        if (manager.objSelected.layer.Equals(8) && manager.objSelected.transform.position.x == 7  ||
            manager.objSelected.layer.Equals(9) && manager.objSelected.transform.position.x == 0 )
            manager.BecomeKing(manager.objSelected);


        if (manager.turn.Equals(8))
            manager.turn = 9;
        else
            manager.turn = 8;

    }
}



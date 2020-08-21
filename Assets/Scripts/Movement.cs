using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private void OnMouseDown()
    {
        manager.resetLight();

        if (manager.turn.Equals(this.gameObject.layer))
        {
            manager.objSelected = this.gameObject;
            
            manager.cellOccupated(this.gameObject);
          
        }
    }


}

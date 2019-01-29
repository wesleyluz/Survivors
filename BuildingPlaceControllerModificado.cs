using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlaceControllerModificado : MonoBehaviour
{
    public StoreManeger Store;
    public bool isFree = true;
    void Start()
    {
        
    }

    void Update()
    {
           
    }
    void OnMouseDown()
    {
        Store.SendMessage("ColocanoMundo",this,SendMessageOptions.RequireReceiver);
    }
}

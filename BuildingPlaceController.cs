using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlaceController : MonoBehaviour
{

    public bool isFree = true;
    public bool isActive = false;
    public StoreManeger storeManagerObject; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        storeManagerObject.ColocaNoMundo(gameObject);
        isFree = false;
    }
}

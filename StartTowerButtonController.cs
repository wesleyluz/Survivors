using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTowerButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameController gameController;
    public Image Store;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTower(){
        gameController.Status = "TOWER";
        Store.enabled = false;
    }
}

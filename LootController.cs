using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    // Start is called before the first frame update
    private int Qtd;
    
    void Start()
    {
        Qtd = Random.Range(10,45);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){

        if(col.collider.tag == "Player"){
            controls Player = col.collider.GetComponent<controls>();
            if(this.tag == "EC"){

                Player.setEnergyCells(Qtd,false);

            }else if(this.tag == "PT"){

                Player.setPowerTanks(Qtd,false);

            }else if(this.tag == "WD"){

                Player.setWood(Qtd,false);

            }else if(this.tag == "MT"){

                Player.setMetal(Qtd,false);

            }else if(this.tag == "CN"){

                Player.setCannons(Qtd,false);

            }
            Destroy(gameObject);
        }
    }
}

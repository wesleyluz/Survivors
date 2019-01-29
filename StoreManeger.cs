using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManeger : MonoBehaviour
{
    bool localisfree,HaveMoney = false;
    int construido;
    public controls Player;
    int wood,metal,energycell,cannons,powertank;
    public GameObject torre;
    public TowerSelect ClickBtn { get;private set; }
    public Button StoreButton;
    public Button StartTowerButton;
    public GameController gameController;
    private AudioSource audioSource;

    void Update(){

        if(gameController.Status == "NOITE"){
            StoreButton.interactable = true;
            StartTowerButton.interactable = true;
        }else {
            StoreButton.interactable = false;
            StartTowerButton.interactable = false;
        }
    }
    
    public void MoneyBitch()
    {   
        Recursos();
        if(ClickBtn.TowerPrefab.tag == "TorreAzul")
        {
            if(metal>=100 && wood >= 25 && energycell>=2 && cannons>=1 )
            {
                HaveMoney = true;
                Player.setMetal(100,true);
                Player.setEnergyCells(2,true);
                Player.setCannons (1,true);
                Player.setWood (25,true);
            }else{
                print("Bitch better have my money");
            }
        }else if(ClickBtn.TowerPrefab.tag == "TorreRoxa"){
            if(metal>=100 && wood >= 25 && powertank>=2)
            {
                HaveMoney = true;
                Player.setMetal(100,true);
                Player.setPowerTanks(2,true);
                Player.setWood (25,true);
            }
        }else if(ClickBtn.TowerPrefab.tag == "Eletrica"){
            if(metal>=100 && wood >= 25 && powertank>=2 && energycell>=2 && cannons>=1)
            {
                HaveMoney = true;
                Player.setMetal(100,true);
                Player.setPowerTanks(2,true);
                Player.setWood (25,true);
                Player.setEnergyCells(2,true);
                Player.setCannons (1,true);
            }
        }
    }


    public void ColocaNoMundo(GameObject Maneger)
    {   
        if(Player.vida >0 && construido >0){
            localisfree = Maneger.GetComponent<BuildingPlaceController>().isFree && Maneger.GetComponent<BuildingPlaceController>().isActive;   
            if(localisfree){   
                Vector2 spawnPosition = Maneger.transform.position;
                if(Input.GetButtonDown("Fire1")){
                    MoneyBitch();
                    if(HaveMoney){
                        print("123");
                        Instantiate(ClickBtn.TowerPrefab,spawnPosition,Quaternion.identity);
                        audioSource = GetComponent<AudioSource>();
                        audioSource.Play();
                        HaveMoney = false;
                        construido = 0;
                    }
                }
            }else{

            }
        } 
    }
    public void Recursos()
    {
        wood = Player.getWood();
        metal = Player.getMetal();
        energycell = Player.getEnergyCells();
        powertank = Player.getPowerTanks();
        cannons = Player.getCannons();


    }
    public void PickTower(TowerSelect towerSelect)
    {
        this.ClickBtn = towerSelect;
        construido = 1;
    }
}

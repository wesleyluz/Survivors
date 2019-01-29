using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text Horas;
    public float horasCont = 17f;
    public Text Minutos;
    public float minutosCont= 0f;
    public Tilemap Zona2;
    public GameObject[] BuildingPlacesZona1;
    public GameObject[] BuildingPlacesZona2;
    public GameObject[] BuildingPlacesZona3;
    private GameObject[] SpawnPointsZona1;
    private GameObject[] SpawnPointsZona2;
    private GameObject[] SpawnPointsZona3;
    public Tilemap Zona3;
    public Tilemap Zona4;
    public Interacao Base;
    public Text Wood;
    public Text Metal;
    public Text EnergyCells;
    public Text PowerTanks;
    public Text Cannons;
    public controls Player;
    public Image EfeitoNoite;
    private float Timing = 10f;
    private bool NoiteComecando = false;
    private float AlphaNoite = 0f;
    private float AlphaNevoa = 1f;
    public string Status = "DIA";
    public Transform PlayerInitialPos;
    public Text WaveText;
    public int Wave = 0;
    public int NumEnemy = 10;
    public GameObject[] Enemies;
    private bool comecou = false;
    public List<GameObject> currentWaveEnemies;
    



    // Start is called before the first frame update
    void Start()
    {        
        
        
        EfeitoNoite.enabled = false;
        EfeitoNoite.color = new Color(1,1,1,AlphaNoite);
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Fire2")){
            horasCont = 17;
            minutosCont = 50;
        }
        //IMPRIME A QUATIDADE DE RECURSOS NA TELA
        Recursos();
        WaveText.text = Wave.ToString();
        Horas.text = horasCont.ToString();
        // TOSTRING("F0") DEIXA SO UMA CASA DECIMAL DO FLOAT
        if( minutosCont >= 30f){
            Minutos.text = "30";
        }else{
            Minutos.text = "00";
        }
        

        if(Status.Equals("DIA")){
            Base.Used = false;
            ContaTempo();
            StartCoroutine(desabilitandoBuilding());


        }else if(Status.Equals("ANOITECENDO")){
            Anoitece();
        }else if(Status.Equals("TOWER")){
            SpawnPointsZona1 = GameObject.FindGameObjectsWithTag("SpawnZona1");
            StartCoroutine(desabilitandoBuilding());
            startWave();
            comecou = true;
            ContaTempo();
        }else if(Status.Equals("NOITE")){
            StartCoroutine(habilitandoBuilding());


        }else if(Status.Equals("ZONA2")){
            LiberaZona(Zona2);
            
            foreach(GameObject x in BuildingPlacesZona2){
                if(x != null){
                    x.GetComponent<BuildingPlaceController>().isActive = true;
                }
            } 
        }else if (Status.Equals("ZONA3")){
            LiberaZona(Zona3);
            foreach(GameObject x in BuildingPlacesZona3){
                if(x != null){
                x.GetComponent<BuildingPlaceController>().isActive = true;
                }
            }
        }else if(Status.Equals("AMANHECENDO")){
            Timing = 10f;
            Amanhece();
            
            
        }else if(Status.Equals("ZONA4")){
            LiberaZona(Zona4);
        }
        endWave();
    }   
// TESTE DE AUMENTO DA AREA EXPLORAVEL
    //     if(Input.GetKey("space")){
            
    //         Status = "ZONA2";
    //     }
    //     if(Input.GetKey("left alt")){
            
    //         Status = "ZONA3";
    //     }

// CONTA O TEMPO DE ACORDO COM FIXED DELTA TIME
    void ContaTempo(){
        minutosCont += ((Time.fixedDeltaTime*5));
        if(minutosCont > 59f){
            horasCont++;
            minutosCont = 0f;
            if(horasCont > 17f){
                if(Status == "DIA"){
                    Status = "ANOITECENDO";
                    EfeitoNoite.enabled = true;
                    NoiteComecando = true;
                }
                if(Status == "NOITE"){
                    
                }
            }
        }
        if(horasCont >23){
            horasCont = 0;
        }
        if( horasCont == 6 && minutosCont == 0f){
            Status = "AMANHECENDO";
            print("AMANHECEU CONTATEMPO");
        }
    }
// FAZ A TRANSIÇÃO ENTRE DIA E NOITE USANDO A IMAGEM PASSADA
    void Anoitece(){
        if(Timing >= 0 && NoiteComecando){
            Timing -= Time.fixedDeltaTime;
            if( AlphaNoite < 0.4f){
                AlphaNoite += Time.fixedDeltaTime *0.1f;
                EfeitoNoite.color = new Color(0.05f,0.02f,0.2f,AlphaNoite);
            }
        }
        if(Timing <= 0){
            Timing = 10f;
        }
    }
// VAI SER USADA APÓS O SISTEMA DE TOWER DEFENSE FOR CRIADO
    void Amanhece(){
        if(Timing >= 0 && NoiteComecando){
            Timing -= Time.fixedDeltaTime;
            if( AlphaNoite > 0f){
                AlphaNoite -= Time.fixedDeltaTime *0.1f;
                EfeitoNoite.color = new Color(0.05f,0.02f,0.2f,AlphaNoite);
            }
        }
        if(Timing <= 0){
            Timing = 10f;
        }
        if( AlphaNoite <= 0){
            Status = "DIA";
            endTowerDefense();
            comecou = false;
        }
    }
    // LIBERA A ZONA PASSADA COMO PARAMETRO E ATIVA TODOS SEUS RESPECTIVOS BUILDINGPLACES
    void LiberaZona(Tilemap zona){
        // print("LIBERANDO ZONA 2: " + Timing);
        if(Timing >= 0){
            Timing -= Time.fixedDeltaTime;
            if( AlphaNevoa >= 0f){
                AlphaNevoa -= Time.fixedDeltaTime;
                zona.color = new Color(0.3962264f,0.143042f,0.1252225f,AlphaNevoa);
            }else{
                Timing = 0;
            }
        }
        if(Timing <= 0){
            Timing = 10f;
            Destroy(zona.GetComponent<TilemapRenderer>());
            foreach (BoxCollider2D x in zona.GetComponents<BoxCollider2D>()){
                x.enabled = false;
            }
            // Destroy(zona);
            
            AlphaNevoa = 1f;
            Status = "AMANHECENDO";
            print("AMANHECEU LIBERAZONA");
        }
    }

    void Recursos(){
        Wood.text = Player.getWood().ToString();
        Metal.text = Player.getMetal().ToString();
        EnergyCells.text = Player.getEnergyCells().ToString();
        PowerTanks.text = Player.getPowerTanks().ToString();
        Cannons.text = Player.getCannons().ToString();

    }
    void endTowerDefense(){
        Player.transform.position = PlayerInitialPos.position;
        Player.GetComponent<SpriteRenderer>().enabled = true;
        Player.GetComponent<BoxCollider2D>().enabled = true;
        Status = "DIA";
        print("AMANHECEU");
    }

    void startWave(){
        // print("COMEÇOU A ONDA");
        if(!comecou){
            Wave++;
            if(Wave < 3){
                
                for(int i = 0; i < NumEnemy;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[0],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO");
                }
                
            }else if(Wave >= 3 && Wave <6){
                
                NumEnemy = 15;
                for(int i = 0; i < NumEnemy;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[0],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO");
                }
                for(int i = 0; i < NumEnemy/2;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[1],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO ESPECIAL1");
                }
            }else if(Wave >= 6 && Wave <9){
                
                NumEnemy = 25;
                for(int i = 0; i < NumEnemy;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[0],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO");
                }
                for(int i = 0; i < NumEnemy/2;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[1],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO ESPECIAL1");
                }
                for(int i = 0; i < NumEnemy/3;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[2],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO ESPECIAL2");
                }
            }else if(Wave >= 9){
                NumEnemy = 30;
                for(int i = 0; i < NumEnemy;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[0],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO");
                }
                for(int i = 0; i < NumEnemy/2;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[1],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO ESPECIAL1");
                }
                for(int i = 0; i < NumEnemy/3;i++){
                    currentWaveEnemies.Add(Instantiate(Enemies[2],SpawnPointsZona1[Random.Range(0,SpawnPointsZona1.Length)].transform.position,Quaternion.identity));
                    print("TA CRIANDO BIXO ESPECIAL2");
                }
            }
        }
        
    }
    void endWave(){
        
        if(Status == "TOWER"){
            print("COUNT: "+ currentWaveEnemies.Count);
            
            if(CheckList()){
                horasCont = 6;
                minutosCont = 0;
                if(Wave == 3){
                    Status = "ZONA2";
                }else if(Wave == 6){
                    Status = "ZONA3";
                }else if(Wave >= 9){
                    SceneManager.LoadScene("Menu");
                }else{
                    print("WAVE: " + Wave);
                    Status = "AMANHECENDO";
                    print("AMANHECEU ENDWAVE");
                }
            }
            // foreach(GameObject x in currentWaveEnemies){
            //     Destroy(x);
            // }
                
        }
        
    }
    

    IEnumerator desabilitandoBuilding(){
        print("DESABILITANDO coroutine");
        foreach(GameObject x in BuildingPlacesZona2){
            if(x != null){
                x.SetActive(false);
            }     
        }

        foreach(GameObject x in BuildingPlacesZona3){
            if(x != null){
                x.SetActive(false);
            }            
        }
        foreach(GameObject x in BuildingPlacesZona1){
                if(x != null){
                x.SetActive(false);
            } 
        }
        
        
        yield return null;
    }

    IEnumerator habilitandoBuilding(){
        foreach(GameObject x in BuildingPlacesZona2){
                if(x != null){
                    x.SetActive(true);
                }
            }

        foreach(GameObject x in BuildingPlacesZona3){
                if(x != null){
                    x.SetActive(true);
                }
            }
        foreach(GameObject x in BuildingPlacesZona1){
                if(x != null){
                    x.SetActive(true);
                }
            }
        
        yield return null;
    }

    bool CheckList(){
        bool emptyList = true;

        foreach(GameObject x in currentWaveEnemies){
            if(x != null){
                emptyList = false;
            }else{
            }
        }
        return emptyList;
    }

    public void skip(){
        if(Status == "DIA"){
            horasCont = 17;
            minutosCont = 50;
        }
    }
}

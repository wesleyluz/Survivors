using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacao : MonoBehaviour,Interable
{
    [SerializeField]
    private Image barra;
    private int LootQtd;
    public bool Used = false;
    public GameObject[] Coletaveis;
    public bool isLootBox = false;
    public bool isBase = false;
    private Animator animator;
    public GameObject Player;
    public GameController gameController;
    private Coroutine start;
    [SerializeField]
    private Canvas canvas;

    public void Start()
    {
        if(!isBase){
        canvas.enabled =false;
        }
        barra.fillAmount = 0;
    }
    public virtual void Interact()
    {   
        if(!isBase && !Used){
        canvas.enabled = true;
        start = StartCoroutine(Progresso());
        }else{
            startTowerDefense();
        }

    }

    public virtual void StopInteract()
    {
        if(start!= null)
        {
            StopCoroutine(start);
            start = null;
            canvas.enabled = false;
        }
    }

    private IEnumerator Progresso(){
        float timeleft = Time.deltaTime;
        float rate = 1.0f/5.0f;
        float progress = 0.0f;
        while(progress <1.0)
        {
            barra.fillAmount = progress;
            progress += rate*Time.deltaTime;
            yield return null;
        }
        if(barra.fillAmount > 0.90){    
            animator = GetComponent<Animator>();
            if(!Used){
                print("TA INTERAGINDO");
                LootQtd = Random.Range(3,6);
            
                if(isLootBox){
                    animator.SetBool("Open",true);
                    gerarLoot();
                }
                if(isBase){
                    print("TA NA BASE");
                    startTowerDefense();
                }
                Used = true;
                canvas.enabled =false;
            }
            // Destroy(gameObject);
        }
    }
    void gerarLoot(){


        for(int x = 0; x < LootQtd;x++){
            int randomItem = Random.Range(0,Coletaveis.Length);
            Instantiate(Coletaveis[randomItem],new Vector2(transform.position.x+Random.Range(0,2),transform.position.y+Random.Range(0,2)),Quaternion.identity);
        }
    }

    void startTowerDefense(){
        if(gameController.Status != "DIA"){
            Player.GetComponent<SpriteRenderer>().enabled = false;
            Player.GetComponent<BoxCollider2D>().enabled = false;
            gameController.Status = "NOITE";
            gameController.horasCont = 19;
            print("PODE ENTRAR");
        }else{
            print("INDISPONIVEL");
        } 
    }
    
    // public float OpenChest()
    // { 
    // }
}

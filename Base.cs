using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{   
    [SerializeField]
    private Stat health;
    public float vida,vidaMax;
    private GameController game;

    
    // Start is called before the first frame update
    void Start()
    {
        health.Inicialize(vida,vida);   
        vidaMax = vida; 
    }

    // Update is called once per frame
    void Update()
    {
        health.MyCurrentValue = vida;
        if(vida <= 0){
            SceneManager.LoadScene("Death");
        }
    }

    public void TakeDamage(int damage)
    {
        vida -=damage;
    }
    public void Restauracao()
    {
        if(game.Status.Equals("DIA"))
        {
            if(vida < vidaMax)
            {
                vida = (vidaMax-vida)/2;   
            }
        }
    }
}

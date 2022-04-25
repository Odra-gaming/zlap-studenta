using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;
    
    //trzeba skonwertowaæ nasze aktualne mapy na game objects, zeby przekazywac je jako kolejne mapy do gry dla gracza w funkcji NextRound()/NewRound()
    //public GameObject[] Maps;

    public Player player;

    public Transform pellets;

    public int score { get; private set; }

    public int lives { get; private set; }
    //rozpocznij gre na start
    private void Start()
    {
        NewGame();
    }
    //funkcja rozpoczecia gry
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    //ustaw score
    private void SetScore(int score)
    {
        this.score = score;

    }

    //wywolanie nowej rundy - wywolaj wszystkie pelletsy oraz wykonaj reset state 
    private void NewRound()
    {
        
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }
    //reset state - gra zostaje zrestartowana OPROCZ aktualnie zjedzonych pelletsow
    private void ResetState()
    {

        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(true);

        }

        this.player.gameObject.SetActive(true);

    }
    //ustaw ilosc zyc
    private void SetLives(int lives)
    {
        this.lives = lives;

    }

    //zakonczenie gry 
    private void GameOver()
    {
        //wszystkie obiekty przeciwnikow zostaja wylaczone
        for (int i = 0; i < this.enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(false);

        }
       //wylacz obiekt gracza (potencjalnie podwojne wylaczenie, sprawdz potem
        this.player.gameObject.SetActive(false);

    }
    //Gracz "zjada" przeciwnika
    public void EnemyPowered(Enemy enemy)
    {
        SetScore(this.score + enemy.points);
    }
    //gracz zostaje "zjedzony" przez przeciwnika
    public void PlayerLost()
    {
        //wylacz obiekt gracza
        this.player.gameObject.SetActive(false);
        //zmniejsz ilosc zyc gracza
        SetLives(this.lives - 1);

        if(this.lives > 0)
        {
            //po 3 sekundach wykonaj reset
            Invoke(nameof(ResetState), 3.0f);

        }
        else
        {
            //gracz nie ma zyc, zakoncz gre
            GameOver();

        }
    }
}

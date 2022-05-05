using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy[] enemies;

    //trzeba skonwertowa? nasze aktualne mapy na game objects, zeby przekazywac je jako kolejne mapy do gry dla gracza w funkcji NextRound()/NewRound()
    //public GameObject[] Maps;

    public player_movement player;

    public Transform pellets;

    public int multiplier { get; private set; } = 1;

    public int score { get; private set; }

    public int lives { get; private set; }


    //rozpocznij gre na start
    private void Start()
    {
        NewGame();
    }

    //sprawdzanie, czy gracz zresetowa³ grê po jej zakoñczeniu
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
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

        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }


    //reset state - gra zostaje zrestartowana OPROCZ aktualnie zjedzonych pelletsow
    private void ResetState()
    {
        Debug.Log("reset state gry - rozpoczêcie resetowania przeciwników i gracza");
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("reset state gry - wywo³anie resetu przeciwników");
            enemies[i].ResetState();
            

        }

        player.ResetState();

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
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(false);

        }
        //wylacz obiekt gracza (potencjalnie podwojne wylaczenie, sprawdz potem
        player.gameObject.SetActive(false);

    }


    //Gracz "zjada" przeciwnika
    public void EnemyPowered(Enemy enemy)
    {
        //int points = enemy.points * multiplier;
        //SetScore(score + points);
        multiplier++;
    }


    //gracz zostaje "zjedzony" przez przeciwnika
    public void PlayerLost()
    {
        //wylacz obiekt gracza
        player.gameObject.SetActive(false);

        //zmniejsz ilosc zyc gracza
        SetLives(lives - 1);

        if (lives > 0)
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

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            player.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            //enemies[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetMultiplier));
        Invoke(nameof(ResetMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetMultiplier()
    {
        multiplier = 1;
    }
}

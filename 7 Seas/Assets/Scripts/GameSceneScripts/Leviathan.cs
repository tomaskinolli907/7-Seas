using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leviathan : MonoBehaviour
{
    public RoundEvents roundEvents;
    public GameLoop gameLoop;
    public GameObject leviathan;
    Player currentPlayer;
    Vector2 center;
    float distance = 0;
    float prevDistance = 0;
    public bool eatPlayer = false;
    float camDist = 5f;
    Coroutine co;
    private AudioSource source;
    public AudioClip clip;
    public Sprite leviathanMouthOpen;
    public Sprite leviathanMouthClosed;
    public GameObject endTurnButton;
    public LoseTurn loseTurn;
    public Camera audioCamera;

    private void Start()
    {
        center = new Vector2(31f, -31f);

        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            eatPlayer = true;
        }

        //if eatplayer boolean is set, find the closest player and appear then eat him
        if(eatPlayer)
        {
            Player closestPlayer;
            closestPlayer = FindPlayerClosestToCenter();
            currentPlayer = gameLoop.players.playersList[gameLoop.playersTurn - 1];
            if (currentPlayer == closestPlayer && Vector2.Distance(currentPlayer.transform.position, Camera.main.transform.position) < camDist)
            {
                co = StartCoroutine(EatPlayer(closestPlayer));
                eatPlayer = false;
            }
        }

    }

    //finds the player in the center
    Player FindPlayerClosestToCenter()
    {
        var currentEvent = roundEvents.currentEvent;
        var currentPlayer = gameLoop.players.playersList[gameLoop.playersTurn - 1];
        Player closestPlayer = gameLoop.players.playersList[0];
        distance = Vector2.Distance(gameLoop.players.playersList[0].transform.position, center);

        foreach (var player in gameLoop.players.playersList)
        {
            prevDistance = Vector2.Distance(closestPlayer.transform.position, center);
            distance = Vector2.Distance(player.transform.position, center);

            if(distance <= prevDistance)
            {
                closestPlayer = player;
                
            }

        }
        return closestPlayer;

    }

    //runs the events to cause the leviathan to eat the player by having him appear, disappear and play sounds
    IEnumerator EatPlayer(Player closestPlayer)
    {
        leviathan.GetComponent<SpriteRenderer>().enabled = true;
        audioCamera.GetComponent<AudioSource>().Pause();
        endTurnButton.SetActive(false);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(.5f);
        leviathan.transform.position = new Vector3(closestPlayer.transform.position.x, closestPlayer.transform.position.y, leviathan.transform.position.z);
        yield return new WaitForSeconds(2f);
        leviathan.GetComponent<SpriteRenderer>().sprite = leviathanMouthClosed;
        closestPlayer.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameLoop.HideHighlights();
        closestPlayer.gold = 0;
        yield return new WaitForSeconds(1f);
        leviathan.GetComponent<SpriteRenderer>().enabled = false;
        leviathan.GetComponent<SpriteRenderer>().sprite = leviathanMouthOpen;
        leviathan.transform.position = new Vector3(0, 0, leviathan.transform.position.z);

        audioCamera.GetComponent<AudioSource>().UnPause();
        yield return new WaitForSeconds(1f);
        closestPlayer.transform.position = new Vector3(closestPlayer.homePort.x * (int)gameLoop.tE.tileSize, 
            -closestPlayer.homePort.y * (int)gameLoop.tE.tileSize, closestPlayer.transform.position.z);
        closestPlayer.shipPos = closestPlayer.homePort;
        closestPlayer.GetComponentInChildren<SpriteRenderer>().enabled = true;
        loseTurn.ShowWindow("Lose Turn, you've been eaten by the Leviathan!");

    }

}

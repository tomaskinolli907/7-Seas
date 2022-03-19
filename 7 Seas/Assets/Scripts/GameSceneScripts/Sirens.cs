using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sirens : MonoBehaviour
{

    public TilingEngine tE;
    public GameLoop gameLoop;
    Vector2 pos;
    RaycastHit2D hit1;
    RaycastHit2D hit2;
    RaycastHit2D hit3;
    RaycastHit2D hit4;
    Player currentPlayer;
    string previousPlayerName = "null";
    //Player previousPlayer = null ;
    bool oneTime = false;
    int tileBoard = 9;
    int tileBoardMask;
    public GameObject sirenTile;

    private AudioSource source;
    public AudioClip sirenSong;
    public delegate void AudioCallback();

    public bool move = false;
    public Vector3 startPoint;
    public Vector3 endPoint;
    float timeStartedMoving;
    float timeTaken = .8f;
    float rotationLeft = 360;
    Camera mainCamera;
    CameraControl camCont;
    public Sprite reefSprite;
    public LoseTurn loseTurn;


    // Use this for initialization
    void Start()
    {
        camCont = Camera.main.GetComponent<CameraControl>();
        sirenTile.SetActive(false);
        source = GetComponent<AudioSource>();
        tileBoardMask = 1 << tileBoard;
    }

    void ResetColliders()
    {
        hit1 = new RaycastHit2D();
        hit2 = new RaycastHit2D();
        hit3 = new RaycastHit2D();
        hit4 = new RaycastHit2D();
    }

    public void CallSiren()
    {
        //currentPlayer = gameLoop.players.playersList[gameLoop.playersTurn - 1];
        hit1 = Physics2D.Raycast(currentPlayer.transform.position, Vector2.up, Mathf.Infinity, tileBoardMask);
        hit2 = Physics2D.Raycast(currentPlayer.transform.position, Vector2.right, Mathf.Infinity, tileBoardMask);
        hit3 = Physics2D.Raycast(currentPlayer.transform.position, Vector2.down, Mathf.Infinity, tileBoardMask);
        hit4 = Physics2D.Raycast(currentPlayer.transform.position, Vector2.left, Mathf.Infinity, tileBoardMask);
    }

    void FixedUpdate()
    {

        //if set to move, lerp between players start and end points
        if (move)
        {
            //turn off collision detection while moving
            currentPlayer.GetComponent<Collider2D>().enabled = false;
            float timeSinceMoving = Time.time - timeStartedMoving;
            float percentComp = timeSinceMoving / timeTaken;
            currentPlayer.transform.position = Vector3.Lerp(startPoint, endPoint, percentComp);
            loseTurn.endTurnButton.SetActive(false);

            //rotate player
            float rotation = percentComp*rotationLeft;
            var playerSprite = currentPlayer.GetComponentInChildren<SpriteRenderer>();
            playerSprite.transform.rotation = Quaternion.identity;
            playerSprite.transform.Rotate(0, 0, rotation);
            //GetComponent<Animator>().SetBool("MusicNotes", true);
            camCont.CullTiles();

            //if (move && (int)(pL.transform.position.x / tE.tileSize) == endPoint.x && -(int)(pL.transform.position.y / tE.tileSize) == endPoint.y)
            if (percentComp >= 1.0f)
            {
                //turn collision detection back on after moving
                currentPlayer.GetComponent<Collider2D>().enabled = true;
                //source.Stop();
                move = false;
                currentPlayer.transform.position = new Vector3(Mathf.RoundToInt(currentPlayer.transform.position.x),
                    Mathf.RoundToInt(currentPlayer.transform.position.y), currentPlayer.transform.position.z);
                //Debug.Log("Moving Complete!");
                currentPlayer.shipPos = new Vector2(Mathf.RoundToInt(currentPlayer.transform.position.x / 2), Mathf.Round(currentPlayer.transform.position.y / -2));
                //gameLoop.showAvailableMoves();
                loseTurn.ShowWindow("Lose Turn, Sirens Shipwrecked You!");

                camCont.CullTiles();
            }

        }


    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T) || gameLoop.roundEvents.currentEvent == "Sirens on Reef")
        {
            currentPlayer = gameLoop.players.playersList[gameLoop.playersTurn - 1];
            //Debug.Log("Previous Player: " + previousPlayerName + "  Current Player: " + currentPlayer.name);
            if (previousPlayerName != currentPlayer.name)
            {
                //Debug.Log("Previous Player: " + previousPlayerName + "  Current Player: " + currentPlayer.name);
                oneTime = false;
            }
            if (!oneTime && Vector2.Distance(currentPlayer.transform.position, Camera.main.transform.position) < 10)
            {
                oneTime = true;
                previousPlayerName = currentPlayer.name;
                CallSiren();

                if (hit1.collider != null && hit1.collider.GetComponent<SpriteRenderer>().sprite.name == reefSprite.name)
                {
                    var targetPos = hit1.transform.position;
                    endPoint = new Vector3(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y - 2), currentPlayer.transform.position.z);
                    SirenEvent(targetPos, endPoint);
                }
                else if (hit2.collider != null && hit2.collider.GetComponent<SpriteRenderer>().sprite.name == reefSprite.name)
                {
                    var targetPos = hit2.transform.position;
                    endPoint = new Vector3(Mathf.RoundToInt(targetPos.x - 2), Mathf.RoundToInt(targetPos.y), currentPlayer.transform.position.z);
                    SirenEvent(targetPos, endPoint);
                }
                else if (hit3.collider != null && hit3.collider.GetComponent<SpriteRenderer>().sprite.name == reefSprite.name)
                {
                    var targetPos = hit3.transform.position;
                    endPoint = new Vector3(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y + 2), currentPlayer.transform.position.z);
                    SirenEvent(targetPos, endPoint);
                }
                else if (hit4.collider != null && hit4.collider.GetComponent<SpriteRenderer>().sprite.name == reefSprite.name)
                {
                    var targetPos = hit4.transform.position;
                    endPoint = new Vector3(Mathf.RoundToInt(targetPos.x + 2), Mathf.RoundToInt(targetPos.y), currentPlayer.transform.position.z);
                    SirenEvent(targetPos, endPoint);
                }

            }

        }


    }

    void SirenEvent(Vector3 tarPos, Vector3 eP)
    {
        sirenTile.transform.position = new Vector3(tarPos.x, tarPos.y, -6f);
        sirenTile.SetActive(true);
        GetComponent<Animator>().Play("MusicNotes");
        gameLoop.HideHighlights();
        move = true;
        source.PlayOneShot(sirenSong, 1f);
        PlaySoundWithCallback(sirenSong, AudioFinished);
        timeStartedMoving = Time.time;
        startPoint = currentPlayer.transform.position;
        endPoint = eP;
        currentPlayer.shipPos = new Vector2(Mathf.RoundToInt(currentPlayer.transform.position.x / 2), Mathf.Round(currentPlayer.transform.position.y / -2));
        ResetColliders();
    }

    public void PlaySoundWithCallback(AudioClip clip, AudioCallback callback)
    {
        source.PlayOneShot(clip);
        StartCoroutine(DelayedCallback(clip.length, callback));
    }
    private IEnumerator DelayedCallback(float time, AudioCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
    void AudioFinished()
    {
        sirenTile.SetActive(false);
        GetComponent<Animator>().StopPlayback();
    }




}

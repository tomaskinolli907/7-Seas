using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {
    public Camera mainCamera;
    //public GameObject player;
    public bool scrollOn = true;
    public TilingEngine tE;
    public GameLoop gameLoop;
    public Players players;
    Vector3 fullScreenCamera;
    public bool fullScreen = false;
    bool followScreen = true;
    Player player;

    float rightBound;
    float leftBound;
    float topBound;
    float bottomBound;
    public SpriteRenderer spriteBounds;
    Vector3 targetPos;
    GameObject fsb;
    public GameObject gameBoarder;
    public GameObject sidebarBoarder;
    public GameObject treasureMapBG;
    public float offPlayerWaitTime = .5f;
    public float onPlayerWaitTime = 1.25f;
    public float offShipWaitTime = .1f;
    public float onShipWaitTime = .1f;


    public int playerCameraSize = 9;

    //camera pinch zoom speed
    public float pinchZoomSpeed;

    //camera pan speed
    public float panSpeed;

    bool followPlayer;
    bool isZooming = false;
    bool screenSet = false;

    Coroutine coPlayer;
    Coroutine coTarShip;


    // Use this for initialization
    void Start () {
        fsb = GameObject.Find("fullscreen");
        panSpeed = 0.03f;
        followPlayer = true;
        pinchZoomSpeed = .06f;
        scrollOn = true;

        //initialize camera components
        mainCamera.orthographicSize = playerCameraSize;
        fullScreenCamera = new Vector3(63f - 32, 1-32, mainCamera.transform.position.z);

    }

    //used for the fullscreen/playerscreen button
    public void SwitchScreens()
    {
        fullScreen = !fullScreen;
        followScreen = !followScreen;


        if (fullScreen)//set to full screen
        {
            mainCamera.orthographicSize = 32;
            CullTiles();
            fsb.GetComponentInChildren<Text>().text = "Player Screen";
            gameLoop.players.playersList[gameLoop.playersTurn - 1].arrow.GetComponent<SpriteRenderer>().enabled = false;
            gameLoop.players.playersList[gameLoop.playersTurn - 1].spotLight.SetActive(false);
            gameLoop.HideHighlights();

            //start coroutines to blink player and target ships
            coPlayer = StartCoroutine(FlashPlayer());
            coTarShip = StartCoroutine(FlashTargetShips());

        }
        else//set to player screen
        {
            mainCamera.orthographicSize = playerCameraSize;
            player = players.playersList[gameLoop.playersTurn - 1];


            //snap camera to player and bounds
            float orthoSize = mainCamera.orthographicSize;
            mainCamera.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, orthoSize - 1, 63 - orthoSize),
                Mathf.Clamp(player.transform.position.y, orthoSize - 63, 1 - orthoSize), mainCamera.transform.position.z);
            fsb.GetComponentInChildren<Text>().text = "Full Screen";

            gameLoop.players.playersList[gameLoop.playersTurn - 1].arrow.GetComponent<SpriteRenderer>().enabled = true;
            gameLoop.players.playersList[gameLoop.playersTurn - 1].spotLight.SetActive(true);
            gameLoop.showAvailableMoves();

            CullTiles();

            //stop coroutines to blink player and target ships
            StopCoroutine(coPlayer);
            StopCoroutine(coTarShip);

            //make sure all player sprites are on and not stopped during a off blink state
            foreach (var player in players.playersList)
            {
                player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
    }

    //coroutine to blink the players
    IEnumerator FlashPlayer()
    {
        for (int i = 0; i < 9999; ++i)
        {
            foreach (var player in players.playersList)
            {
                player.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }

            yield return new WaitForSeconds(offPlayerWaitTime);

            foreach (var player in players.playersList)
            {
                player.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }

            yield return new WaitForSeconds(onPlayerWaitTime);
        }
    }

    //coroutine to blink the targetships
    IEnumerator FlashTargetShips()
    {
        for (int i = 0; i < 9999; ++i)
        {
            foreach (var tarShip in gameLoop.targetShips.tarShipList)
            {
                tarShip.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }

            yield return new WaitForSeconds(offPlayerWaitTime);

            foreach (var tarShip in gameLoop.targetShips.tarShipList)
            {
                tarShip.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }

            yield return new WaitForSeconds(onPlayerWaitTime);
        }
    }

    //hide tiles that the camera doesnt see in order to massively increase performance
    public void CullTiles()
    {
        var camVertSize = Camera.main.orthographicSize * 2;
        var camPosX = Camera.main.transform.position.x - Camera.main.orthographicSize - 1;
        var camPosY = Mathf.Abs(Camera.main.transform.position.y + Camera.main.orthographicSize - 1);

        //not fullscreen
        if (!fullScreen)
        {
            for (var y = 0; y < tE.MapSize.y; y++)
            {
                for (var x = 0; x < tE.MapSize.x; x++)
                {
                    tE._tiles[x, y].SetActive(false);

                    if (x > Mathf.RoundToInt(camPosX) / 2 - 1 && x <= (Mathf.RoundToInt(camPosX) + camVertSize) / 2 + 1)
                    {
                        if (y >= Mathf.RoundToInt(camPosY) / 2 - 1 && y <= (Mathf.RoundToInt(camPosY) + camVertSize) / 2 + 1)
                        {
                            tE._tiles[x, y].SetActive(true);

                        }
                    }

                }

            }
        }
        //fullscreen
        else if(fullScreen)
        {
            for (var y = 0; y < tE.MapSize.y; y++)
            {
                for (var x = 0; x < tE.MapSize.x; x++)
                {
                    tE._tiles[x, y].SetActive(true);

                }

            }
        }


    }

    // Update is called once per frame
    void Update ()
    {
        //calculate screen ratio
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        var screenRatio = screenWidth / screenHeight;

        //adjust camera rectangle to fit 16:10 and 16:9 ratios
        if(screenRatio >= 1.7 && !screenSet) //16:9
        {
            mainCamera.rect = new Rect(.436f, mainCamera.rect.y, mainCamera.rect.width, mainCamera.rect.height);
            RectTransform gameBoarderRT = gameBoarder.GetComponent<RectTransform>();
            gameBoarderRT.offsetMin = new Vector2(345f, -1f);
            RectTransform sidebarBoarderRT = sidebarBoarder.GetComponent<RectTransform>();
            sidebarBoarderRT.offsetMax = new Vector2(-454f, 1f);
            RectTransform treasureMapRT = treasureMapBG.GetComponent<RectTransform>();
            treasureMapRT.offsetMax = new Vector2(-459f, 1f);
            screenSet = true;
        }
        else if(screenRatio >= 1.59 && screenRatio < 1.7 && !screenSet) //16:10
        {
            mainCamera.rect = new Rect(.375f, mainCamera.rect.y, mainCamera.rect.width, mainCamera.rect.height);
            RectTransform gameBoarderRT = gameBoarder.GetComponent<RectTransform>();
            gameBoarderRT.offsetMin = new Vector2(298f, -1f);
            RectTransform sidebarBoarderRT = sidebarBoarder.GetComponent<RectTransform>();
            sidebarBoarderRT.offsetMax = new Vector2(-500f, 1f);
            RectTransform treasureMapRT = treasureMapBG.GetComponent<RectTransform>();
            treasureMapRT.offsetMax = new Vector2(-505f, 1f);
            screenSet = true;
        }

        //pan the camera on touch drag
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //camera pan
            followPlayer = false;
            Vector2 touchDeltaPos = Input.GetTouch(0).deltaPosition;
            mainCamera.transform.Translate(-touchDeltaPos.x * panSpeed, -touchDeltaPos.y * panSpeed, 0);
            CullTiles();

            //camera pan bounds
            float orthoSize = mainCamera.orthographicSize;
            mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, orthoSize - 1, 63 - orthoSize),
                Mathf.Clamp(transform.position.y, orthoSize-63, 1-orthoSize), mainCamera.transform.position.z);
        }
        else if(Input.touchCount == 0)
        {
            followPlayer = true;
        }

        //fullscreen
        if(fullScreen && !followScreen)
        {
            transform.position = fullScreenCamera;
        }
        //player screen
        else if(followScreen && !fullScreen && !isZooming)
        {
            player = players.playersList[gameLoop.playersTurn - 1];
            targetPos = new Vector3(Mathf.Clamp(player.transform.position.x, mainCamera.orthographicSize - 1, 63 - mainCamera.orthographicSize),
                Mathf.Clamp(player.transform.position.y, mainCamera.orthographicSize - 63, 1 - mainCamera.orthographicSize),
                mainCamera.transform.position.z);

            if (followPlayer)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, 2.0f * Time.deltaTime);
                if(Vector3.Distance(mainCamera.transform.position, targetPos) >= 1f)
                {
                    CullTiles();
                }
            }

        }

        //pinch zooming for mobile device
        if(Input.touchCount == 2 && !fullScreen)//if there are two fingers touching
        {
            isZooming = true;
            //store the touches
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            //find the previous frame position of the touches
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            //find the distance between the touches in each frame
            float prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDelta = (touch0.position - touch1.position).magnitude;

            //find the diff in distances between frames
            float deltaDiff = prevTouchDelta - touchDelta;

            //change camera
            float resizeAmt = deltaDiff * pinchZoomSpeed;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize += resizeAmt, 5f, playerCameraSize);
            float orthoSize = mainCamera.orthographicSize;
            targetPos = new Vector3(Mathf.Clamp(player.transform.position.x, mainCamera.orthographicSize - 1, 63 - mainCamera.orthographicSize),
                Mathf.Clamp(player.transform.position.y, mainCamera.orthographicSize - 63, 1 - mainCamera.orthographicSize),
                mainCamera.transform.position.z);
            mainCamera.transform.position = new Vector3(Mathf.Clamp(targetPos.x,
                orthoSize - 1, 63 - orthoSize),
                Mathf.Clamp(targetPos.y,
                orthoSize - 63, 1 - orthoSize),
                mainCamera.transform.position.z);

            CullTiles();

        }
        else { isZooming = false; }


        //zoom with mouse scrolling
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && scrollOn && !fullScreen)
        {
            isZooming = true;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize -= .4f, 5f, playerCameraSize);

            //camera bounds
            targetPos = new Vector3(Mathf.Clamp(player.transform.position.x, mainCamera.orthographicSize - 1, 63 - mainCamera.orthographicSize),
                Mathf.Clamp(player.transform.position.y, mainCamera.orthographicSize - 63, 1 - mainCamera.orthographicSize),
                mainCamera.transform.position.z);
            float orthoSize = mainCamera.orthographicSize;
            mainCamera.transform.position = new Vector3(Mathf.Clamp(targetPos.x, orthoSize - 1, 63 - orthoSize),
                Mathf.Clamp(targetPos.y, orthoSize - 63, 1 - orthoSize), mainCamera.transform.position.z);

            CullTiles();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && scrollOn && !fullScreen)
        {
            isZooming = true;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize += .4f, 5f, playerCameraSize);

            //camera bounds
            targetPos = new Vector3(Mathf.Clamp(player.transform.position.x, mainCamera.orthographicSize - 1, 63 - mainCamera.orthographicSize),
                Mathf.Clamp(player.transform.position.y, mainCamera.orthographicSize - 63, 1 - mainCamera.orthographicSize),
                mainCamera.transform.position.z);
            float orthoSize = mainCamera.orthographicSize;
            mainCamera.transform.position = new Vector3(Mathf.Clamp(targetPos.x, orthoSize - 1, 63 - orthoSize),
                Mathf.Clamp(targetPos.y, orthoSize - 63, 1 - orthoSize), mainCamera.transform.position.z);

            CullTiles();

        }
        else { isZooming = false; }
    }
}

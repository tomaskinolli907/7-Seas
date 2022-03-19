using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera main;
    [SerializeField] private CinemachineVirtualCamera[] playerDefault;
    [SerializeField] private CinemachineVirtualCamera[] playerOverhead;
    [SerializeField] private CinemachineVirtualCamera diceAndCups;

    private static bool overhead = true;
    public static bool cups = false;
    static int playerNum;
    Vector3 currPos;

    public float touchSensitivity = 10f;
    public float minZoom = 30f;
    public float maxZoom = 80f;
    public float zoomSpeed = 0.05f;

    //GUI
    public bool GUI = false;
    public GameObject buttons;
    private bool left = false;
    private bool right = false;

    //CAMERA DUMMY
    public GameObject dummy;
    public GameObject[] ship;
    private int maxPlayers;

    void Start()
    {

        playerNum = MapLoad.camNum;

        playerOverhead[playerNum].Priority = 1;

        currPos = main.transform.position;

        CinemachineCore.GetInputAxis = this.GetCustomAxis;

        //GUI
        if (GUI)
        {
            buttons.SetActive(true);
        }
        else
        {
            buttons.SetActive(false);
        }

        dummy.transform.position = ship[playerNum].transform.position;

        maxPlayers = MapLoad.maxCams;
        Debug.Log(maxPlayers);
    }

    //Mostly Switching between cameras
    void Update()
    {
        if (playerNum != MapLoad.playerNum)
        {
            playerDefault[playerNum].Priority = 0;
            playerOverhead[playerNum].Priority = 0;

            playerNum = MapLoad.playerNum;

            playerDefault[playerNum].Priority = 0;
            playerOverhead[playerNum].Priority = 1;
        }

        currPos = main.transform.position;

        if (GUI == false)
        {
            Zoom(Input.GetAxis("Mouse ScrollWheel"));      //Zooming with mouse scrollwheel
        }
        
    }

    public void ChangeView()
    {
        if (overhead)
        {
            playerDefault[playerNum].Priority = 1;
            playerOverhead[playerNum].Priority = 0;

            overhead = false;
        }
        else
        {
            playerDefault[playerNum].Priority = 0;
            playerOverhead[playerNum].Priority = 1;

            overhead = true;
        }
    }

    public void SetDefaultView()
    {
        playerDefault[playerNum].Priority = 1;
        playerOverhead[playerNum].Priority = 0;

        overhead = false;
    }

    public void SetOverheadView()
    {
        playerDefault[playerNum].Priority = 0;
        playerOverhead[playerNum].Priority = 1;

        overhead = true;
    }

    public void SetDiceAndCups()
    {
        StartCoroutine(DiceAndCups());
    }

    public IEnumerator DiceAndCups()
    {
        diceAndCups.Priority = 2;

        yield return new WaitUntil(() => currPos == diceAndCups.transform.position);

        cups = true;

        yield return new WaitForSeconds(10);

        MapLoad.isRolling = false;
        MapLoad.diceSet = true;

        diceAndCups.Priority = 0;
    }

    //Manages Camera Movement
    public float GetCustomAxis(string axisName)
    {
        //Debug.Log(Input.touchCount);

        if (GUI == false)   //Touch Mode
        {
            if (axisName == "Custom")
            {
                if (Input.touchCount == 2)          //Two fingers for pinch zooming
                {
                    Debug.Log("Two Finger Touch");
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                    float difference = currentMagnitude - prevMagnitude;

                    Zoom(difference * zoomSpeed);
                }
                else if (Input.touchCount > 0)       //Mobile Touch
                {
                    if (overhead == false)
                    {
                        Debug.Log("One Finger Touch");
                        return Input.touches[0].deltaPosition.x / touchSensitivity;
                    }

                }
                else if (Input.GetMouseButton(0))    //Mouse Click
                {
                    if (overhead == false)
                    {
                        return UnityEngine.Input.GetAxis("Mouse X");
                    }
                }
            }
        }
        else if (GUI == true)   //Button Mode
        {
            if (left == true)
            {
                //return UnityEngine.Input.GetAxis("Mouse X") - 1;
            }
            else if (right == true)
            {
                //return UnityEngine.Input.GetAxis("Mouse X") + 1;
            }
        }

        
        
        return UnityEngine.Input.GetAxis(axisName);
    }
    
    public void LeftButton()
    {
        if (overhead == true)
        {
            Debug.Log("Left Button");

            if (dummy.transform.position.x - ship[playerNum].transform.position.x > -100)
            {
                dummy.transform.Translate(-10.0f, 0.0f, 0.0f);
            }
        }
    }
        

    public void RightButton()
    {
        if (overhead == true)
        {
            Debug.Log("Right Button");

            if (dummy.transform.position.x - ship[playerNum].transform.position.x < 100)
            {
                dummy.transform.Translate(10.0f, 0.0f, 0.0f);
            }
        }
    }

    public void UpButton()
    {
        if (overhead == true)
        {
            Debug.Log("Up Button");

            if (dummy.transform.position.z - ship[playerNum].transform.position.z < 100)
            {
                dummy.transform.Translate(0.0f, 0.0f, 10.0f);
            }
        }
        
        
    }

    public void DownButton()
    {
        if (overhead == true)
        {
            Debug.Log("Down Button");

            if (dummy.transform.position.z - ship[playerNum].transform.position.z > -100)
            {
                dummy.transform.Translate(0.0f, 0.0f, -10.0f);
            }
        }
        
    }

    public void ZoomIn()
    {
        Zoom(1.0f);
    }

    public void ZoomOut()
    {
        Zoom(-1.0f);
    }

    public void ResetDummy()
    {
        dummy.transform.position = ship[playerNum].transform.position;
    }
    
    public void UpdateDummy()
    {
        if (playerNum < maxPlayers - 1)
        {
            Debug.Log("Next Player: " + (playerNum + 1));
            dummy.transform.position = ship[playerNum + 1].transform.position;
            
        }
        else if (playerNum == maxPlayers - 1)
        {
            Debug.Log("Player 1");
            dummy.transform.position = ship[0].transform.position;
        }
        
    }
    
    //Camera zooming function
    void Zoom(float increment)
    {
        if (overhead)
        {
            playerOverhead[playerNum].m_Lens.FieldOfView = Mathf.Clamp(playerOverhead[playerNum].m_Lens.FieldOfView - (increment * 10), minZoom, maxZoom);
        }
        else
        {
            playerDefault[playerNum].m_Lens.FieldOfView = Mathf.Clamp(playerDefault[playerNum].m_Lens.FieldOfView - (increment * 10), minZoom, maxZoom);
        }
    }
}

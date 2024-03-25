using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject amAtMiddleOfRoom;

    public GameObject northExit;
    public GameObject southExit;
    public GameObject eastExit;
    public GameObject westExit;
    private float speed = 5.0f;
    private bool amMoving = false;

    private void turnOffExits()
    {
        this.northExit.gameObject.SetActive(false);
        this.southExit.gameObject.SetActive(false);
        this.eastExit.gameObject.SetActive(false);
        this.westExit.gameObject.SetActive(false);
    }

    private void turnOnExits()
    {
        this.northExit.gameObject.SetActive(false);
        this.southExit.gameObject.SetActive(false);
        this.eastExit.gameObject.SetActive(false);
        this.westExit.gameObject.SetActive(false);
    }



    // Start is called before the first frame update
    void Start()
    {
        this.turnOffExits();

        this.amAtMiddleOfRoom.SetActive(false);



        if (MySingleton.currentDirection.Equals("North"))
        {
            this.gameObject.transform.position = this.southExit.transform.position;
            this.gameObject.transform.LookAt(this.northExit.transform.position);
        }
        else if (MySingleton.currentDirection.Equals("South"))
        {
            this.gameObject.transform.position = this.northExit.transform.position;
            this.gameObject.transform.LookAt(this.southExit.transform.position);
        }
        else if (MySingleton.currentDirection.Equals("East"))
        {
            this.gameObject.transform.position = this.westExit.transform.position;
            this.gameObject.transform.LookAt(this.westExit.transform.position);
        }
        else if (MySingleton.currentDirection.Equals("West"))
        {
            this.gameObject.transform.position = this.eastExit.transform.position;
            this.gameObject.transform.LookAt(this.eastExit.transform.position);
        }
        else
        {
            this.amMoving = false;
            this.amAtMiddleOfTheRoom.SetActive(false);
            this.MiddleOfTheRoom.SetActive(false);
            this.gameObject.transform.position = this.MiddleOfTheRoom.transform.position;
        }

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("door"))
        {

            MySingleton.thePlayer.getCurrentRoom().removePlayer(MySingleton.currentDirection);
            EditorSceneManager.LoadScene("Room1");
        }
        else if (other.CompareTag("power-pellet"))
        {
            other.gameObject.SetActive(false);

            Room theCurrentRoom = MySingleton.thePlayer.getCurrentRoom();
            if(MySingleton.isThisTheFirstTimeInTheFirstRoom)
            {
                theCurrentRoom.removePellet(MySingleton.currentDirection);
                MySingleton.isThisTheFirstTimeInTheFirstRoom = false;

            }
            else
            {
                theCurrentRoom.removePellet(MySingleton.flipDirection(MySingleton.currentDirection));
            }
            
        }
        else if (other.CompareTag("MiddleOfTheRoom") && !MySingleton.currentDirection.Equals("?"))
        {
            this.MiddleOfTheRoom.SetActive(false);
            this.turnOnExits();
            print("at middle of the room");
            this.amAtMiddleOfTheRoom = true;
            this.amMoving = false;
            MySingleton.currentDirection = "middle";
        }



    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("north"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "north";
            this.gameObject.transform.LookAt(this.northExit.transform.position);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("south"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "south";
            this.gameObject.transform.LookAt(this.southExit.transform.position);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("west"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "west";
            this.gameObject.transform.LookAt(this.westExit.transform.position);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !this.amMoving && MySingleton.thePlayer.getCurrentRoom().hasExit("east"))
        {
            this.amMoving = true;
            this.turnOnExits();
            MySingleton.currentDirection = "east";
            this.gameObject.transform.LookAt(this.eastExit.transform.position);
        }

        if (MySingleton.currentDirection.Equals("north"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.northExit.transform.position, this.speed * Time.deltaTime);
        }

        if (MySingleton.currentDirection.Equals("south"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.southExit.transform.position, this.speed * Time.deltaTime);
        }

        if (MySingleton.currentDirection.Equals("west"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.westExit.transform.position, this.speed * Time.deltaTime);
        }
        if (MySingleton.currentDirection.Equals("east"))
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, this.eastExit.transform.position, this.speed * Time.deltaTime);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingoGameController : MonoBehaviour
{


    public Vector2 StartPosition = new Vector3(-2.4f, -11.8f);
    public Vector2 LastPos = new Vector3(-34.5f, -11.8f);
    public List<GameObject> Flamingos = new List<GameObject>();
    public float m_cameraSize;

    //private bool immortal = false;
    private FlamingoSoundPlayer m_soundManager;
    private PlayerBalanced m_balanced;
    private bool m_gameStarted = false;
    private bool m_playerReached = false;
    private bool game_finished = false;
    private bool m_stayOn = false;
    private CameraController m_cameraController;
    private GameObject m_player;
    private MovementGuider m_playerMovementGuider;
    private Animator m_playerAnimator;
    private Vector3 nextFlamingo;
    private Vector3 playerStartPosition;
 
    int counter = 0;
    // Use this for initialization
    void Start()
    {
        m_cameraController = Camera.main.GetComponent<CameraController>();
        m_soundManager = GetComponent<FlamingoSoundPlayer>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerAnimator = m_player.GetComponent<Animator>();
        m_playerMovementGuider = m_player.GetComponent<MovementGuider>();
       
        m_balanced = m_player.GetComponent<PlayerBalanced>();
       nextFlamingo = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_playerReached)
        {
            gameStart();
            m_playerReached = false;
        }
        if (game_finished)
        {

            m_player.GetComponent<MovementGuider>().SpecialState = MovementSpecialState.None;
            nextFlamingo = transform.position;
            game_finished = false;
        }
        if(m_gameStarted && Input.GetMouseButtonDown(0) && m_stayOn)
        {

            m_stayOn = false;
            if (counter == 0)
                {
              
                    var movementGuider = m_player.GetComponent<MovementGuider>();
                    movementGuider.SpecialState = MovementSpecialState.Bouncing;
                    nextFlamingo = Flamingos[counter].transform.position;
                    ++counter;
                    movementGuider.Destination = new Destination(nextFlamingo, FlamingoReached, null);
                }
                else
                {
                    //if(immortal)
                    //{
                    //PlayerWon();
                    //}
                    //else
                    if (m_balanced.IsBalanced)
                    {
                        m_balanced.setBalance(0);
                        PlayerWon();
                    }
                    else
                        PlayerLost();
                }
            }
        
    }

    private void OnMouseDown()
    {
        if (!m_gameStarted)
        {
            var movementGuider = m_player.GetComponent<MovementGuider>();

            movementGuider.Destination = new Destination(
                StartPosition,
                () =>
                {
                    m_gameStarted = true;
                    m_playerReached = true;
                    counter = 0;
                    m_playerMovementGuider.Locked = false;
                }
                );
            m_playerMovementGuider.Locked = true;
        }
    }

    private void gameStart()
    {
        var movementGuider = m_player.GetComponent<MovementGuider>();
        movementGuider.SpecialState = MovementSpecialState.None;
        m_gameStarted = true;
        playerStartPosition = m_player.transform.position;
        

        //
        m_cameraController.Lock(m_player, m_cameraSize);
        m_stayOn = true;
       

    }
    private void Scream()
    {

    }

    private void PlayerWon()
    {
        var movementGuider = m_player.GetComponent<MovementGuider>();
        if (counter == Flamingos.Count)
        {
            nextFlamingo = LastPos;
            m_gameStarted = false;
            m_playerReached = false;
            game_finished = true;

            counter = 0;
            movementGuider.Destination = new Destination(
                nextFlamingo,
                LastFlamingoReached,
                LastFlamingoReached,
                null);
        }
        else
        {
            nextFlamingo = Flamingos[counter].transform.position;
            ++counter;
            movementGuider.Destination = new Destination(nextFlamingo, FlamingoReached, null);
        }

        
    }

    private void FlamingoReached()
    {
        m_stayOn = true;
        m_soundManager.playCry();
        m_playerAnimator.Play("Balancing", -1, Random.Range(0f, 1f));
        m_playerAnimator.SetInteger("state", (int)NPCSTate.Balancing);
    }

    private void LastFlamingoReached()
    {
        m_cameraController.SetLimits(
                       -97f,
                       Flamingos[Flamingos.Count - 2].transform.position.x
                   );
        m_cameraController.Free();
    }

    private void PlayerLost()
    {
    
        m_gameStarted = false;
        m_playerReached = false;
        //immortal = true;
        nextFlamingo = transform.position;
        /*
        movementGuider.Destination = new Destination(StartPosition);
        */

        StartCoroutine("Falling");
    }

    private IEnumerator Falling()
    {
        if (counter > 0)
        {
            m_playerAnimator.SetInteger("state", (int)NPCSTate.Falling);
            m_playerMovementGuider.SpecialState = MovementSpecialState.Falling;
            yield return new WaitForSeconds(0.25f);
            m_soundManager.playFall();
            var dest = m_player.transform.position;
            dest.y -= 10;
            m_playerMovementGuider.Destination = new Destination(dest);
            yield return new WaitForSeconds(0.25f);
            m_player.transform.position = playerStartPosition;
            m_playerAnimator.SetInteger("state", (int)NPCSTate.Stop);
            m_playerMovementGuider.SpecialState = MovementSpecialState.None;
            m_playerMovementGuider.Destination = null;
            m_cameraController.Free(m_player.transform.position.x);
            counter = 0;
        }
    }
}

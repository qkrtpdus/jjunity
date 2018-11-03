using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{// 이렇게 적으면 0부터 4까지 자동적으로 다 적용 된거임
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK
}

public class FSMManager : MonoBehaviour
{

    public PlayerState currentState;
    public PlayerState startState;
    public Transform marker;

    Dictionary<PlayerState, PlayerFSMState> states = new Dictionary<PlayerState, PlayerFSMState>();

    //유니티에서 가장 먼저 스크립트를 실행 해주는게 바로 아래에 있는 코드임
    private void Awake()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;

        states.Add(PlayerState.IDLE,GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());


    }

    public void SetState(PlayerState newState)
    {
        foreach(PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }
        
        states[newState].enabled = true;
    }
    // Use this for initialization
    void Start()
    {
        SetState(startState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 1000))
            {
                marker.position = hit.point;

                SetState(PlayerState.RUN);
            }
        }
    }
}

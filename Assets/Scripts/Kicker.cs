using System.Collections;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    [SerializeField]
    private Ball ball;

    [SerializeField]
    private HPManager hpManager;

    [SerializeField]
    private ScoresManager scoresManager;

    [SerializeField]
    private GoalKeeper goalKeeper;

    [SerializeField]
    private KickState state;

    [SerializeField]
    private UIManager uiManager;

    private enum KickState
    {
        NullState,
        ChoosingHorizontal,
        ChoosingVertical,
        KickTime,
        EndGame
    }

    private bool moveLeft;
    private bool moveTop;
    private float speed = 2;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private Vector3 LeftPos
    {
        get { return new Vector3(-4, transform.position.y, -3); }
    }

    private Vector3 RightPos
    {
        get { return new Vector3(4, transform.position.y, -3); }
    }

    private Vector3 TopPos
    {
        get { return new Vector3(transform.position.x, 3, -3); }
    }

    private Vector3 DownPos
    {
        get { return new Vector3(transform.position.x, 0.7f, -3); }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateGameState();
        }
    }

    public void EndGame()
    {
        state = KickState.EndGame;
    }

    public void UpdateGameState()
    {
        switch (state)
        {
            case KickState.NullState:
                state = KickState.ChoosingHorizontal;
                StartCoroutine(ChoosingHorizontal());
                break;
            case KickState.ChoosingHorizontal:
                state = KickState.ChoosingVertical;
                StopAllCoroutines();
                StartCoroutine(ChoosingVertical());
                break;
            case KickState.ChoosingVertical:
                StopAllCoroutines();
                state = KickState.KickTime;
                goalKeeper.CatchBall(transform.position);
                ball.Kick(transform.position);
                break;
            case KickState.EndGame:
                return;
        }
    }

    public void Restore(bool goalHappen)
    {
        if (state != KickState.EndGame)
        {
            speed++;
            state = KickState.NullState;
            ball.Respawn();
            goalKeeper.Respawn();
            transform.position = startPos;
            UpdateGameState();
            UpdateGameScores(goalHappen);
        }
    }

    private void UpdateGameScores(bool goalHappen)
    {
        if (goalHappen)
        {
            scoresManager.AddScore();
        }
        else
        {
            hpManager.RestoreHp();
        }
    }

    IEnumerator ChoosingHorizontal()
    {
        while (true)
        {
            if (moveLeft)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    LeftPos,
                    Time.fixedDeltaTime * speed
                );
                yield return new WaitForFixedUpdate();
                if (transform.position == LeftPos)
                {
                    moveLeft = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    RightPos,
                    Time.fixedDeltaTime * speed
                );
                yield return new WaitForFixedUpdate();
                if (transform.position == RightPos)
                {
                    moveLeft = true;
                }
            }
        }
    }

    IEnumerator ChoosingVertical()
    {
        while (true)
        {
            if (moveTop)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    TopPos,
                    Time.fixedDeltaTime * speed
                );
                yield return new WaitForFixedUpdate();
                if (transform.position == TopPos)
                {
                    moveTop = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    DownPos,
                    Time.fixedDeltaTime * speed
                );
                yield return new WaitForFixedUpdate();
                if (transform.position == DownPos)
                {
                    moveTop = true;
                }
            }
        }
    }
}

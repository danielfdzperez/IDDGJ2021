using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    enum State { attack, preDefend,deffend, wait, waitAttack};
    [SerializeField]
    Dialog initialDialog;
    [SerializeField]
    Dialog gameOverDialog;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject battleMenu;
    [SerializeField]
    GameObject attackButton;
    [SerializeField]
    Transform stage1PlayerPos;
    [SerializeField]
    Transform defendPlayerPos;
    [SerializeField]
    Transform waitPlayerPos;
    [SerializeField]
    GameObject tutorialTexto;
    TMPro.TextMeshProUGUI textTut;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    SpriteRenderer bossSprite;

    [SerializeField]
    TextMeshProUGUI bossName;
    LogicInterface bossLogic;

    State currentSate = State.attack;
    Vector2 currentPlayerPosTarget;
    bool movePlayer = false;
    bool gameFinished = false;

    public UnityEvent OnEndGame;
    private void Awake()
    {
        EnableDisablePlayerMovement(false);

        EnemyTask taskConf = TaskManager.Instance.GetCurrentTask();
        bossSprite.sprite = taskConf.GetSprite(TimeManagement.Instance.isNight());
        bossName.text = taskConf.taskName;
        string[] sentence = { taskConf.text };
        initialDialog.SetSentences(sentence);
        string[] sentenceEnd = { taskConf.defeatedString };
        gameOverDialog.SetSentences(sentenceEnd);
        TimeManagement.Instance.AddAnHour();

        SoundManager.Instance.LoadMusic(TaskManager.Instance.GetCurrentTask().battleTheme);
        SoundManager.Instance.PlayMusic() ;
    }

    private void Start()
    {
 

        if (OnEndGame == null)
            OnEndGame = new UnityEvent();
        textTut = tutorialTexto.GetComponent<TextMeshProUGUI>();
        bossLogic = boss.GetComponent<Boss>().GetRandomLogic();
        initialDialog.Activate();
    }

    private void Update()
    {
        if (gameFinished)
            return;
        if (movePlayer)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, currentPlayerPosTarget, Time.deltaTime * 5);
            //Mathf.Approximately()
            if (Vector2.Distance(player.transform.position, currentPlayerPosTarget) <= 0.1)
            {
                if(currentSate == State.attack)
                {
                    tutorialTexto.SetActive(true);
                    textTut.text = "Pulsa Espacio";
                }
                if(currentSate == State.preDefend)
                {
                    ChangeState(State.deffend);
                }
                if(currentSate == State.wait)
                {
                    ChangeState(State.waitAttack);

                }

            }
        }
    }

    public void InitialDialogEnd()
    {
        battleMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(attackButton);
        //Mostrar interfaz de combate
    }

    public void Run()
    {
        GameOver();
    }

    public void Attack()
    {
        if (gameFinished)
            return;

        ChangeState(State.attack);
    }

    public void PlayerHit(HitType type)
    {
        if (gameFinished)
            return;
        tutorialTexto.SetActive(false);

        if (State.attack == currentSate)
        {
            ChangeState(State.preDefend);
        }
        else
        {

        }
    }

    void ChangeState(State newState)
    {
        if (gameFinished)
            return;
        currentSate = newState;
        switch(currentSate)
        {
            case State.preDefend:
                currentPlayerPosTarget = defendPlayerPos.position;
                movePlayer = true;
                break;
            case State.deffend:
                tutorialTexto.SetActive(true);
                textTut.text = "Defiendete";
                movePlayer = false;
                StartCoroutine(WaitUntilBossAttack());
                EnableDisablePlayerMovement(true);
                break;
            case State.wait:
                textTut.text = "";
                currentPlayerPosTarget = waitPlayerPos.position;
                movePlayer = true;
                EnableDisablePlayerMovement(false);
                break;
            case State.attack:
                battleMenu.SetActive(false);

                currentPlayerPosTarget = stage1PlayerPos.position;
                movePlayer = true;
                break;
            case State.waitAttack:
                battleMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(attackButton);
                break;
        }
    }

    void EnableDisablePlayerMovement(bool enable)
    {
        if (enable)
            player.GetComponent<MovementComponent>().EnableMovement();
        else
            player.GetComponent<MovementComponent>().StopMovement();
    }

    public void GameOver()
    {
        OnEndGame.Invoke();
        EnableDisablePlayerMovement(false);
        StopAllCoroutines();
        gameOverDialog.Activate();
        SoundManager.Instance.PlayHouseMusic();
        gameFinished = true;
    }

    public void Win()
    {
        OnEndGame.Invoke();
        EnableDisablePlayerMovement(false);
        StopAllCoroutines();

        EnemyTask taskConf = TaskManager.Instance.GetCurrentTask();
        string[] sentenceEnd = { taskConf.winText };
        gameOverDialog.SetSentences(sentenceEnd);
        gameOverDialog.Activate();
        taskConf.completed = true;
        SoundManager.Instance.PlayHouseMusic();
        GameManager.Instance.AddScore();
        gameFinished = true;
    }

    IEnumerator WaitUntilBossAttack()
    {
        yield return new WaitForSeconds(1);
        bossLogic.Activate(true);
        StartCoroutine(DefendTime());
    }

    IEnumerator DefendTime()
    {
        yield return new WaitForSeconds(3);
        bossLogic.Activate(false);
        yield return new WaitForSeconds(2);
        ChangeState(State.wait);
    }
}

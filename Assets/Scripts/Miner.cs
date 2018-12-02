using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miner : MonoBehaviour
{
    public State State;
    public float Speed;
    public float MiningDuration;
    public float IdleDuration;
    private GameObject miningObject;
    private GameObject depositPoint;
    void Start()
    {
        notWorkingTime = Time.time;
    }
    private float startedMining;
    private float notWorkingTime;
    // Update is called once per frame
    void Update()
    {
        if (State == State.Mining && MiningDuration <= (Time.time - startedMining))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            State = State.BackToCastle;
        }
        if (State == State.NotWorking && IdleDuration <= (Time.time - notWorkingTime))
            GoMine();
        if (State == State.Walking)
        {
            // The step size is equal to speed times frame time.
            float step = Speed * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, miningObject.transform.position, step);

            if (Mathf.Abs(Vector3.Distance(transform.position, miningObject.transform.position)) <= 2)
                Mine();
        }
        if (State == State.BackToCastle)
        {
            // The step size is equal to speed times frame time.
            float step = Speed * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, depositPoint.transform.position, step);
            if (Mathf.Abs(Vector3.Distance(transform.position,depositPoint.transform.position)) <= 2)
                Deposit();
        }

    }

    public static int currentIndex;
    void GoMine()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        lock (MapGrowth.ObjectsToMineFrom) { 
        currentIndex = currentIndex >= MapGrowth.ObjectsToMineFrom.Count ? 0 : currentIndex + 1;
        miningObject = MapGrowth.ObjectsToMineFrom[Mathf.Clamp(currentIndex, 0, MapGrowth.ObjectsToMineFrom.Count - 1)];
        }
        State = State.Walking;
    }
   void Mine()
    {
        State = State.Mining;
        startedMining = Time.time;
        GetComponent<Rigidbody>().isKinematic = true;
        var dep = GameObject.FindGameObjectsWithTag("Deposit");
        depositPoint = dep[Random.Range(0, dep.Length)];
    }

    void Deposit()
    {
        if (miningObject.GetComponent<ScoreManager>() == null)
            miningObject.transform.parent.gameObject.GetComponent<ScoreManager>().IncreaseScore(); // INCREASE SCORE
        else
            miningObject.GetComponent<ScoreManager>().IncreaseScore(); // INCREASE SCORE
        State = State.NotWorking;
        GetComponent<Rigidbody>().isKinematic = false;
        notWorkingTime = Time.time;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (miningObject != null && collision.gameObject.tag == miningObject.tag && State == State.Walking)
            Mine();
        if (collision.gameObject.tag == "Deposit" && State == State.BackToCastle)
             Deposit();
       
    }


}

public enum State
{
    NotWorking,
    Walking,
    Mining,
    BackToCastle,
    Desposit
}

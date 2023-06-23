using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngMonster : MonoBehaviour{
    static public MngMonster Instance;

    [SerializeField] private GameObject M_Skeleton;
    [SerializeField] private Transform TspawnPoint;
    public bool spawnOn = false;
    private int countMonster;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start(){
        DontDestroyOnLoad(this);
        countMonster = 0;
    }

    private void Update(){
        spwanMonster();
        Stopspawn();
    }

    public void spwanMonster(){
        if (spawnOn){
            GameObject obj = Instantiate(M_Skeleton, TspawnPoint);
            obj.transform.position = TspawnPoint.position;
            countMonster++;
        }
    }

    public void Stopspawn(){
        if(countMonster > 3){
            spawnOn = false;
        }
    }


}

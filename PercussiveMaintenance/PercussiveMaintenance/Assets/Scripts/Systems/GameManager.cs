using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ManagerStatus
{
    [HideInInspector]
    Loaded
}
public abstract class Manager : MonoBehaviour
{
    public ManagerStatus Status;
    public abstract void Init();
}

public class GameManager : MonoBehaviour
{


    private void Awake()
    {
       var managers = GetComponentsInChildren<Manager>();
        StartCoroutine(InitManagers(managers));
    }

    IEnumerator InitManagers(Manager[] managers)
    {
        foreach (var manager in managers)
        {
            manager.Init();
            while (manager.Status != ManagerStatus.Loaded)
            {
                yield return null;
                Debug.Log(manager + " is not loaded yet, make sure you put Status.Loaded at the end of its Init()");
            }
        }
    }
}

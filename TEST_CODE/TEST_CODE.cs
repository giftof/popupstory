using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MyEvent : UnityEvent<int> { }

public class TEST_CODE : MonoBehaviour
{
    public UnityAction<int> _unityAction;
    public MyEvent[] _event = new MyEvent[8];
    int state = 0;
    void Start()
    {
        //create unityaction delegate
        UnityAction<int>[] action = new UnityAction<int>[8];
        action[0] = new UnityAction<int>(FuncA);
        action[1] = new UnityAction<int>(FuncB);
        action[2] = new UnityAction<int>(FuncC);
        action[3] = new UnityAction<int>(FuncD);
        action[4] = new UnityAction<int>(FuncE);
        action[5] = new UnityAction<int>(FuncF);
        action[6] = new UnityAction<int>(FuncG);
        action[7] = new UnityAction<int>(FuncA);
        action[7] += new UnityAction<int>(FuncB);
        action[7] += new UnityAction<int>(FuncC);
        action[7] += new UnityAction<int>(FuncD);
        action[7] += new UnityAction<int>(FuncE);
        action[7] += new UnityAction<int>(FuncF);
        action[7] += new UnityAction<int>(FuncG);

        //register
        for (int i = 0; i < 8; i++)
        {
            _event[i] = new MyEvent();
            _event[i].AddListener(action[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _event[state].Invoke(100);
            state = (++state > 7) ? 0 : state;
        }
    }

    public void FuncA(int cnt) { Debug.Log("Air:" + cnt); }
    public void FuncB(int cnt) { Debug.Log("Baby:" + cnt); }
    public void FuncC(int cnt) { Debug.Log("Cat:" + cnt); }
    public void FuncD(int cnt) { Debug.Log("Do:" + cnt); }
    public void FuncE(int cnt) { Debug.Log("Ear:" + cnt); }
    public void FuncF(int cnt) { Debug.Log("Fly:" + cnt); }
    public void FuncG(int cnt) { Debug.Log("Good:" + cnt); }
}

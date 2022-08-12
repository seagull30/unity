using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveCtrl : MonoBehaviour
{
    public enum State
    {
        Hide_On,
        Hide_Off,
    }

    public State state = State.Hide_Off;
    private Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Hide_On: UpdateHideOn(); break;
            case State.Hide_Off: UpdateHideOff(); break;
        }
    }

    void UpdateHideOn()
    {
        float dissolveAmount = _material.GetFloat("_DissolveAmount");

        if(dissolveAmount<1f)
        {
            _material.SetFloat("_DissolveAmount", dissolveAmount + (0.5f * Time.deltaTime));
        }
        else
        {
            _material.SetFloat("_DissolveAmount", 1f);
        }

    }

    void UpdateHideOff()
    {
        float dissolveAmount = _material.GetFloat("_DissolveAmount");

        if (dissolveAmount > 0f)
        {
            _material.SetFloat("_DissolveAmount", dissolveAmount - (0.5f * Time.deltaTime));
        }
        else
        {
            _material.SetFloat("_DissolveAmount", 0f);
        }
    }

    public void ChangeState(State nextState)
    {
        state = nextState;
    }
}

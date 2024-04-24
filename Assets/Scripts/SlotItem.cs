using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    float _moveSpeed;
    float _timeToSpin;
    float _spinTime;
    public float positionToChange;
    int startPosition = 4;    
    float breakCoef = 1;
    bool _isMoving = false;
    bool _isBreaking = false;

    private void FixedUpdate()
    {
        if(transform.localPosition.y < positionToChange)
            transform.localPosition = new Vector3(0, startPosition, 0);

        if (_isMoving)
            transform.Translate(Vector3.down * _moveSpeed * breakCoef * Time.fixedDeltaTime);

        if (_isBreaking && breakCoef > 0)
            breakCoef -= 0.01f;

        if (breakCoef < 0)
            breakCoef = 0;
    }

    public void Spin(float moveSpeed, float timeToSpin, float spinTime)
    {
        _isMoving = false;
        _isBreaking = false;
        _moveSpeed = moveSpeed;
        _timeToSpin = timeToSpin;
        _spinTime = spinTime;
        breakCoef = 1;
        StartCoroutine(SetMoving());
        StartCoroutine(SetBreaking());
    }

    IEnumerator SetMoving()
    {
        yield return new WaitForSeconds(_timeToSpin);
        _isMoving = true;
    }
    IEnumerator SetBreaking()
    {
        yield return new WaitForSeconds(_spinTime);
        _isBreaking = true;
    }
}

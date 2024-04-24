using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public float finishPos;
    public float oldPosition;
    public SlotTakenPositionChecker slotItemPosChecker;
    public int myIndex; 
    float _moveSpeed;
    float _timeToSpin;
    float _spinTime;
    public float positionToChange;
    Vector3 _startPosition = new Vector3 (0, 4, 0);    
    public float breakCoef = 1f;
    bool _isMoving = false;
    bool _isBreaking = false;

    private void FixedUpdate()
    {
        if (_isMoving)
            transform.Translate(Vector3.down * _moveSpeed * breakCoef * Time.fixedDeltaTime);

        if (_isBreaking && breakCoef > 0)
            breakCoef -= 0.015f;

        if (transform.localPosition.y < positionToChange)
            transform.localPosition = _startPosition;

        if (breakCoef < 0)
        {
            oldPosition = gameObject.transform.localPosition.y;
            CalculateFinishPosition();
        }
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
        StopCoroutine(FinishElementPosition(null, Vector3.zero));
    }

    void CalculateFinishPosition()
    {
        breakCoef = 0;
        finishPos = slotItemPosChecker.CalculatePosition(myIndex, gameObject.transform);//Mathf.Round(transform.localPosition.y / 2) * 2;
        StartCoroutine(FinishElementPosition(transform, new Vector3(0,finishPos,0)));
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

    IEnumerator FinishElementPosition(Transform element, Vector3 targetPosition)
    {
        float moveSpeed = 10;
        while (Vector3.Distance(element.localPosition, targetPosition) > 0.01f)
        {
            element.localPosition = Vector3.Lerp(element.localPosition, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        element.localPosition = targetPosition; // убедимся, что элемент точно попадает в целевую позицию
    }
}

using UnityEngine;

public class SlotTakenPositionChecker
{
    float[] _slotsPositions;
    public void SetSlotsItemsCount(int count) => _slotsPositions = new float[count];
    
    public float CalculatePosition(int itemIndex, Transform itemTransform)
    {
        float roundPos = Mathf.Round(itemTransform.localPosition.y / 2) * 2;
        if (itemIndex <= 1)
        {
            _slotsPositions[itemIndex] = roundPos;
            return roundPos;
        }
        else if (_slotsPositions[itemIndex - 1] == roundPos)
        {
            _slotsPositions[itemIndex] = roundPos;
            return roundPos - 2;
        }
        else if (_slotsPositions[itemIndex - 1] == roundPos)
        {
            _slotsPositions[itemIndex] = roundPos;
            return roundPos + 2;
        }
        else
        {
            _slotsPositions[itemIndex] = roundPos;
            return roundPos;
        }
    }

}

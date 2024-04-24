using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] List<GameObject> slots = new List<GameObject>();
    [SerializeField] GameObject _slotPrefab;
    [SerializeField] Sprite[] _itemsSprites;
    public float timeToSpin;
    public float spinTime;
    [SerializeField, Range(5f, 10f)] float minSpinSpeed;
    [SerializeField, Range(10f, 20f)] float maxSpinSpeed;
    SlotItem[] slotItems;
    float _positionToChange = -2;
    [SerializeField] Button _spinButton;
    [SerializeField] bool _isLastSlot = false;

    private void Start()
    {
        CreateSlotItems();
    }

    void CreateSlotItems()
    {
        int position = 4; //first item position
        for (int i = 0; i < _itemsSprites.Length; i++)
        {
            GameObject SlotItem = Instantiate(_slotPrefab, transform.position, Quaternion.identity);
            SlotItem.transform.SetParent(gameObject.transform);
            SlotItem.SetActive(true);
            SlotItem.GetComponent<SpriteRenderer>().sprite = _itemsSprites[i];
            slots.Add(SlotItem);
        }

        slotItems = new SlotItem[slots.Count];
        SlotTakenPositionChecker slotTakenPositionChecker = new SlotTakenPositionChecker();
        slotTakenPositionChecker.SetSlotsItemsCount(slots.Count);
        float breackCoef = 1.5f;

        for (int i = 0; i < slots.Count; i++)
        {
            slotItems[i] = slots[i].GetComponent<SlotItem>();
            slotItems[i].myIndex = i;
            slotItems[i].breakCoef = breackCoef - 0.1f;
            slotItems[i].slotItemPosChecker = slotTakenPositionChecker;
            slots[i].transform.localScale = Vector3.one;
            position -= 2;
            slots[i].transform.localPosition = new Vector3(0, position, 0);
        }

        CalculateItemChangePosition();
    }

    public void Spin()
    {
        float SpinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);

        _spinButton.interactable = false;
        if (_isLastSlot)
            StartCoroutine(EnableButton());

        for (int i = 0; i < slots.Count; i++)
        {
            slotItems[i].Spin(SpinSpeed, timeToSpin, spinTime);
        }
    }

    void CalculateItemChangePosition()
    {
        if (slots.Count <= 3)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slotItems[i].positionToChange = -2;
            }
        }
        else
        {
            for (int i = 0; i < slots.Count - 3; i++)
            {
                _positionToChange -= 2;
            }
            for (int i = 0; i < slots.Count; i++)
            {
                slotItems[i].positionToChange = _positionToChange;
            }
        }
    }

    IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(spinTime + 0.5f);
        _spinButton.interactable = true;
    }
}

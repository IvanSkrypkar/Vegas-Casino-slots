using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] List<GameObject> slots = new List<GameObject>();
    [SerializeField] GameObject _slotPrefab;
    [SerializeField] Sprite[] _itemsSprites;
    public float timeToSpin;
    public float spinTime;
    [SerializeField, Range(1f,5f)] float minSpinSpeed;
    [SerializeField, Range(5f, 10f)] float maxSpinSpeed;
    SlotItem slotItem;
    float _positionToChange = -2;

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
            slotItem = SlotItem.GetComponent<SlotItem>();
            SlotItem.SetActive(true);
            SlotItem.GetComponent<SpriteRenderer>().sprite = _itemsSprites[i];
            slots.Add(SlotItem);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.localScale = Vector3.one;
            position -= 2;
            slots[i].transform.localPosition = new Vector3(0, position, 0);
        }

        CalculateItemChangePosition();
    }

    public void Spin()
    {
        float SpinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<SlotItem>().Spin(SpinSpeed, timeToSpin, spinTime);
        }
    }

    void CalculateItemChangePosition()
    {
        if (slots.Count <= 3)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slotItem = slots[i].GetComponent<SlotItem>();
                slotItem.positionToChange = -2;
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
                slotItem = slots[i].GetComponent<SlotItem>();
                slotItem.positionToChange = _positionToChange;
            }
        }
    }
}

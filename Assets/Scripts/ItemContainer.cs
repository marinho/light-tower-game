using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : Singleton<ItemContainer>
{
    [SerializeField] List<GameObject> items;
    [SerializeField] UnityEvent onAddItem;

    // Prevent non-singleton constructor use.
    protected ItemContainer() { }

    public void AddItem(GameObject item) {
        items.Add(item);
        onAddItem.Invoke();
    }

    public void RemoveItem(GameObject item) {
        items.Remove(item);
    }

    public bool HasItem(GameObject item) {
        return items.Contains(item);
    }

    public int CountItemsOfTag(string tag) {
        int result = 0;
        foreach (var item in items)
        {
            if (item.CompareTag(tag)) {
                result++;
            }
        }
        return result;
    }
}

using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public enum ItemType
    {
        PictureBook,
        Textbook,
        Key,
        Door,
        MovingWall
    }

    public static event Action<string> UseUIOn;
    public static event Action UseUIOff;
    public static event Action<ItemType, GameObject> UseItem;

    public GameObject rayObject;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(UnityEngine.Camera.main.transform.position, UnityEngine.Camera.main.transform.forward, out hit, 3))
        {
            rayObject = hit.transform.gameObject;
            if (!TryHandleRayTarget(rayObject))
                UseUIOff?.Invoke();
        }
        else
        {
            rayObject = null;
            UseUIOff?.Invoke();
        }
    }

    bool TryHandleRayTarget(GameObject obj)
    {
        if (obj == null) return false;

        string tag = obj.tag.ToLowerInvariant();

        return tag switch
        {
            "picturebook" => HandleInteraction(ItemType.PictureBook, obj, "E 읽기"),
            "textbook" => HandleInteraction(ItemType.Textbook, obj, "E 읽기"),
            "door" => HandleInteraction(ItemType.Door, obj, "E 읽기"),
            "key" => HandleInteraction(ItemType.Key, obj, "E 획득"),
            "movingwall" => HandleInteraction(ItemType.MovingWall, obj, "E 밀기"),
            _ => false
        };
    }

    bool HandleInteraction(ItemType type, GameObject obj, string message)
    {
        UseUIOn?.Invoke(message);
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem?.Invoke(type, obj);
        }
        return true;
    }

}

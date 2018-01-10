using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapIcon {
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class MinimapController : MonoBehaviour {

    public Transform playerPos;
    public Camera mapCamera;
    public static List<MapIcon> mapIcons = new List<MapIcon>();

    public static void RegisterMapIcon(GameObject o, Image i) {
        Image image = Instantiate(i);
        mapIcons.Add(new MapIcon() {owner = o, icon = image});
    }

    public static void RemoveMapIcons(GameObject o) {
        List<MapIcon> newList = new List<MapIcon>();
        for (int i = 0; i < mapIcons.Count; i++) {
            if (mapIcons[i].owner == o) {
                Destroy(mapIcons[i].icon);
                continue;
            }
            else {
                newList.Add(mapIcons[i]);
            }
            mapIcons.RemoveRange(0, mapIcons.Count);
            mapIcons.AddRange(newList);
        }

    }

    void DrawMapIcons() {
        foreach (MapIcon mi in mapIcons) {
            Vector3 screenPos = mapCamera.WorldToViewportPoint(mi.owner.transform.position);
            mi.icon.transform.SetParent(transform);

            RectTransform rt = GetComponent<RectTransform>();
            screenPos.x = screenPos.x * rt.rect.height;
            screenPos.y = screenPos.y * rt.rect.height;

            screenPos.z = 0;
            mi.icon.transform.position = screenPos;
        }
    }

    void Update() {
        DrawMapIcons();
    }
}


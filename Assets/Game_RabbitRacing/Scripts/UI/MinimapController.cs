using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class MapIcon {
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class MinimapController : MonoBehaviour {

    public Transform playerPos;
    public Camera mapCamera;
    //Icons on the minimap
    public static List<MapIcon> mapIcons = new List<MapIcon>();


    //register the icon and display it
    public static void RegisterMapIcon(GameObject o, Image i) {
        Image image = Instantiate(i);
        mapIcons.Add(new MapIcon() {owner = o, icon = image});
    }

    //Delete icons when Used, Destroyed or abducted by martians
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

    //Let's create a method to draw the Icons to the screen and call it in update. 
    void DrawMapIcons() {
        foreach (MapIcon mi in mapIcons) {
            //Map Icon Owner Position
            Vector2 mop = new Vector2(mi.owner.transform.position.x, mi.owner.transform.position.y);
            //Player Icon position
            Vector2 pp = new Vector2(playerPos.position.x, playerPos.position.y);

            //if it's 200 out hide the icon
            if (Vector2.Distance(mop, pp) > 200) {
                mi.icon.enabled = false;
                continue;
            }
            //Otehrwise show it
            else {
                mi.icon.enabled = true;
            }


            //Here we are converting our players position in the world to position on the map
            Vector3 screenPos = mapCamera.WorldToViewportPoint(mi.owner.transform.position);
            mi.icon.transform.SetParent(transform);

            //Let's make it accurate. Grab the corners, Clamp height and width. 
            RectTransform rt = GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            screenPos.x = Mathf.Clamp(screenPos.x * rt.rect.height + corners[0].x, corners[0].x,corners[2].x);
            screenPos.y = Mathf.Clamp(screenPos.y * rt.rect.height + corners[0].y, corners[0].y,corners[1].y);

            screenPos.z = 0;
            mi.icon.transform.position = screenPos;
        }
    }

    void Update() {
        DrawMapIcons();
    }
}


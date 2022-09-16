using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnTexture : MonoBehaviour
{
    public Texture2D baseTexture;


    // Update is called once per frame
    void Update()
    {
        DoMouseDrawing();
    }

    /// <summary>
    /// 通过鼠标在材质上绘画
    /// </summary>
    /// <exception cref="Exception"></exception>

    private void DoMouseDrawing()
    {
        if (Camera.main == null)
        {
            throw new Exception("Can't find main camera");
        }

        //检查是否有鼠标点击
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            return;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        // 如果没有击中任何物体就返回
        if (!Physics.Raycast(mouseRay, out hit))
            return;
        // 如果没有击中就返回
        if (hit.collider.transform != transform)
            return;

        //活动击中的纹理坐标
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= baseTexture.width;
        pixelUV.y *= baseTexture.height;

        //当左键点击时着白色，右键点击时着黑色
        Color colorToSet = Input.GetMouseButton(0) ? Color.white : Color.black;

        baseTexture.SetPixel((int)pixelUV.x, (int)pixelUV.y, colorToSet);
        baseTexture.Apply();
    }

}

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
    /// ͨ������ڲ����ϻ滭
    /// </summary>
    /// <exception cref="Exception"></exception>

    private void DoMouseDrawing()
    {
        if (Camera.main == null)
        {
            throw new Exception("Can't find main camera");
        }

        //����Ƿ��������
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            return;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        // ���û�л����κ�����ͷ���
        if (!Physics.Raycast(mouseRay, out hit))
            return;
        // ���û�л��оͷ���
        if (hit.collider.transform != transform)
            return;

        //����е���������
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= baseTexture.width;
        pixelUV.y *= baseTexture.height;

        //��������ʱ�Ű�ɫ���Ҽ����ʱ�ź�ɫ
        Color colorToSet = Input.GetMouseButton(0) ? Color.white : Color.black;

        baseTexture.SetPixel((int)pixelUV.x, (int)pixelUV.y, colorToSet);
        baseTexture.Apply();
    }

}

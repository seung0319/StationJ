using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class SavePhoto : MonoBehaviour
{
    [SerializeField] Image photoView;
    [SerializeField] GameObject saveMessage;
    [SerializeField] string sceneName;

    Texture2D savephotoTexture;
    private void Start()
    {
        GetImageAndShow();
        saveMessage.SetActive(false);
    }

    public void GetImageAndShow()
    {
        string imagePath = Path.Combine(Application.persistentDataPath, "ImageName.png");
        //���� ����ҿ� ����� �̹��� ���� ��θ� �����´�.

        //������ �����ϴ��� Ȯ��
        if(File.Exists(imagePath))
        {
            //������ ����Ʈ �迭�� �о�´�.
            byte[] imaheytes = File.ReadAllBytes(imagePath);

            //����Ʈ �迭�� Texture2D�� ��ȯ
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imaheytes);

            //Texture2D�� Sprite�� ��ȯ
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            //Image ������Ʈ�� Sprite �� ����
            photoView.sprite = sprite;
        }
    }

    public void SaveToGalleryBtn()
    {
        //�� �ؽ��Ŀ� ������ ��µ� �̹����� �ؽ��ĸ� �����Ѵ�.
        Texture2D savephotoTexture = photoView.sprite.texture;

        //�ٹ��̸�, �����̸� ����
        string albumName = "StationJ";
        string fileName = DateTime.Now.ToString("yyMMdd-HH-mm-ss");

        //NativeGallery �� ����� �������� ����
        NativeGallert




    }
}

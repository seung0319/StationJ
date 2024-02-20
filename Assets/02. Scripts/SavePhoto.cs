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
        //내부 저장소에 저장된 이미지 파일 경로를 가져온다.

        //파일이 존재하는지 확인
        if(File.Exists(imagePath))
        {
            //파일을 바이트 배열로 읽어온다.
            byte[] imaheytes = File.ReadAllBytes(imagePath);

            //바이트 배열을 Texture2D로 변환
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imaheytes);

            //Texture2D를 Sprite로 변환
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            //Image 컴포넌트에 Sprite 를 설정
            photoView.sprite = sprite;
        }
    }

    public void SaveToGalleryBtn()
    {
        //빈 텍스쳐에 사진이 출력된 이미지의 텍스쳐를 저장한다.
        Texture2D savephotoTexture = photoView.sprite.texture;

        //앨범이름, 파일이름 설정
        string albumName = "StationJ";
        string fileName = DateTime.Now.ToString("yyMMdd-HH-mm-ss");

        //NativeGallery 를 사용해 갤러리에 저장
        NativeGallert




    }
}

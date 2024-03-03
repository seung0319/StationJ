using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 몇M후 좌회전 우회전 할때 M 계산 하는 클래스
/// </summary>
public class RemainingDistance : MonoBehaviour
{
    Transform player;
    [SerializeField]Text metersText;

    private void Update()
    {
        //플레이어 위치를 참조받아 거리계산
        if (player != null)
        {
            float distance = Vector3.Distance(player.position,transform.position);
            metersText.text = $"{(int)distance}M 후";
        }
    }

    //플레이어 참조를 전달 받아 초기화 하는 함수
    public void PlayerTransformSet(Transform player)
    {
        this.player = player;
    }
}

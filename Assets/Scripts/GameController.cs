using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform wallPrefabs; //벽 프리팹
    [SerializeField]
    private Transform leftWalls; //왼쪽 벽들의 부모 Transform
    [SerializeField]
    private Transform rightWalls; //오른쪽 벽들의 부모 Transform

    [SerializeField]
    private int currentLevel = 1;

    private readonly float wallMaxScaleY = 20;

    //레벨에 따른 벽 개수 [레벨-1] = 벽 개수
    private readonly int[] wallCountByLevel = new int[7] {1,2,3,4,5,6,7};

    private void Awake()
    {
        SpawnWalls();
    }

    void SpawnWalls()
    {
        int numberOfWalls = wallCountByLevel[currentLevel-1];

        int currentWallCount = leftWalls.childCount; //현재 벽 개수 확인

        //추가로 필요한 개수만큼 벽 생성 (현재 벽의 개수가 현재 레벨에 필요한 벽 개수보다 적으면 벽 추가 생성)
        if(currentWallCount < numberOfWalls)
        {
            for (int i = 0; i < numberOfWalls - currentWallCount; i++)
            {
                Instantiate(wallPrefabs, leftWalls); //왼쪽 벽 생성

                Instantiate(wallPrefabs, rightWalls); //오른쪽 벽 생성
            }
        }

        //모든 벽 위치, 크기 설정
        for(int i = 0;i < numberOfWalls;i++)
        {
            //벽 크기 설정 (y)
            Vector3 scale = new Vector3(0.5f,1,1);
            scale.y = wallMaxScaleY / numberOfWalls; //number:1,scale.y:20 , number:2,scale.y:10
            //벽 위치 설정 (y)
            Vector3 position = Vector3.zero;
            position.y = scale.y * (numberOfWalls / 2-i) - (numberOfWalls % 2 == 0 ? scale.y / 2 : 0);
            //왼,오른쪽 벽 위치/크기 설정
            SetTransform(leftWalls.GetChild(i), position,scale);
            SetTransform(rightWalls.GetChild(i), position,scale);
        }
    }

    private void SetTransform(Transform t, Vector3 position, Vector3 scale)
    {
        t.localPosition = position;
        t.localScale = scale;
    }
}

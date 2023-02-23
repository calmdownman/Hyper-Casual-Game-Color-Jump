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

        for(int i = 0;i < numberOfWalls;i++)
        {

        }
    }
}

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
    private int currentLevel = 1; //현재 레벨(레벨에 따라 벽 개수 변경)
    private int maxLevel = 7; //최대 레벨
    private int currentScore = 0; //현재 점수
    

    [SerializeField]
    private List<Color32> colors; //벽의 색상 목록

    [SerializeField]
    private Player player; //플레이어 컴포넌트 정보

    private readonly float wallMaxScaleY = 20; //벽 최대 y 크기

    //레벨에 따른 벽 개수 [레벨-1] = 벽 개수
    private readonly int[] wallCountByLevel = new int[7] {1,2,3,4,5,6,7};
    //레벨업에 필요한 점수 [레벨-1] = 필요 점수
    private readonly int[] needLevelUpScroe = new int[7] {1,2,4,8,16,32,64};

    private void Awake()
    {
        SpawnWalls();
        SetColors();
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
            position.y = scale.y * (numberOfWalls / 2 - i) - (numberOfWalls % 2 == 0 ? scale.y / 2 : 0);
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

    private void SetColors()
    {
        var tempColors = new List<Color32>();

        //현재 선택 가능한 모든 색상(colors)에서 wallCountByLevel[currnetLevel-1] 개수만큼 임의의 색상을 뽑아낸다
        int[] indexs = Utils.RandomNumerics(colors.Count, wallCountByLevel[currentLevel - 1]);
        for (int i =0;i < indexs.Length;i++)
        {
            tempColors.Add(colors[indexs[i]]);
        }

        int colorCount = tempColors.Count;

        //왼쪽 벽 색상 설정
        int[] leftWallIndexs = Utils.RandomNumerics(colorCount, colorCount);
        for (int i =0; i < leftWalls.childCount; i++)
        {
            leftWalls.GetChild(i).GetComponent<SpriteRenderer>().color = tempColors[leftWallIndexs[i]];
        }
        //오른쪽 벽 색상 설정
        int[] rightWallIndexs = Utils.RandomNumerics(colorCount, colorCount);
        for (int i = 0; i < rightWalls.childCount; i++)
        {
            rightWalls.GetChild(i).GetComponent<SpriteRenderer>().color = tempColors[rightWallIndexs[i]];
        }
        //임의의 색상 배열 중 임의의 색상을 선택해 플레이어의 색상으로 설정
        int index = Random.Range(0, tempColors.Count);
        player.GetComponent<SpriteRenderer>().color = tempColors[index];
    }

    //플레이어가 벽과 충돌했을 때 처리
    public void CollisionWithWall()
    {
        //현재 점수 증가
        currentScore++;

        //아직 레벨 업이 가능하고, 현재 점수가 다음 레벨에 필요한 점수보다 높으면
        if (currentLevel < maxLevel && needLevelUpScroe[currentLevel] < currentScore)
        {
            //현재 레벨 증가
            currentLevel++;
            //벽 추가
            SpawnWalls();
        }

        //벽, 플레이어 색상 설정
        SetColors();
    }
       
    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}

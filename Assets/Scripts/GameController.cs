using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform wallPrefabs; //�� ������
    [SerializeField]
    private Transform leftWalls; //���� ������ �θ� Transform
    [SerializeField]
    private Transform rightWalls; //������ ������ �θ� Transform

    [SerializeField]
    private int currentLevel = 1;

    //������ ���� �� ���� [����-1] = �� ����
    private readonly int[] wallCountByLevel = new int[7] {1,2,3,4,5,6,7};

    private void Awake()
    {
        SpawnWalls();
    }

    void SpawnWalls()
    {
        int numberOfWalls = wallCountByLevel[currentLevel-1];

        int currentWallCount = leftWalls.childCount; //���� �� ���� Ȯ��

        //�߰��� �ʿ��� ������ŭ �� ���� (���� ���� ������ ���� ������ �ʿ��� �� �������� ������ �� �߰� ����)
        if(currentWallCount < numberOfWalls)
        {
            for (int i = 0; i < numberOfWalls - currentWallCount; i++)
            {
                Instantiate(wallPrefabs, leftWalls); //���� �� ����

                Instantiate(wallPrefabs, rightWalls); //������ �� ����
            }
        }

        for(int i = 0;i < numberOfWalls;i++)
        {

        }
    }
}

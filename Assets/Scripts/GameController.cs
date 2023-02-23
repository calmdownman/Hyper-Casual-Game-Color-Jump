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

    private readonly float wallMaxScaleY = 20;

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

        //��� �� ��ġ, ũ�� ����
        for(int i = 0;i < numberOfWalls;i++)
        {
            //�� ũ�� ���� (y)
            Vector3 scale = new Vector3(0.5f,1,1);
            scale.y = wallMaxScaleY / numberOfWalls; //number:1,scale.y:20 , number:2,scale.y:10
            //�� ��ġ ���� (y)
            Vector3 position = Vector3.zero;
            position.y = scale.y * (numberOfWalls / 2-i) - (numberOfWalls % 2 == 0 ? scale.y / 2 : 0);
            //��,������ �� ��ġ/ũ�� ����
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

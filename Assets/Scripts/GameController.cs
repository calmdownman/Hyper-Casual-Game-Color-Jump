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
    private int currentLevel = 1; //���� ����(������ ���� �� ���� ����)
    private int maxLevel = 7; //�ִ� ����
    private int currentScore = 0; //���� ����
    

    [SerializeField]
    private List<Color32> colors; //���� ���� ���

    [SerializeField]
    private Player player; //�÷��̾� ������Ʈ ����

    private readonly float wallMaxScaleY = 20; //�� �ִ� y ũ��

    //������ ���� �� ���� [����-1] = �� ����
    private readonly int[] wallCountByLevel = new int[7] {1,2,3,4,5,6,7};
    //�������� �ʿ��� ���� [����-1] = �ʿ� ����
    private readonly int[] needLevelUpScroe = new int[7] {1,2,4,8,16,32,64};

    private void Awake()
    {
        SpawnWalls();
        SetColors();
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
            position.y = scale.y * (numberOfWalls / 2 - i) - (numberOfWalls % 2 == 0 ? scale.y / 2 : 0);
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

    private void SetColors()
    {
        var tempColors = new List<Color32>();

        //���� ���� ������ ��� ����(colors)���� wallCountByLevel[currnetLevel-1] ������ŭ ������ ������ �̾Ƴ���
        int[] indexs = Utils.RandomNumerics(colors.Count, wallCountByLevel[currentLevel - 1]);
        for (int i =0;i < indexs.Length;i++)
        {
            tempColors.Add(colors[indexs[i]]);
        }

        int colorCount = tempColors.Count;

        //���� �� ���� ����
        int[] leftWallIndexs = Utils.RandomNumerics(colorCount, colorCount);
        for (int i =0; i < leftWalls.childCount; i++)
        {
            leftWalls.GetChild(i).GetComponent<SpriteRenderer>().color = tempColors[leftWallIndexs[i]];
        }
        //������ �� ���� ����
        int[] rightWallIndexs = Utils.RandomNumerics(colorCount, colorCount);
        for (int i = 0; i < rightWalls.childCount; i++)
        {
            rightWalls.GetChild(i).GetComponent<SpriteRenderer>().color = tempColors[rightWallIndexs[i]];
        }
        //������ ���� �迭 �� ������ ������ ������ �÷��̾��� �������� ����
        int index = Random.Range(0, tempColors.Count);
        player.GetComponent<SpriteRenderer>().color = tempColors[index];
    }

    //�÷��̾ ���� �浹���� �� ó��
    public void CollisionWithWall()
    {
        //���� ���� ����
        currentScore++;

        //���� ���� ���� �����ϰ�, ���� ������ ���� ������ �ʿ��� �������� ������
        if (currentLevel < maxLevel && needLevelUpScroe[currentLevel] < currentScore)
        {
            //���� ���� ����
            currentLevel++;
            //�� �߰�
            SpawnWalls();
        }

        //��, �÷��̾� ���� ����
        SetColors();
    }
       
    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}

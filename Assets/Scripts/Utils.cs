using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Utils : MonoBehaviour
{
   public static int[] RandomNumerics(int maxCount, int n)
    {
        //0~maxCount������ ���� �� ��ġ�� �ʴ� n���� ������ �ʿ��� �� ���
        int[] defaults = new int[maxCount]; //0~maxCount���� ������� �����ϴ� �迭
        int[] results = new int[n]; //��� ������ �����ϴ� �迭

        //�迭 ��ü�� 0���� maxCount�� ���� ������� ����
        for (int i =0; i < maxCount; ++i)
        {
            defaults[i] = i;
        }

        for (int i = 0; i < n; ++i)
        {
            int index = Random.Range(0, maxCount); //������ ���ڸ� �ϳ� �̾Ƽ�

            results[i] = defaults[index];
            defaults[index] = defaults[maxCount - 1];

            maxCount--;
        }
        return results;
    }
}

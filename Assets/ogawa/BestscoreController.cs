using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BestscoreController : MonoBehaviour
{
    //1�ʂ̃p�l������ꂽ�z��
    public GameObject[] rankingColumns;
    //Eclipse�̃T�[�u���b�g�ɐڑ�����servletConnector�N���X
    public ServletConnector servletConnector;
    public IEnumerator GetRankingData()
    {
        Debug.Log("1�yBestscoreController�zGetRankingData()���s�J�n");
        //�T�[�o�[(�T�[�u���b�g)�ɐڑ��������L���O�f�[�^���擾
        // yield return���g���āAservletConnector.ServletGet()�̏�������������܂őҋ@
        yield return StartCoroutine(servletConnector.ServletGet());
        //�����L���O�f�[�^���擾�ł����ꍇ�̂݁A�����L���O�p�l�����X�V
        if (!servletConnector.IsError)
        {
            UpdateRankingTexts();
        }
    }
    void UpdateRankingTexts()
    {
        Debug.Log("3�yBestscoreController�zUpdateRankingTexts()���s�J�n");
        //servletConnector���f�[�^�x�[�X����擾�������X�g���擾
        List<Record> recordList = servletConnector.Ranking.recordList;
        //���X�g�̗v�f���� �J��Ԃ�
        for (int i = 0; i < recordList.Count; i++)
        {
            //�e�����N�̃p�l���� �q�I�u�W�F�N�g��TextMeshProUGUI�R���|�[�l���g���擾
            TextMeshProUGUI[] tmps = rankingColumns[i].GetComponentsInChildren<TextMeshProUGUI>();
            // tmps[0] �� ScoreTMP
            //Score��Text�̒l��ς���
            tmps[0].text = recordList[i].score.ToString();
        }
    }
    public IEnumerator InsertNewRecord_and_GetRankingData()
    {
        Debug.Log("5�yBestscoreController�zInsertNewRecord_and_GetRankingData()���s�J�n");
        // yield return���g���āAservletConnector.ServletPost()�̏�������������܂őҋ@
        yield return StartCoroutine(servletConnector.ServletPost());
        //�V�L�^�𖳎��o�^�ł����ꍇ�̂݁A�V���������L���O�f�[�^���擾����
        if (!servletConnector.IsError)
        {
            StartCoroutine(GetRankingData());
        }
    }
}
using System.Collections;
using System.Collections.Generic;
//JSON�`���Ƃ̕ϊ����\�ɂ��邽�߂�[System.Serializable]���L�q
[System.Serializable]
public class BestScore
{
    public List<Record> recordList;
    public BestScore(List<Record> list)
    {
        recordList = list;
    }
}
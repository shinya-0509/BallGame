using System.Collections;
using System.Collections.Generic;
//JSON�`���Ƃ̕ϊ����\�ɂ��邽�߂�[System.Serializable]���L�q
[System.Serializable]
public class Ranking
{
    public List<Record> recordList;
    public Ranking(List<Record> list)
    {
        recordList = list;
    }
}
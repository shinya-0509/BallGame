using System.Collections;
using System.Collections.Generic;
//JSON形式との変換を可能にするために[System.Serializable]を記述
[System.Serializable]
public class Ranking
{
    public List<Record> recordList;
    public Ranking(List<Record> list)
    {
        recordList = list;
    }
}
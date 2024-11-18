using System.Collections.Generic;
//JSON形式との変換を可能にするために[System.Serializable]を記述
[System.Serializable]
public class Record
{
    public int score;
    public Record(int score)
    {
        this.score = score;
    }
}
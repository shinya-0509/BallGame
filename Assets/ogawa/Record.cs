using System.Collections.Generic;
//JSON�`���Ƃ̕ϊ����\�ɂ��邽�߂�[System.Serializable]���L�q
[System.Serializable]
public class Record
{
    public int score;
    public Record(int score)
    {
        this.score = score;
    }
}
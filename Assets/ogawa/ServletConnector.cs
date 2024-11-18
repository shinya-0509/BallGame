using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ServletConnector : MonoBehaviour
{
    //�ڑ��󋵂�\������TMPro
    //public TextMeshProUGUI connectionInfoTMP;
    [SerializeField] GameDirector gameDirector;

    //���IWeb�A�v���P�[�V������URL
    const string ROOTPATH = "http://localhost:8080/teamb_online/";

    //�T�[�u���b�g����̃��X�|���X�̕����񂪓���ϐ�
    string responseText;

    //�ڑ��G���[���N���������ǂ����̃t���O
    bool isError;

    //JSON�����񂩂�C#�I�u�W�F�N�g�ɕϊ������|�C���g�����L���O������
    Ranking ranking;

    //�v���p�e�B
    public bool IsError
    {
        get { return isError; }
    }
    public Ranking Ranking
    {
        get { return ranking; }
    }

    //Get���\�b�h(�����L���O�f�[�^�̎擾)
    public IEnumerator ServletGet()
    {
        Debug.Log("2-1�yServletConnector�zServletGet()���s�J�n");

        //�ϐ��̏�����
        responseText = "";
        isError = false;
        ranking = null;

        //UnityWebRequest�N���X��Web�T�[�o�[�Ƃ̒ʐM�Ɏg�p�����

        //���̃N���X�̃I�u�W�F�N�g�͕�������ʂ������̂ŁA
        //�g�p�ςɂȂ����玩���I�ɔj�������悤��using�X�e�[�g�����g���g��
        //UnityWebRequest.Get���\�b�h(������ �ʐM���URI)�ŁA
        //�T�[�o�[(�T�[�u���b�g)�֑��M������e��ݒ肷��
        using (UnityWebRequest request = UnityWebRequest.Get(ROOTPATH + "GetRankingServlet"))
        {
            //request.SendWebRequest()�ŃT�[�u���b�g�Ƀ��N�G�X�g���M������A
            //���X�|���X���Ԃ��Ă���܂őҋ@
            yield return request.SendWebRequest();

            //�T�[�u���b�g�ɃA�N�Z�X����������request.result��Success�ɕς��B
            //���s������ConnectionError�ɕς��

            //�A�N�Z�X�����̏ꍇ�A�T�[�u���b�g���̏����̐��ۂł���ɕ��򂷂�
            if (request.result == UnityWebRequest.Result.Success)
            {
                //�T�[�u���b�g����Ԃ��Ă�����������擾
                responseText = request.downloadHandler.text;

                //������null�łȂ������ꍇ(�܂�T�[�u���b�g���̏��������������ꍇ)
                if (responseText != "null")
                {
                    isError = false;
                    Debug.Log("2-2�yServletConnector�zServletGet() : �擾����JSON�`���̕�����: " + responseText);

                    //�T�[�u���b�g����Ԃ��Ă���������(JSON)��C#�̃I�u�W�F�N�g(Ranking�^)�ɕϊ�
                    ranking = JsonUtility.FromJson<Ranking>(responseText);
                }
                else//�T�[�u���b�g���̏��������s�����ꍇ
                {
                    isError = true;

                    Debug.Log("2-2�yServletConnector�zServletGet() : �����L���O�f�[�^�̎擾�Ɏ��s���܂���");

                    //�G���[�������������Ƃ��Q�[����ʂɕ\��
                    //connectionInfoTMP.text = "Database Connection Error";
                }
            }
            else //���������T�[�u���b�g�ɃA�N�Z�X�ł��Ȃ������ꍇ
            {
                isError = true;

                //request.responseCode�˃G���[�̎�ނ��ԍ��ŕ\�����
                //0�̏ꍇ�˃T�[�o�[���N�����Ă��Ȃ��A�������̓T�[�o�[�̃A�h���X���Ԉ���Ă���
                //404�̏ꍇ�˃T�[�o�[�ɐڑ��ł��Ă��邪�A
                //���IWeb�v���W�F�N�g���������̓T�[�u���b�g�����Ԉ���Ă���
                //request.error�˃G���[�̐������������Ă���
                Debug.Log("2-2�yServletConnector�zServletGet() : responseCode = "
                + request.responseCode + " / " + request.error);

                //�G���[�������������Ƃ��Q�[����ʂɕ\��
                //connectionInfoTMP.text = "Server Connection Error";
            }
        }
    }
    //Post���\�b�h(�V�L�^�̑��M)
    public IEnumerator ServletPost()
    {
        Debug.Log("6-1�yServletConnector�zServletPost()���s�J�n");

        //�ϐ��̏�����
        responseText = "";
        isError = false;
        ranking = null;

        //Web�T�[�o�[�ɑ��M����t�H�[���f�[�^�𐶐�����N���X
        WWWForm form = new WWWForm();
        Debug.Log("score" + this.gameDirector.Point);
        //���肽���p�����[�^���t�H�[���f�[�^�ɓ����
        form.AddField("score", this.gameDirector.Point);
        //UnityWebRequest�N���X��Web�T�[�o�[�Ƃ̒ʐM�Ɏg�p�����
        //���̃N���X�̃I�u�W�F�N�g�͕�������ʂ������̂ŁA
        //�g�p�ςɂȂ����玩���I�ɔj�������悤��using�X�e�[�g�����g���g��
        //UnityWebRequest.Post���\�b�h(������ �ʐM���URI �� ���M����t�H�[���f�[�^)�ŁA
        //�T�[�o�[(�T�[�u���b�g)�֑��M������e��ݒ肷��
        using (UnityWebRequest request = UnityWebRequest.Post(ROOTPATH + "InsertRecordServlet", form))
        {

            //SendWebRequest()�Ŏ��s���f�[�^���M�J�n
            //�T�[�o�[(�T�[�u���b�g)���烌�X�|���X���Ԃ��Ă���܂Ŏ��Ԃ�������̂�
            //yield return�Ń��X�|���X���Ԃ��Ă���܂Ŏ��̃R�[�h���s��҂�
            yield return request.SendWebRequest();

            //�T�[�u���b�g�ɃA�N�Z�X����������request.result��Success�ɕς��B
            //���s������ConnectionError�ɕς��
            //�T�[�u���b�g�ɃA�N�Z�X�ł����ꍇ�A�T�[�u���b�g���̏����̐��ۂ�\��
            if (request.result == UnityWebRequest.Result.Success)
            {
                //�T�[�u���b�g����Ԃ��Ă�����������擾
                responseText = request.downloadHandler.text;
                //�T�[�u���b�g���̏��������������ꍇ
                if (responseText == "SUCCESS")
                {
                    isError = false;

                    Debug.Log("6-2�yServletConnector�zServletPost(): ���R�[�h�������݊���");

                    //�f�[�^�x�[�X�ɓo�^�������Ƃ��Q�[����ʂɕ\��
                    //connectionInfoTMP.text += "has been registered";
                }
                else //�T�[�u���b�g���̏��������s�����ꍇ
                {
                    isError = true;

                    Debug.Log("6-2�yServletConnector�zServletPost(): ���R�[�h�������݃G���[");

                    //�G���[�������������Ƃ��Q�[����ʂɕ\��
                    //connectionInfoTMP.text += "Database Connection Error";
                }
            }
            else //���������T�[�u���b�g�ɃA�N�Z�X�ł��Ȃ������ꍇ
            {
                isError = true;

                //request.responseCode�˃G���[�̎�ނ��ԍ��ŕ\�����
                //0�̏ꍇ�˃T�[�o�[���N�����Ă��Ȃ��A�������̓T�[�o�[�̃A�h���X���Ԉ���Ă���
                //404�̏ꍇ�˃T�[�o�[�ɐڑ��ł��Ă��邪�A
                //���IWeb�v���W�F�N�g���������̓T�[�u���b�g�����Ԉ���Ă���
                //request.error�˃G���[�̐������������Ă���
                Debug.Log("6-2�yServletConnector�zServletPost() : responseCode = "
                + request.responseCode + " / " + request.error);

                //�G���[�������������Ƃ��Q�[����ʂɕ\��
                //connectionInfoTMP.text += "Server Connection Error";
            }
        }
    }
}
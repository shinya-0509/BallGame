using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    // �Q�[���I�u�W�F�N�g���擾
    public GameObject BallPrefab;
    Rigidbody rb;
    public bool born;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        // Spawn��1�b�Ԋu�Ŏ��s����
        InvokeRepeating("Spawn", 1f, 1f);
    }
    // Enemy�𐶐�
    private void Spawn()
    {

        if (born)
        {
            GameObject item = Instantiate(BallPrefab);
            item.transform.position = transform.position;// �����������I�u�W�F�N�g
        }


    }
}
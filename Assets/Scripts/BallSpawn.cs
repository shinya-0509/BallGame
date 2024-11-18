using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    // ゲームオブジェクトを取得
    public GameObject BallPrefab;
    Rigidbody rb;
    public bool born;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        // Spawnを1秒間隔で実行する
        InvokeRepeating("Spawn", 1f, 1f);
    }
    // Enemyを生成
    private void Spawn()
    {

        if (born)
        {
            GameObject item = Instantiate(BallPrefab);
            item.transform.position = transform.position;// 生成したいオブジェクト
        }


    }
}
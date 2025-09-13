using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Build.Reporting;
#endif
using UnityEngine;

public class Lao8 : MonoBehaviour
{
    public GameUI gameUI;

    public GameAudio gameAudio;

    Vector3 direction = Vector3.right;
    public Transform bodyPrefab;
    public List<Transform> bodies = new List<Transform>();

    private bool pendingAddBody = false;
    private List<Vector3> positions = new List<Vector3>();
    private int step = 2; // 身体间隔更小
    private float moveInterval = 0.08f; // 方向键更灵敏
    private float moveTimer = 0f;


    void Start()
    {
        bodies.Add(this.transform);
        positions.Add(transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            direction = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            direction = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
            direction = Vector3.right;
        }
    }

    private void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f;

            // 记录蛇头当前位置
            positions.Add(transform.position);

            // 移动蛇头
            transform.position += direction;

            // 移动身体段
            for (int i = 1; i < bodies.Count; i++)
            {
                int posIndex = positions.Count - 1 - i * step;
                if (posIndex >= 0)
                {
                    bodies[i].position = positions[posIndex];
                }
                else
                {
                    bodies[i].position = positions[0];
                }
            }

            // 控制队列长度
            int maxLength = bodies.Count * step + step;
            if (positions.Count > maxLength)
            {
                positions.RemoveRange(0, positions.Count - maxLength);
            }

            // 添加新身体段
            if (pendingAddBody)
            {
                Vector3 initPos = positions.Count > bodies.Count * step
                    ? positions[positions.Count - bodies.Count * step]
                    : positions[0];
                bodies.Add(Instantiate(bodyPrefab, initPos, Quaternion.identity));
                pendingAddBody = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("apple"))
        {
            pendingAddBody = true;

            gameUI.AddScore();
        }

        if (collision.CompareTag("obstacle"))
        {
            Debug.Log("Game Over");
            transform.position = Vector3.zero;
            direction = Vector3.right;
            for (int i = 1; i < bodies.Count; i++)
            {
                Destroy(bodies[i].gameObject);
            }
            bodies.Clear();
            bodies.Add(transform);
            positions.Clear();
            positions.Add(transform.position);

            gameUI.ResetScore();

            gameAudio.ReplayBackgroundMusic();
        }

        for (int i = 1; i < bodies.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, bodies[i].position);
            if (distance < 0.1f)
            {
                Debug.Log("Game Over");
            }
        }
    }
}

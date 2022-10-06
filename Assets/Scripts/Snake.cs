using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;
    public int score = 0;

    private void Start()
    {
        // Sets to 10 fps (This will be difficulty later)
        Time.fixedDeltaTime = 0.1f;
        score = 0;
        Debug.Log(Time.fixedDeltaTime);

        ResetState();
    }

    // Update every Frame
    private void Update()
    {
        /* 
         * User Input 
         */

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }

        switch (score)
        {
            case 5: 
                Time.fixedDeltaTime = 0.09f;
                Debug.Log(Time.fixedDeltaTime);
                break;
            case 10:
                Time.fixedDeltaTime = 0.08f;
                Debug.Log(Time.fixedDeltaTime);
                break;
            case 15:
                Time.fixedDeltaTime = 0.07f;
                Debug.Log(Time.fixedDeltaTime);
                break;
            case 20:
                Time.fixedDeltaTime = 0.06f;
                Debug.Log(Time.fixedDeltaTime);
                break;
            case 25:
                Time.fixedDeltaTime = 0.05f;
                Debug.Log(Time.fixedDeltaTime);
                break;
            default:
                break;
        }

    }

    // Update at a fixed rate
    private void FixedUpdate()
    {
        /*
         * Segment position update
         */

        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        /* 
         * Physics 
         */

        // Movement is based on Vector2 _direction. (Rounding is not strictly nessecary but is good to maintain the feel of the grid)
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x + _direction.x), 
            Mathf.Round(this.transform.position.y) + _direction.y, 
            0.0f
            );
    }

    private void Grow()
    {
        // Spawn a segment
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState()
    {
        for(int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for(int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        score = 0;
        Time.fixedDeltaTime = 0.1f;
        this.transform.position = Vector3.zero;

        Debug.Log(Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we hit food, grow
        if (collision.tag == "Food")
        {
            Grow();
            score++;
        }
        else if(collision.tag == "Obstacle")
        {
            ScoreManager.instance.ResetPoint();
            ResetState();
        }
    }
}

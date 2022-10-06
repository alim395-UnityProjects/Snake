using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        randomizePosition();
    }

    private void randomizePosition()
    {
        /*
         * Get the size of gridArea object to form spawn area
        */

        Bounds bounds = gridArea.bounds;
        
        // Generate random x,y coordiantes within the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Spawn the food in the generated x,y position
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ScoreManager.instance.AddPoint();
            randomizePosition();
        }
    }
}

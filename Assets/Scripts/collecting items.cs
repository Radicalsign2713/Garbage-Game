using UnityEngine;

public class Mover2D : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
       
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        
      
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AbsorbObstacle();
        }
    }

    private void AbsorbObstacle()
    {
       
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            Destroy(hit.collider.gameObject); 
        }
    }
}

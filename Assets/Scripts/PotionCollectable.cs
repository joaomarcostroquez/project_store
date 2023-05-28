using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotionCollectable : MonoBehaviour
{
    //[SerializeField] private NextLevel nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NextLevel nextLevel = collision.gameObject.GetComponent<NextLevel>();

            nextLevel.potionsLeft -= 1;

            if(nextLevel.potionsLeft <= 0)
            {
                SceneManager.LoadScene(0);
            }

            Destroy(gameObject);
        }
    }
}

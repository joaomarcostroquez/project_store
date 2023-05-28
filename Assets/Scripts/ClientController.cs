using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClientController : MonoBehaviour
{
    [SerializeField] private Sprite[] clientSprites;

    [SerializeField] private float time = 180f;
    [SerializeField] private TextMeshProUGUI txTimer;

    private float timer;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // I know this is bad, but no time bro :/
        //float rand = Random.Range(0, 3);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = clientSprites[0];

        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= time / 4)
        {
            if (spriteRenderer.sprite != clientSprites[2])
            {
                spriteRenderer.sprite = clientSprites[2];
            }
        } else if (timer <= time / 2)
        {
            if(spriteRenderer.sprite != clientSprites[1])
            {
                spriteRenderer.sprite = clientSprites[1];
            }
        }

        if(timer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(Mathf.FloorToInt(timer % 60) >= 10)
        {
            txTimer.text = (Mathf.FloorToInt(timer / 60) + ":" + Mathf.FloorToInt(timer % 60));
        }
        else
        {
            txTimer.text = (Mathf.FloorToInt(timer / 60) + ":0" + Mathf.FloorToInt(timer % 60));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

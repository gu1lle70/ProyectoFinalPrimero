using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    DASH dash;
    public  SpriteRenderer spriteRenderer;
    public GameObject ghostPrefab;
    public float delay = 1.0f;
    float delta = 0;
    public float destroyTime = 0.1f;
    public Color color;
    public Material material = null;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            createGhost();
        }
    }
    public void createGhost()
    {
        GameObject ghostObj = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghostObj.transform.localScale = PlayerMove.Instance.transform.localScale;
        Destroy(ghostObj, destroyTime);

        SpriteRenderer ghostSpriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        ghostSpriteRenderer.sprite = PlayerSprites.Instance.spriteRenderer.sprite;
        ghostSpriteRenderer.color = color;
        if (material != null)
        {
            ghostSpriteRenderer.material = material;
        }

        ghostSpriteRenderer.flipX = PlayerSprites.Instance.spriteRenderer.flipX;
    }
}

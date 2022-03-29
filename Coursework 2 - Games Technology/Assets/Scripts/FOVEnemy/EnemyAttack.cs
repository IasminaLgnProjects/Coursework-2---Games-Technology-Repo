using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    FieldOfViewEnemy fovScript;
    SpriteRenderer sRenderer;

    Sprite defaultSprite;
    [SerializeField] Sprite newSprite;
    // Start is called before the first frame update
    void Start()
    {
        fovScript = gameObject.GetComponent<FieldOfViewEnemy>();
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        defaultSprite = sRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(fovScript.CanSeePlayer)
        {
            SpriteChange(newSprite);
        }
        else
        {
            SpriteChange(defaultSprite);
        }
    }

    void SpriteChange(Sprite sprite)
    {
        sRenderer.sprite = sprite;
    }
}

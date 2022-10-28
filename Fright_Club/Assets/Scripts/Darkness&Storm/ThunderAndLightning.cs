using System.Collections;
using System.Threading;
using UnityEngine;

public class ThunderAndLightning : MonoBehaviour
{
    public AudioClip[] thunderSounds;
    public AudioSource source;
    public int minLightningGap;
    public int maxLightningGap;


    // Start is called before the first frame update
    void Start()
    {
        //Start the thunder storm
        StartCoroutine(nameof(PlayThunderAndLightningWithDelay));
    }

    IEnumerator PlayThunderAndLightningWithDelay()
    {
        while (true)
        {
            float delay = Random.Range(minLightningGap, maxLightningGap);
            yield return new WaitForSeconds(delay);

            var spriteRendererComponent = GetComponent<SpriteRenderer>();
            var flashlightMeshRenderer = GameObject.Find("Flashlight").GetComponent<MeshRenderer>();

            //store old info
            Color originalColor = spriteRendererComponent.color;
            int originalSortingOrder = spriteRendererComponent.sortingOrder;
            var originalMaskInteraction = spriteRendererComponent.maskInteraction;

            //loop between 2 and 3 times
            int loopCount = Random.Range(2, 3);

            for (int i = 0; i < loopCount; i++)
            {
                //flash white
                spriteRendererComponent.color = Color.white;
                //make sure that the flash is on top of everything
                spriteRendererComponent.sortingOrder = 5;
                spriteRendererComponent.maskInteraction = SpriteMaskInteraction.None;

                //clear flashlight
                flashlightMeshRenderer.enabled = false;

                yield return new WaitForSeconds(0.06f);

                //set transparent for flash of lightning
                spriteRendererComponent.color = new Color(0, 0, 0, 0);
                spriteRendererComponent.sortingOrder = originalSortingOrder;
                spriteRendererComponent.maskInteraction = originalMaskInteraction;

                //wait for 0.1 seconds
                yield return new WaitForSeconds(0.15f);

                //set the color back to darkness
                spriteRendererComponent.color = originalColor;

                //change the flashlight normal
                flashlightMeshRenderer.enabled = true;
            }

            //delay between light and sound
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

            //Play thunder sound
            source.clip = thunderSounds[Random.Range(0, thunderSounds.Length)];
            source.PlayOneShot(source.clip);
        }
    }
}
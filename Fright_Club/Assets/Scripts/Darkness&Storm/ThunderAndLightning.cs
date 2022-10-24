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
            
           var component = GetComponent<SpriteRenderer>();
           
           //store old info
            Color oldColor = component.color;
            int originalSortingOrder = component.sortingOrder;
            var oldMaskInteraction =component.maskInteraction;
        
            //loop between 2 and 3 times
            int loopCount = Random.Range(2, 3);

            for (int i = 0; i < loopCount; i++)
            {
                //flash white
                component.color = Color.white;
                //make sure that the flash is on top of everything
                component.sortingOrder = 5;
                component.maskInteraction = SpriteMaskInteraction.None;
                yield return new WaitForSeconds(0.06f);

                //set transparent for flash of lightning
                component.color = new Color(0, 0, 0, 0);
                component.sortingOrder = originalSortingOrder;
                component.maskInteraction = oldMaskInteraction;

                //wait for 0.1 seconds
                yield return new WaitForSeconds(0.15f);

                //set the color back to darkness
                component.color = oldColor;
            }
        
            //delay between light and sound
            yield return new WaitForSeconds( Random.Range(0.5f, 1.5f));
        
            //Play thunder sound
            source.clip = thunderSounds[Random.Range(0, thunderSounds.Length)];
            source.PlayOneShot(source.clip);
        }
    }
}
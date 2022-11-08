using System.Collections;
using System.Threading;
using UnityEngine;

public class RandomBackGroundSoundsTrigger : MonoBehaviour
{
    public AudioClip[] randomSounds;
    public AudioSource source;
    public int minSoundGap;
    public int maxSoundGap;


    // Start is called before the first frame update
    void Start()
    {
        //Start the thunder storm
        StartCoroutine(nameof(PlayRandomSoundWithDelay));
    }

    IEnumerator PlayRandomSoundWithDelay()
    {
        while (true)
        {
            float delay = Random.Range(minSoundGap, maxSoundGap);
            yield return new WaitForSeconds(delay);

            //Play random sound
            source.clip = randomSounds[Random.Range(0, randomSounds.Length)];
            source.PlayOneShot(source.clip);
           
        }
    }
}
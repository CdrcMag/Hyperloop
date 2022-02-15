using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedNotes : MonoBehaviour
{

    public Manager_Score mScore;

    public GameObject exploPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fall")
        {
            FailedNote();
            GameObject i = Instantiate(exploPrefab, collision.transform.position, Quaternion.identity);
            Health.Instance.RemoveHealth(1);
            Manager_Audio.Instance.PlayFx(1);
            CameraShake.Instance.Shake(0.2f, 1);
            Destroy(i, 5f);
        }
            
    }

    private void FailedNote()
    {
        mScore.ResetMultiplier();
        mScore.ResetInRow();
    }



}

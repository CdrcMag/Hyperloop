using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSystem : MonoBehaviour
{
    public static TransitionSystem Instance;

    public Transform curtain;

    public bool OpenOnStart;
    public bool CloseOnStart;

    public float speed;

    private void Awake()
    {
        Instance = this;

        if (OpenOnStart) OpenCurtain();
        if (CloseOnStart) CloseCurtain();
    }

    public void OpenCurtain() => StartCoroutine(IOpenCurtain());
    public void CloseCurtain() => StartCoroutine(ICloseCurtain());

    IEnumerator IOpenCurtain()
    {
        //Permet l'agrandissement et active le sprite
        bool opening = true;
        curtain.gameObject.SetActive(true);

        //Set la position au centre et la scale à 0
        curtain.localPosition = new Vector2(0, 0);
        curtain.localScale = new Vector2(0, 0);


        while (opening)
        {
            //Augmente la taille à la vitesse openingSpeed
            curtain.localScale = new Vector3(curtain.localScale.x + speed, curtain.localScale.y + speed);

            //Une fois a x >= 23, sort de la boucle
            if (curtain.localScale.x >= 1.1f)
            {
                opening = false;
            }

            //curtain.gameObject.SetActive(false);

            yield return null;
        }
    }

    IEnumerator ICloseCurtain()
    {
        //Permet l'agrandissement et active le sprite
        bool closing = true;
        curtain.gameObject.SetActive(true);

        //Set la position au centre et la scale à 0
        curtain.localPosition = new Vector2(0, 0);
        curtain.localScale = new Vector2(1, 1);


        while (closing)
        {
            //Augmente la taille à la vitesse openingSpeed
            curtain.localScale = new Vector3(curtain.localScale.x - speed, curtain.localScale.y - speed);

            //Une fois a x >= 23, sort de la boucle
            if (curtain.localScale.x <= 0)
            {
                closing = false;
            }

            

            yield return null;
        }

        curtain.gameObject.SetActive(false);
    }

  
}

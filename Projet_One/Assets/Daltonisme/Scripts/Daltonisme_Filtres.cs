using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
[RequireComponent(typeof(Volume))]
public class Daltonisme_Filtres : MonoBehaviour
{
    enum Type { Neutre, Protanopie, Protanomalie, Deuteranopie, Deuteranomalie, Tritanopie, Tritanomaly, Achromatopsie }

    //Type de vision souhait�
    [SerializeField] private Type TypeDeVision = Type.Neutre;
    
    //Type de vision actuel
    Type TypeDeVisionActuel;

    //Profils utilis�s avec le volume pour appliquer les filtres
    VolumeProfile[] filtres;

    //Volume utilis� pour appliquer les filtres
    Volume postProcessVolume;


    void Start()
    {
        //Assigne la vision choisie au lancement
        TypeDeVisionActuel = TypeDeVision;

        //Initialisation du volume
        InitVolume();

        //Chargement des filtres
        Chargement_Filtres();

        //Change le filtre actuel avec le filtre choisi
        Changer_Filtre();
        
    }

    void Update()
    {
        //Si le mode de vision choisi ne correspond pas au mode de vision choisi => Change le filtre
        if (TypeDeVision != TypeDeVisionActuel)
        {
            TypeDeVisionActuel = TypeDeVision;
            Changer_Filtre();
        }
    }

    /// <summary>
    /// R�cup�ration du post processing volume
    /// </summary>
    void InitVolume()
    {
        postProcessVolume = GetComponent<Volume>();
        postProcessVolume.isGlobal = true;
    }

    /// <summary>
    /// M�thode de chargement des filtres.
    /// Les filtres doivent �tre dans un dossier nomm� "Resources" et doivent contenir le mot "Filtre".
    /// </summary>
    public void Chargement_Filtres()
    {
        Object[] profileObjects = Resources.LoadAll("", typeof(VolumeProfile));
        filtres = new VolumeProfile[profileObjects.Length];

        for (int i = 0; i < profileObjects.Length; i++)
        {
            if (profileObjects[i].name.Contains("Filtre"))
            {
                filtres[i] = (VolumeProfile)profileObjects[i];
            }
        }
    }

    /// <summary>
    /// Assigne le filtre choisi au post processing volume
    /// </summary>
    void Changer_Filtre()
    {
        postProcessVolume.profile = filtres[(int)TypeDeVisionActuel];
    }


}

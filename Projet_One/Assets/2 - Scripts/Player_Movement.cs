using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Movement : MonoBehaviour
{
    //public TextMeshProUGUI t;

    [Header("== General settings ===")]
    public float speed;
    public GameObject trail;
    public Follower follower;
    public Manager_Score mScore;
    public GameObject explosionPrefab;
    public GameObject explosionPrefabColored;
    public List<Transform> basePositions;
    public GameObject movingParticles;
    public Scene_Manager_1 scene_manager;
    public GameObject c_light;

    private bool moving = false;

    private Vector2[,] positions = new Vector2[3,3];

    public int currentPositionX = 1;
    public int currentPositionY = 1;

    private void Awake()
    {
        //positions[0,0] = new Vector2(-1.5f, -2+1.5f);//A1
        //positions[0,1] = new Vector2(0, -2 + 1.5f);//A2
        //positions[0,2] = new Vector2(1.5f, -2 + 1.5f);//A3
        //positions[1,0] = new Vector2(-1.5f, -3 + 1.5f);//B1
        //positions[1,1] = new Vector2(0, -3 + 1.5f);//B2
        //positions[1,2] = new Vector2(1.5f, -3 + 1.5f);//B3
        //positions[2,0] = new Vector2(-1.5f, -4 + 1.5f);//C1
        //positions[2,1] = new Vector2(0, -4 + 1.5f);//C2
        //positions[2,2] = new Vector2(1.5f, -4 + 1.5f);//C3

        c_light.SetActive(false);
        StartCoroutine(TempoLight());

 
        positions[0, 0] = basePositions[0].position;
        positions[0, 1] = basePositions[1].position;
        positions[0, 2] = basePositions[2].position;
        positions[1, 0] = basePositions[3].position;
        positions[1, 1] = basePositions[4].position;
        positions[1, 2] = basePositions[5].position;
        positions[2, 0] = basePositions[6].position;
        positions[2, 1] = basePositions[7].position;
        positions[2, 2] = basePositions[8].position;

        transform.position = positions[currentPositionY, currentPositionX];

    }

    private void Update()
    {
        if (moving)
            return;

//If the game runs in the editor
#if UNITY_EDITOR
        HandleKeyboardInputs();
#endif

//If the game runs on mobile
#if UNITY_ANDROID
        HandleMobileInputs();
#endif

        //t.text = direction.ToString();

    }

    private Vector2 startPos;
    private Vector2 direction;
    private bool allowMove = true;
    private float time = 0;
    public float timer = .5f;

    private void HandleMobileInputs()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    allowMove = true;
                    break;

                case TouchPhase.Ended:
                    allowMove = false;
                    break;
            }
        }

        direction.Normalize();

        if (allowMove && !moving)
        {
            time += Time.deltaTime;
            if (time >= timer)
            {

                //Gauche 
                if (direction.x < 0 && direction.y > -0.5f && direction.y < 0.5f)
                {
                    currentPositionX -= 1;
                    if (currentPositionX == -1)
                    {
                        currentPositionX = 2;
                        StartCoroutine(IMoveToOtherSide(-4));
                    }
                    else
                    {
                        StartCoroutine(IMove());
                    }
                }

                //Droite
                if (direction.x > 0 && direction.y > -0.5f && direction.y < 0.5f)
                {
                    currentPositionX += 1;
                    if (currentPositionX == 3)
                    {
                        currentPositionX = 0;
                        StartCoroutine(IMoveToOtherSide(4));
                    }
                    else
                    {
                        StartCoroutine(IMove());
                    }
                }

                //Bas
                if (direction.y < 0 && direction.x > -0.5f && direction.x < 0.5f)
                {
                    if(currentPositionY < 2)
                    {
                        currentPositionY += 1;
                        StartCoroutine(IMove());
                    }
                    
                }

                //Haut
                if (direction.y > 0 && direction.x > -0.5f && direction.x < 0.5f)
                {
                    if(currentPositionY > 0)
                    {
                        currentPositionY -= 1;
                        StartCoroutine(IMove());
                    }
                    
                }

                direction.x = 0;
                direction.y = 0;

                time = 0;
            }
        }
    }

    private void HandleKeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentPositionY > 0)
        {
            SpawnMovingParticles(Sens.Haut);
            currentPositionY -= 1;
            StartCoroutine(IMove());
        }
        if (Input.GetKeyDown(KeyCode.S) && currentPositionY < 2)
        {
            SpawnMovingParticles(Sens.Bas);
            currentPositionY += 1;
            StartCoroutine(IMove());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnMovingParticles(Sens.Gauche);

            currentPositionX -= 1;
            if (currentPositionX == -1)
            {
                currentPositionX = 2;
                StartCoroutine(IMoveToOtherSide(-4));
            }
            else
            {
                StartCoroutine(IMove());
            }

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnMovingParticles(Sens.Droite);

            currentPositionX += 1;
            if (currentPositionX == 3)
            {
                currentPositionX = 0;
                StartCoroutine(IMoveToOtherSide(4));
            }
            else
            {
                StartCoroutine(IMove());
            }

        }
    }

    IEnumerator IMove()
    {
        moving = true;

        Vector2 target = positions[currentPositionY, currentPositionX];

        while (Vector2.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;

        moving = false;
    }

    IEnumerator IMoveToOtherSide(int sens)
    {
        moving = true;

        float x = positions[currentPositionY, currentPositionX].x;
        float y = positions[currentPositionY, currentPositionX].y;

        Vector2 target1 = new Vector2(x+sens, y);
        Vector2 target2 = new Vector2(x-sens, y);
        Vector2 finalTarget = positions[currentPositionY, currentPositionX];

        bool a = true;

        trail.SetActive(false);
        while (a)
        {
            transform.position = Vector2.MoveTowards(transform.position, target1, speed * 1.3f * Time.deltaTime);
            if (Vector2.Distance(transform.position, target1) < 0.01f) a = false;
            yield return null;
        }

       
        transform.position = target2;
        trail.SetActive(true);

        while (Vector2.Distance(transform.position, finalTarget) >= 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, finalTarget, speed * 1.3f * Time.deltaTime);
            yield return null;
        }

        moving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fall")
        {
            if(collision.GetComponent<SpriteRenderer>().color == follower.currentColor)
            {
                //Destroys other object
                Destroy(collision.gameObject);

                //Handles the score
                mScore.AddInARow(1);
                mScore.AddScore();

                //Sound
                Manager_Audio.Instance.PlayFx(0, 1);

                //Particles
                SpawnFunkyParticles();

                //Camera Shake
                CameraShake.Instance.Shake(0.1f, 0.2f);

                //Changes background color
                ColorChanger.Instance.ChangeColor();

                LensDistortionChanger.Instance.ChangeLens(1);

            }
        }

        if(collision.tag == "Healer")
        {
            //Destroys other object
            Destroy(collision.gameObject);

            //Handles health
            Health.Instance.AddHealth(1);

            //and camera shake
            CameraShake.Instance.Shake(0.1f, 1f);

            //Spawns funky particles
            SpawnFunkyParticles();

            //Changes background color
            if(ColorChanger.Instance) ColorChanger.Instance.ChangeColor();

        }

        if(collision.tag == "ColorChanger")
        {
            //Destroys other object
            Destroy(collision.gameObject);

            //and camera shake
            CameraShake.Instance.Shake(0.1f, 1f);

            //Spawns funky particles
            SpawnSpecificParticles();

            //Changes background color
            if (ColorChanger.Instance) ColorChanger.Instance.ChangeColor();

            //Changes all fixed squares colors
            scene_manager.SetCarresColors();

            //Add score
            mScore.AddOneShotScore(1000);

            //Impact with lense
            LensDistortionChanger.Instance.ChangeLens(3);

        }
    }

    private void SpawnSpecificParticles()
    {
        GameObject a = Instantiate(explosionPrefabColored, transform.position, Quaternion.identity);
        Destroy(a, 5f);

    }

    private void SpawnFunkyParticles()
    {
        GameObject a = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ParticleSystem ps = a.GetComponent<ParticleSystem>();
        var col = ps.colorOverLifetime;
        col.enabled = true;

        //Color handling
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[]
              {
                      new GradientColorKey(Color.blue, 0.0f),
                      new GradientColorKey(Color.red, 1.0f) },
              new GradientAlphaKey[] {
                      new GradientAlphaKey(1.0f, 0.0f),
                      new GradientAlphaKey(1.0f, 0.8f),
                      new GradientAlphaKey(0.0f, 1.0f)
              });

        col.color = grad;

        Destroy(a, 0.5f);
    }

    public enum Sens { Gauche, Droite, Haut, Bas};

    private void SpawnMovingParticles(Sens s)
    {
        //0 = droite
        int sens = 0;
        if (s == Sens.Droite) sens = 180;
        if (s == Sens.Gauche) sens = 0;
        if (s == Sens.Haut) sens = 90;
        if (s == Sens.Bas) sens = -90;

        Quaternion q = Quaternion.Euler(sens, 90, 0);

        GameObject particles = Instantiate(movingParticles, transform.position, q);
        Destroy(particles, 1f);
    }

    IEnumerator TempoLight()
    {
        yield return new WaitForSeconds(1f);
        c_light.SetActive(true);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class GameManager : MonoBehaviour {
    //Trivia
    public GameObject triviashow;
    public GameObject triviashow2;
    public GameObject triviashow3;
    public GameObject triviashow4;
    public GameObject triviashow5;
    public GameObject triviashow6;
    public GameObject triviashow7;
    public GameObject triviashow8;
    public GameObject triviashow9;
    public GameObject triviashow10;
    public GameObject triviashow11;
    public GameObject triviashow12;
    public GameObject triviashow13;
    public GameObject triviashow14;
    public GameObject triviashow15;
    public GameObject triviashow16;
    public GameObject triviashow17;
    public GameObject triviashow18;
    public GameObject triviashow19;
    public GameObject triviashow20;
    public GameObject triviashow21;
    public GameObject triviashow22;
    public GameObject triviashow23;
    public GameObject triviashow24;
    public GameObject triviashow25;
    public Animator[] anim;

    public GameObject[] question;



    //TRIVIA WRONG 
    public GameObject[] wrong;
    
    
    

    public GameObject[] WrongButton;

    //Collider
    public GameObject collide;
    

    //Sound
    private bool mute;
    AudioSource audios;
    public AudioClip Button;
    public AudioClip Correct;
    public AudioClip Wrong;
     public float Volume;

    bool somebool = false;
   
    //Main
    private const int Coinscore = 5;
    private const int BBQscore = 120;
    private const int MangoScore = 50;
    private const int Rice = 80;
    private const int Correctanswer = 500;
    private const int WrongAnswer = -100;
    private const int Adscore = 1000;
    public static GameManager Instance { set; get; }
    public bool isDead { set; get; }
    private bool isGamestarted = false;
    private PlayerMotorClone motor;
    
    private EnemyController motor1;
    private float animationduration = 3.0f;

    //CHAR
   public GameObject maria;
   public GameObject ibarra;
 

    //UI
    public Text coinText, distanceText;
    private float coins, distance;
    public int coin;
    public int count = 3;
    //Death
    public Animator deathMenuAnim;
    public Animator pausedMenuAnim;
    
    public Text deaddistanceText, deadcoinText,pausedistanceText,pausedcoinText;

    int ischarsold;

    //ADS
    int adsControl = 5;
    int roundShowAds = 0;

    private void Start()
    {
       
        audios = GetComponent<AudioSource>();
        audios.Play();                                                  //PlayBACKGROUND
        audios.volume = PlayerPrefs.GetFloat("Volume");                 //GET1st Volume SCENE

        ischarsold = PlayerPrefs.GetInt("ischarsold");
        
      
        
            
          
        





    }
    private void Awake()
    {
        Instance = this;
        UpdateScores();
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotorClone>();

        //ads

        Advertisement.Initialize("3016738");
        

    }


    public void UpdateScores ()                                
    {
        coinText.text = coin.ToString();
        distanceText.text = distance.ToString();
             
    }
    public void UpdateModifier(float modifierAmount)
    {
        modifierAmount = 1.0f + modifierAmount;
        UpdateScores();
    }
 	
	private void Update () {

    //    if (Time.time < animationduration)
        // return;

       


        if ( !isGamestarted)                                         //startrun after anim camera
        {
          isGamestarted = true;
           motor.StartRunning();
            

        }
        
        if (isGamestarted && !isDead)                               //STOP distance when dead and before start run
        {
            //score
            distance += (Time.deltaTime);
            distanceText.text = distance.ToString("0");
            
        }
      


    }
   public void GetCoin()                                //coinget
    {
        coin++;
        coinText.text = coin.ToString("0");
        distance += Coinscore;
        distanceText.text = distanceText.text = distance.ToString("0");
       
    }  


    public void GetBBQ()
    {
        coin++;
        coinText.text = coin.ToString("0");
        coin += BBQscore;
        coinText.text = coinText.text = coin.ToString("0");
    }
    public void GetMangoScore()
    {
        coin++;
        coinText.text = coin.ToString("0");
        coin +=MangoScore;
        coinText.text = coinText.text = coin.ToString("0");
    }
    public void GetRice()
    {
        coin++;
        coinText.text = coin.ToString("0");
        coin += Rice;
        coinText.text = coinText.text = coin.ToString("0");
    }
    



    public void OnPlayButton()                  //Restart
    {
     UnityEngine.SceneManagement.SceneManager.LoadScene("Platform");           
     }
    public void ShowRewardedVid()
    {
        Advertisement.Show("PlayAgain");
     }
    
   
           
    public void OnPlayMariaButton()                  //Restart
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("PlatformMaria");
    }
    public void OnPlaySisaButton()                  //Restart
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlatformSISA");
    }
    public void OnPlayEliasButton()                  //Restart
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlatformElias");
    }
    public void OnPlayRizButton()                  //Restart
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlatformRiz");
    }

    public void OnHomeButton()                              //HomeMenu
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }

    public void OnDeath()                                           //DEATH
    {
        isDead = true;
        deaddistanceText.text = distance.ToString("0");                             //FINAL RESULTS OF COIN AND SCORE AFTER DEAD
        deadcoinText.text = coin.ToString("0");
        deathMenuAnim.SetTrigger("Dead");                                       //TRIGGER ANIM DEAD
        audios.Stop();                                                              //STOP AUDIO AFTER DEAD

        //SAVING SCOREANDCOINS
        if (PlayerPrefs.GetFloat("Highscore") < distance)                           //SAVING HIGH SCORE
            PlayerPrefs.SetFloat("Highscore", distance);

        PlayerPrefs.SetInt("MaxCoins", PlayerPrefs.GetInt("MaxCoins") + coin);                    //SAVING COINS
       
       
              Invoke("ShowRewardedVid", 4);
        }
        
    public void OnPause() //PAUSE MENU          
    {
        
        if (Time.time < animationduration)                                  //WONT PRESS PAUSE BEFORE ANIM CAM
            return;
        
      
        if (isGamestarted)                                                          //STOP MOVEMENT AFTER PRESS PAUSE
        {
           
            isDead = true;
            isGamestarted = true;
            motor.StopRunning();
            audios.Pause();
          
            
            
        }
     

        pausedistanceText.text = distance.ToString("0");                                        //SHOWS PAUSED CURRENT COIN AND HIGHSCORE
        pausedcoinText.text = coin.ToString("0");
        pausedMenuAnim.SetTrigger("Paused");
        

    }
    public void OnStopPlayer()                          //STOP WHEN QUESTION APPEARS
    {

        if (isGamestarted)
           
            isDead = true;
        motor.StopRunning();
        isGamestarted = true;
        audios.Pause();
        
    }
   public void StartPlayer()
    {
        if (!isGamestarted)
        {
          // isDead = false;
           motor.StartRunning();
            isGamestarted = true;
            audios.UnPause();


        }
    }
   

    public void OnResume()                                              //RESUME GAME
    {
      
        if (isGamestarted)
        {
                   
            
            isDead = false;
            motor.StartRunning();
            pausedMenuAnim.SetTrigger("Resumed");
            audios.PlayOneShot(Button, Volume);
            audios.UnPause();
                        }

       

    }
    //Answer1
    public void Showtrivias()
    {
        isDead = false;
        triviashow.SetActive(true);
        motor.StopRunning();
        
        
    }

    //answer2
    public void Showtrivias2()
    {
        isDead = false;
        triviashow2.SetActive(true);
        motor.StopRunning();

    }

    public void Showtrivias3()
    {
        isDead = false;
        triviashow3.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias4()
    {
        isDead = false;
        triviashow4.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias5()
    {
        isDead = false;
        triviashow5.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias6()
    {
        isDead = false;
        triviashow6.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias7()
    {
        isDead = false;
        triviashow7.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias8()
    {
        isDead = false;
        triviashow8.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias9()
    {
        isDead = false;
        triviashow9.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias10()
    {
        isDead = false;
        triviashow10.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias11()
    {
        isDead = false;
        triviashow11.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias12()
    {
        isDead = false;
        triviashow12.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias13()
    {
        isDead = false;
        triviashow13.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias14()
    {
        isDead = false;
        triviashow14.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias15()
    {
        isDead = false;
        triviashow15.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias16()
    {
        isDead = false;
        triviashow16.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias17()
    {
        isDead = false;
        triviashow17.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias18()
    {
        isDead = false;
        triviashow18.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias19()
    {
        isDead = false;
        triviashow19.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias20()
    {
        isDead = false;
        triviashow20.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias21()
    {
        isDead = false;
        triviashow21.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias22()
    {
        isDead = false;
        triviashow22.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias23()
    {
        isDead = false;
        triviashow23.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias24()
    {
        isDead = false;
        triviashow24.SetActive(true);
        motor.StopRunning();


    }
    public void Showtrivias25()
    {
        isDead = false;
        triviashow25.SetActive(true);
        motor.StopRunning();
    }










    //Correct Button
    public void GetCorrectAnswer()
    {
        
        audios.PlayOneShot(Correct, Volume);
        motor.StartRunning();
        isGamestarted = false;
        coin++;
        coinText.text = coin.ToString("0");
        coin += Correctanswer;
        coinText.text = coinText.text = coin.ToString("0");
        triviashow.SetActive(false);
        triviashow2.SetActive(false);
        triviashow3.SetActive(false);
        triviashow4.SetActive(false);
        triviashow5.SetActive(false);
        triviashow6.SetActive(false);
        triviashow7.SetActive(false);
        triviashow8.SetActive(false);
        triviashow9.SetActive(false);
        triviashow10.SetActive(false);
        triviashow11.SetActive(false);
        triviashow12.SetActive(false);
        triviashow13.SetActive(false);
        triviashow14.SetActive(false);
        triviashow15.SetActive(false);
        triviashow16.SetActive(false);
        triviashow17.SetActive(false);
        triviashow18.SetActive(false);
        triviashow19.SetActive(false);
        triviashow20.SetActive(false);
        triviashow21.SetActive(false);
        triviashow22.SetActive(false);
        triviashow23.SetActive(false);
        triviashow24.SetActive(false);
        triviashow25.SetActive(false);
        Instance.RemoveCollideNow();
    }
    //REMOVE COLLIDER
    public void RemoveCollideNow()
    {
        collide.SetActive(false);
    }
    //FORWRONG
    public void RemoveCollide()
    {
        Invoke("WaitforWrong",5.0f);
    }

    public void WaitforWrong()
    {
        motor.StartRunning();
        collide.SetActive(false);
        
    }

    //ONWRONGANSWER
    public void OnWrongAnswer()
    {

        audios.PlayOneShot(Wrong, Volume);
        coinText.text = coin.ToString("0");
        coin += WrongAnswer;
        coinText.text = coinText.text = coin.ToString("0");
        Invoke("WaitForWrongTriv", 5.0f);

        foreach (GameObject go in wrong)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in question)
        {
            go.SetActive(false);
        }
      
        // anim.SetTrigger("Shake");
        Invoke("returntriv", 5.0f);
        for (int i=0; i < anim.Length; i++)
       {
           
          anim[i].SetTrigger("Shake");

             
        }
        // WrongButton.SetActive(false);
        foreach (GameObject go in WrongButton)
        {
           go.SetActive(false);
            Invoke("ResetBut", 5.0f);
        }
        



    }
    public void ResetBut()
    {
        foreach (GameObject go in WrongButton)
        {
            go.SetActive(true);
           
        }
    }
    public void WaitForWrongTriv()
    {
        Instance.RemoveCollideNow();
        motor.StartRunning();
        triviashow.SetActive(false);
        triviashow2.SetActive(false);
        triviashow3.SetActive(false);
        triviashow4.SetActive(false);
        triviashow5.SetActive(false);
        triviashow6.SetActive(false);
        triviashow7.SetActive(false);
        triviashow8.SetActive(false);
        triviashow9.SetActive(false);
        triviashow10.SetActive(false);
        triviashow11.SetActive(false);
        triviashow12.SetActive(false);
        triviashow13.SetActive(false);
        triviashow14.SetActive(false);
        triviashow15.SetActive(false);
        triviashow16.SetActive(false);
        triviashow17.SetActive(false);
        triviashow18.SetActive(false);
        triviashow19.SetActive(false);
        triviashow20.SetActive(false);
        triviashow21.SetActive(false);
        triviashow22.SetActive(false);
        triviashow23.SetActive(false);
        triviashow24.SetActive(false);
        triviashow25.SetActive(false);



    }
    public void returntriv()
    {

        foreach (GameObject go in wrong)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in question)
        {
            go.SetActive(true);
        }
    
    }




    


}

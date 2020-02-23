using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text wordIndicator;
    public Text scoreIndicator;
    public Text letterIndicator;
    public Text typedLetter;

    private HangmanController hangman;
    private string word;
    private char[] revealed;
    private int score;
    private bool completed;

    private int k = 0;
    private int length = 0;
    private string []s = File.ReadAllLines(@"C:\Users\KKARTHIKEYAN\Documents\2dStickman\Assets\Scripts\notes.txt");

    void Start()
    {
        hangman = GameObject.FindGameObjectWithTag("Player").GetComponent<HangmanController>();
        length = s.Length;
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (completed)
        {
            //string tmp = Input.inputString;
            if (Input.anyKeyDown)
            {
                Debug.Log("reset done");
                reset();
            }
            return;
        }

        /*if (!Input.anyKeyDown)
            return;*/

        string s = Input.inputString;
        
       
        if(s.Length==1 && TextUtils.isAlpha(s[0])){
            if (!check(s.ToUpper()[0]))
            {
                hangman.punish();
                typedLetter.text += s;
            }

            if(hangman.isDead)
            {
                wordIndicator.text = word;
               
                completed = true;
                
            }
        }
    }

    private bool check(char c)
    {
        bool ret = false;
        int complete = 0;
        int score = 0;
        for (int i = 0; i < revealed.Length; i++)
        {
            if (c == word[i])
            {
                ret = true;
                if (revealed[i] == 0)
                {
                    revealed[i] = c;
                    score++;
                }

            }
            if (revealed[i] != 0)
            {
                complete++;
            }
        }

            if(score!=0)
            {
                this.score = score;
                if (complete == revealed.Length)
                {
                    this.completed = true;
                    this.score += revealed.Length;
                }
                updateScoreIndicator();
                updateWordIndicator();
            }

        
        return ret;
    }

    private void updateWordIndicator()
    {
        string displayed = "";
        for (int i=0;i<revealed.Length;i++)
        {
            char c = revealed[i];
            if(c==0)
            {
                c = '_';
            }
            displayed += ' ';
            displayed += c;
        }

        wordIndicator.text = displayed;
    }

    private void updateScoreIndicator()
    {
        scoreIndicator.text = "Score :" + score;
    }
    private void setWord(string word)
    {
        word = word.ToUpper();
        this.word = word;
        revealed = new char[word.Length];
        letterIndicator.text = "Letters: " + word.Length;

        updateWordIndicator();
    }

    void next()
    {
        
        setWord(s[k]);
        
        k += 1;
       
    }

    void reset()
    {
        score = 0;
        completed = false;
        typedLetter.text = "TypedLetters :";
        hangman.reset();
        updateScoreIndicator();
        if (k < length)
            next();
        else
            SceneManager.LoadScene("SampleScene");
    }
}

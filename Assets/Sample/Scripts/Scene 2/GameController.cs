using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    public GameObject[] RightAnswer;
    private Queue<GameObject> Answers = new Queue<GameObject>();

    public Transform Canvas;

    //[HideInInspector]
    public GameObject GivenAnswer;

    public GameObject myTextPrefab;
    public GameObject buttonPrefab;

    public GameObject[] combinations;
    public GameObject objInScene;
    Vector3 objInScenePos;

    public Vector3 objInSceneScale { get; private set; }

    private int i;

    GameObject introText;

    private int pontuacao;

    public void Start()
    {
        
        objInScene = GameObject.Find("combinacaoPortas2Desafio Variant");
        objInScenePos = objInScene.transform.position;
        objInSceneScale = objInScene.transform.localScale;
        
        for (int i = 0; i < RightAnswer.Length; i++)
        {
            Answers.Enqueue(RightAnswer[i]);
        }
        
    }

    public void CheckAnswer()
    {
        if (Answers.Peek().name == GivenAnswer.name)
        {
            pontuacao += 10;
            introText = (GameObject)Instantiate(myTextPrefab, Canvas);
            introText.transform.localPosition = new Vector3(-100f, 170f, 0f);
            introText.GetComponent<Text>().text = "Resposta Certa!!! Parabéns" + "\nSua Pontuação: " + pontuacao.ToString() + " Pontos";
        }
        else
        {
            pontuacao += 5;
            introText = (GameObject)Instantiate(myTextPrefab, Canvas);
            introText.transform.localPosition = new Vector3(-100f, 170f, 0f);
            introText.GetComponent<Text>().text = "Resposta Errada :(" + "\nSua Pontuação: " + pontuacao.ToString() + " Pontos";
            Debug.Log(pontuacao);
        }
    }
    
    public void Next()

    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (GivenAnswer != null)
        {
            i++;
            Answers.Dequeue();

            
            
            if(Answers.Count == 0 && sceneIndex == 6)
            {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                return;
            }
            

            print("What is objInScene: " + objInScene);
            Destroy(objInScene);
            print("Destroyed objInScene: " + objInScene);
            Destroy(GivenAnswer);
            Destroy(introText);

            objInScene = Instantiate(combinations[i], objInScenePos,Quaternion.identity);
            objInScene.transform.localScale = objInSceneScale;
            //objInScene.transform.localEulerAngles = new Vector3(0.269f, 1.044f, -4.89f);
            objInScene.transform.eulerAngles = new Vector3(0f, -90f, 0f);
            //objInScene.transform.localScale = new Vector3(0.06952316f, 0.06952316f, 0.06952316f);
            objInScene.SetActive(true);


        }
    }
}

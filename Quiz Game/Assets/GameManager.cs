using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
   
 public Question[] questions;
 private static List<Question> unansweredQuestions;

 private Question currentQuestion;  

[SerializeField]
 private Text factText; 

[SerializeField]
 private Text trueAnswerText;
[SerializeField]
 private Text falseAnswerText;

[SerializeField]
 private Animator animator;

[SerializeField]
private float timeBetweenQuestions = 1f;

void Start ()

  {
     if (unansweredQuestions == null || unansweredQuestions.Count == 0)
   {
      unansweredQuestions = questions.ToList<Question>();
   }
    setCurrentQuestion();
    
   }
  void setCurrentQuestion ()
  {
   int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
   currentQuestion = unansweredQuestions[randomQuestionIndex];
   
   factText.text = currentQuestion.fact;
   
   if (currentQuestion.isTrue)
   {
     trueAnswerText.text = "CORRECT";
     falseAnswerText.text = "WRONG";
   } else
    {
     trueAnswerText.text = "WRONG";
     falseAnswerText.text = "CORRECT";
    }
  }
  
 IEnumerator TransitionToNextQuestion ()
 {

 unansweredQuestions.Remove(currentQuestion);

 yield return new WaitForSeconds(timeBetweenQuestions);

 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
 
}
 public void UserSelectTrue ()
 {
  animator.SetTrigger("True");
  if (currentQuestion.isTrue)
 {
  Debug.Log ("CORRECT!");
 }else
 {
 Debug.Log("Wrong!");
 }

 StartCoroutine(TransitionToNextQuestion());

}

 public void UserSelectFalse()
 {
  animator.SetTrigger("False");
  if (!currentQuestion.isTrue)
 {
  Debug.Log("CORRECT!");
 }
 else
 {
 Debug.Log("Wrong!");
 }
 StartCoroutine(TransitionToNextQuestion());
}


}

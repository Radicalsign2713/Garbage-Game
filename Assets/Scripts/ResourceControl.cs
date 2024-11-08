using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceControl : MonoBehaviour
{
    private bool isInDistance = false;//if the resource is in collectable distance
    private bool isMouseDown = false;

    [SerializeField]
    private float duration = 1.8f;

    private float timer = 0;

    private TaskControl taskControl;

    private GameObject progressCanvas;
    private Image progressBar;
    private Rigidbody2D rb;
    [SerializeField] float range = 2;

    private void OnMouseDown()
    {
        if (!isInDistance) return;

        isMouseDown = true;
    }


    private void OnMouseUpAsButton()
    {
        if (!isInDistance) return;

        if (isMouseDown)
        {
            isMouseDown = false;

            if(timer >= duration)
            {
                CollectResource();
            }
        }
        
    }
    private void CollectResource()
    {

        Destroy(gameObject);

        taskControl.AddCompleted();
    }

    private void Awake()
    {
        taskControl = GameObject.Find("Task").GetComponent<TaskControl>();
        progressCanvas = transform.Find("Canvas").gameObject;
        progressBar = progressCanvas.transform.Find("ProgressBar").GetComponent<Image>();
        progressBar.fillAmount = 0;
        progressCanvas.SetActive(false);
    }
    private void Start()
    {
        GameObject player = GameObject.Find("Steve-E");
        rb = player.GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        if (Mathf.Sqrt(Mathf.Pow(rb.position.x - transform.position.x, 2) + Mathf.Pow(rb.position.y - transform.position.y, 2)) <= range){
            if (!isInDistance){
                isInDistance = true;
                StartCoroutine(CollectableAnimating());
                // progressCanvas.SetActive(true);
                progressBar.fillAmount = 0;
            }
        } else {isInDistance = false;}
        if ( isInDistance && isMouseDown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        progressBar.fillAmount = timer / duration;

        if(progressBar.fillAmount >= 1)
        {
            CollectResource();
        }
    }

    private IEnumerator CollectableAnimating()
    {
        while (isInDistance)
        {
            yield return transform.DOScale(1.1f, 0.5f).WaitForCompletion();
            yield return transform.DOScale(1f, 0.5f).WaitForCompletion();
        }
    }
}
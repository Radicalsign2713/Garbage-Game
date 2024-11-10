using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isInDistance = false;//if the resource is in collectable distance
    private bool isMouseDown = false;

    [SerializeField]
    private float duration = 1.8f;

    private float timer = 0;

    private TaskControl taskControl;

    private GameObject progressCanvas;
    private Image progressBar;

    private void OnMouseDown()
    {
        print("on Mouse down");
        if (!isInDistance) return;

        CollectResource();

        isMouseDown = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        print("on Mouse down");
        if (!isInDistance) return;

        isMouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isInDistance) return;

        if (isMouseDown)
        {
            isMouseDown = false;

            if (timer >= duration)
            {
                CollectResource();
            }
        }
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
        if (PlayerControl.instance.State != GameSate.playMode) return;
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("resource  get mouse down " + isInDistance);
        }
        if (Input.GetMouseButtonDown(0) && isInDistance)
        {
            print("resource  get mouse down ");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("�����Sprite�ϰ��£����߼�⣩");

                print("on Mouse down");
                
                isMouseDown = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("�����Sprite��̧�����߼�⣩");

                if (isMouseDown)
                {
                    isMouseDown = false;

                    if (timer >= duration)
                    {
                        CollectResource();
                    }
                }
            }
        }

        if ( isInDistance && isMouseDown)
        {
            timer += Time.deltaTime;

            progressCanvas.SetActive(true);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print($"trigger isInDistance {isInDistance} ");
            isInDistance = true;
            StartCoroutine(CollectableAnimating());
            //progressCanvas.SetActive(true);
            progressBar.fillAmount = 0;
        }
        else if (collision.CompareTag("PlayerGetter"))
        {
            CollectResource();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInDistance = false;
            progressCanvas.SetActive(false);
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

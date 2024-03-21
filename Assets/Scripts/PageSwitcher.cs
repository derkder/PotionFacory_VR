using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageSwitcher : MonoBehaviour
{
    public GameObject[] pages;
    private int currentPageIndex = 0; 

    private void Start()
    {
        SetPage(currentPageIndex); 
    }


    public void NavigateToNextPage()
    {
        Debug.Log("dfggh");
        currentPageIndex = (currentPageIndex + 1) % pages.Length;
        SetPage(currentPageIndex);
    }


    public void NavigateToPreviousPage()
    {
        currentPageIndex--;
        if (currentPageIndex < 0)
        {
            currentPageIndex = pages.Length - 1;
        }
        SetPage(currentPageIndex);
    }

    // set current page and ban other pages
    private void SetPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null)
            {
                pages[i].SetActive(i == index);
            }
        }
    }
}

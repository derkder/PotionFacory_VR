using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageSwitcher : MonoBehaviour
{
    public GameObject[] pages; // 存放所有页面的数组
    private int currentPageIndex = 0; // 当前显示的页面索引

    private void Start()
    {
        SetPage(currentPageIndex); // 初始时显示第一个页面
    }

    // 切换到下一个页面
    public void NavigateToNextPage()
    {
        Debug.Log("dfggh");
        currentPageIndex = (currentPageIndex + 1) % pages.Length; // 循环增加索引
        SetPage(currentPageIndex);
    }

    // 切换到上一个页面
    public void NavigateToPreviousPage()
    {
        currentPageIndex--;
        if (currentPageIndex < 0)
        {
            currentPageIndex = pages.Length - 1; // 循环减少索引
        }
        SetPage(currentPageIndex);
    }

    // 设置当前页面，并禁用其他所有页面
    private void SetPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null) // 确保页面存在
            {
                pages[i].SetActive(i == index); // 仅激活当前索引对应的页面
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SubScoreSlider : MonoBehaviour
{
    public GameObject[] fills;
    public Gradient colorStatus;

    public void UpdateSlider(int lv)
    {
        for (var i = 0; i < 20; i++)
        {
            fills[i].SetActive(false);
            fills[i].GetComponent<Image>().color = colorStatus.Evaluate(lv / 20f);
        }
        for (var i = 0; i < lv; i++)
        {
            fills[i].SetActive(true);
        }
    }
}

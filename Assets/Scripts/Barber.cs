using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Barber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI firstNameUI;
    [SerializeField] private TextMeshProUGUI lastNameUI;
    [SerializeField] private TextMeshProUGUI fiveStarReviewUI;
    [SerializeField] private TextMeshProUGUI fourStarReviewUI;
    [SerializeField] private TextMeshProUGUI threeStartReviewUI;
    [SerializeField] private TextMeshProUGUI twoStarReviewUI;
    [SerializeField] private TextMeshProUGUI oneStarReviewUI;

    public string FirstNameUI
    {
        set { firstNameUI.text = value; }
    }
    public string LastNameUI
    {
        set { lastNameUI.text = value; }
    }
    public string FiveStarUI
    {
        set { fiveStarReviewUI.text = value; }
    }
    public string FourStarUI
    {
        set { fourStarReviewUI.text = value; }
    }
    public string ThreeStarUI
    {
        set { threeStartReviewUI.text = value; }
    }
    public string TwoStarUI
    {
        set { twoStarReviewUI.text = value; }
    }
    public string OneStarUI
    {
        set { oneStarReviewUI.text = value; }
    }
}

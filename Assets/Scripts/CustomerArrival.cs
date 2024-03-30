using PW;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerArrival : MonoBehaviour
{
    public GameObject orderUI; 
    public GameObject actualOrderUI;
    public GameObject greenBorderUI;
    public GameObject TimerUI;

    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI timerText;
    private float timeLeft = 5f; // Countdown time in seconds
    private bool timerRunning = false;
    private float currentMoney = 0.00f;

    private string currentOrderName = "";

    public Sprite[] orderIcons; 
    public Transform uiOffset; 
    public Transform target;
    
    public TextMeshProUGUI _promptText;
    public TextMeshProUGUI _CustomerArrivalText;
    public TextMeshProUGUI _KeyText;
    private bool orderShown = false;
    private bool orderTaken = false;
    private bool orderCompleted = false;

    public SandwichMaker sandwichMaker;
    public SandwichMaker sandwichMaker1;
    public SandwichMaker sandwichMaker2;

    public DrinkMaker drinkMaker;
    public DrinkMaker drinkMaker1;
  

    public Transform targetPosition;
    public float speed = 1.0f;

    void Start()
    {
        orderUI.SetActive(false);
        greenBorderUI.SetActive(false);
        actualOrderUI.SetActive(false);
        TimerUI.SetActive(false);

        


    }

    void Update()
    {
        if (!orderShown && Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            ShowOrder();
            orderShown = true;
        }
        if (orderUI.activeSelf)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(uiOffset.position);
            orderUI.GetComponent<RectTransform>().position = screenPosition;
        }


        if (Input.GetButtonDown("Interact") && !orderTaken) TakeOrder();

    
        if (Input.GetKeyDown(KeyCode.F) && orderTaken && !orderCompleted) CompleteOrder();

        if (Input.GetKeyDown(KeyCode.R) && orderTaken && !orderCompleted)
        {
            ReOrder();
        }



        if (sandwichMaker.isPicked || sandwichMaker1.isPicked || sandwichMaker2.isPicked || drinkMaker.isPicked || drinkMaker1.isPicked)
        {
            greenBorderUI.SetActive(true);
        }

        // Update the timer if it's running
        if (timerRunning)
        {
            if (timeLeft > 0)
            {
                // Decrease the timer and update the UI
                timeLeft -= Time.deltaTime;
                UpdateTimerUI(timeLeft);
            }
            else
            {
                // Time's up
                timerRunning = false;
                timeLeft = 0;
                UpdateTimerUI(timeLeft);
                OnTimerEnd();
            }
        }
    }

    public void ReOrder()
    {
        // Indicate that the current order is no longer valid and needs to be repacked
        _promptText.text = "Oh item not on stock? Take Order Again.";
        orderTaken = false;
        orderCompleted = false;

        // Optionally, reset any specific states related to the current order
        sandwichMaker.isPicked = false;
        sandwichMaker1.isPicked = false;
        sandwichMaker2.isPicked = false;
        drinkMaker.isPicked = false;
        drinkMaker1.isPicked = false;
       

        // Reactivate the order UI if necessary
        orderUI.SetActive(true);
        greenBorderUI.SetActive(false);
        actualOrderUI.SetActive(false); // Consider whether you want to reset the visual order representation

        // Reset and restart the timer if you're using it for the order taking process
        timeLeft = 5f; // Or whatever initial value you prefer
        timerRunning = true;
        TimerUI.SetActive(true);

        // Ensure the green border UI is deactivated
        greenBorderUI.SetActive(false);
    }

    private void UpdateTimerUI(float time)
    {
        
        timerText.text = time.ToString("F2") + "s";
    }

    private void OnTimerEnd()
    {
        Debug.Log("Timer ended!");
        timerText.gameObject.SetActive(false);
        if(!orderCompleted) FailOrder();
    }


    void ShowOrder()
    {
        // Convert the world position of uiOffset to a screen position
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(uiOffset.position);
        orderUI.GetComponent<RectTransform>().position = screenPosition;

        orderUI.transform.localPosition = Vector3.zero;
        _promptText.text = "Take Order";
        orderUI.SetActive(true);
        


        
    }

    public void TakeOrder()
    {
        int randomIndex = Random.Range(0, orderIcons.Length);

        currentOrderName = orderIcons[randomIndex].name;

        actualOrderUI.GetComponentInChildren<Image>().sprite = orderIcons[randomIndex];

        actualOrderUI.SetActive(true);

        _KeyText.text = "F";
        _promptText.text = "Finished Order";
        orderTaken = true;

        timeLeft = 10f; // Reset the time each time an order is taken
        timerRunning = true;
        TimerUI.SetActive(true);
    }

   

    public void CompleteOrder()
    {

        bool correctItemPicked = false;

        if (sandwichMaker.isPicked && sandwichMaker.itemName == currentOrderName) correctItemPicked = true;
        else if (sandwichMaker1.isPicked && sandwichMaker1.itemName == currentOrderName) correctItemPicked = true;
        else if (sandwichMaker2.isPicked && sandwichMaker2.itemName == currentOrderName) correctItemPicked = true;
        else if (drinkMaker.isPicked && drinkMaker.itemName == currentOrderName) correctItemPicked = true;
        else if (drinkMaker1.isPicked && drinkMaker1.itemName == currentOrderName) correctItemPicked = true;
    

        if (correctItemPicked)
        {
            greenBorderUI.SetActive(false);
            orderCompleted = true;
            _promptText.text = "Thank You";
            currentMoney += 2.00f;

            GameManager.Instance.AddMoney(2.00f);
            UpdateMoneyUI();
            ResetOrderUI();
        }
        else
        {
            FailOrder();
        }

    }

    private void UpdateMoneyUI()
    {
        // Format the current money as currency with 2 decimal places
        moneyText.text = $"${GameManager.Instance.CurrentMoney:0.00}";
    }

    public void FailOrder()
    {
        _promptText.text = "SO MADDDD Retake Order";
        ResetOrderUI();
    }

    
    public void ResetOrderUI()
    {
        orderShown = false;
        orderTaken = false;
        orderCompleted = false;

        sandwichMaker.isPicked = false;
        sandwichMaker1.isPicked = false;
        sandwichMaker2.isPicked = false;
        drinkMaker.isPicked = false;
        drinkMaker1.isPicked = false;
       

        orderUI.SetActive(false);
        actualOrderUI.SetActive(false);
        greenBorderUI.SetActive(false);
        TimerUI.SetActive(false);

        _KeyText.text = "E";
        
    }
}

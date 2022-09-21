using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BunglarScript : MonoBehaviour
{
    [SerializeField] private Text _verticalPinLeftText;
    [SerializeField] private Text _verticalPinCenterText;
    [SerializeField] private Text _verticalPinRightText;
    [SerializeField] private Text _timerText;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _homeScreenPanel;
    private readonly float _timer = 60f;
    private float _timeLeft = 0f;
    private bool _timerOn = false;
    
    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimeText();
            }            
        }        
    }

    /// <summary>
    /// Метод перезапуска игры
    /// </summary>
    private void Restart()
    {        
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
        _timeLeft = _timer;
        _timerOn = true;
        _verticalPinLeftText.text = string.Empty;
        _verticalPinCenterText.text = string.Empty;
        _verticalPinRightText.text = string.Empty;
        _verticalPinLeftText.text = Random.Range(0, 11).ToString();
        _verticalPinCenterText.text = Random.Range(0, 11).ToString();
        _verticalPinRightText.text = Random.Range(0, 11).ToString();

    }

    /// <summary>
    /// Метод обновления текста по времени таймера
    /// </summary>
    private void UpdateTimeText()
    {
        if (_timeLeft > 0)
        {
            float minutes = Mathf.FloorToInt(_timeLeft / 60);
            float seconds = Mathf.FloorToInt(_timeLeft % 60);
            _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
        else
        {
            _timeLeft = 0;
            CheckLoseGame();
        }        
    }       

    /// <summary>
    /// Метод для изменения значения пинов
    /// </summary>
    /// <param name="leftPin"></param>
    /// <param name="centerPin"></param>
    /// <param name="rightPin"></param>
    private void ChangingValuePin(int leftPin, int centerPin, int rightPin)
    {        
        int leftValue = int.Parse(_verticalPinLeftText.text);
        int centerValue = int.Parse(_verticalPinCenterText.text);
        int rightValue = int.Parse(_verticalPinRightText.text);
                
        leftValue += leftPin;
        centerValue += centerPin;
        rightValue += rightPin;
                
        bool check = CheckBorderPin(leftValue, centerValue, rightValue);

        if (check == true)
        {
            _verticalPinLeftText.text = leftValue.ToString();
            _verticalPinCenterText.text = centerValue.ToString();
            _verticalPinRightText.text = rightValue.ToString();
        }                     

        CheckWinGame(leftValue, centerValue, rightValue);
    }
        
    /// <summary>
    /// Метод проверки победы
    /// </summary>
    /// <param name="firstValue"></param>
    /// <param name="secondValue"></param>
    /// <param name="thirdValue"></param>
    private void CheckWinGame(int firstValue, int secondValue, int thirdValue)
    {
        if (firstValue == secondValue && secondValue == thirdValue)
        {
            _winPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Метод проверки поражения
    /// </summary>
    private void CheckLoseGame()
    {        
        _losePanel.SetActive(true);        
    }

    /// <summary>
    /// Метод проверки чтобы пины не выходили за нужные границы
    /// </summary>
    /// <param name="leftValue"></param>
    /// <param name="centerValue"></param>
    /// <param name="rightValue"></param>
    /// <returns></returns>
    private bool CheckBorderPin(int leftValue, int centerValue, int rightValue)
    {        
        if (leftValue > 10 || centerValue > 10 || rightValue > 10)
        {
           return false;
        }
        else if (leftValue < 0 || centerValue < 0 || rightValue < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Метод для кнопки Заново
    /// </summary>
    public void OnClickRestart()
    {
        Restart();
    }

    /// <summary>
    /// Метод для кнопки Молоток
    /// </summary>
    public void HammerButtonOnClick()
    {
        ChangingValuePin(-1, +2, -1);
    }

    /// <summary>
    /// Метод для кнопки Дрель
    /// </summary>
    public void DrillButtonOnClick()
    {
        ChangingValuePin(+1, -1, 0);
    }

    /// <summary>
    /// Метод для кнопки Отмычка
    /// </summary>
    public void MasterKeyButtonOnClick()
    {
        ChangingValuePin(-1, +1, +1);
    }

    /// <summary>
    /// Метод для кнопки начала игры
    /// </summary>
    public void OnClickHomeScreen()
    {
        _homeScreenPanel.SetActive(false);
        Restart();
    }
}

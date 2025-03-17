using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManageController : MonoBehaviour
{
    public GameObject player;
    
    public Sprite healthCardImage;

    public Sprite playerImage;

    public Sprite star1Image;
    public Sprite star2Image;
    public Sprite star3Image;
    public Sprite cardBack1Image;
    public Sprite cardBack2Image;
    public Sprite cardBack3Image;
    
    private List<Card> _cards = new List<Card>();
    
    void Awake()
    {
        InitialCard();
    }

    void InitialCard()
    {
        _cards.Add(new HealthCard(2, player, 1, "Player instantly recovers 2 health points", "Heal", healthCardImage, playerImage));
        _cards.Add(new HealthCard(3, player, 2, "Player instantly recovers 3 health points", "Heal", healthCardImage, playerImage));
        _cards.Add(new HealthCard(4, player, 3, "Player instantly recovers 4 health points", "Heal", healthCardImage, playerImage));
        
        _cards.Add(new HealthCard(1, player, 1, "Player's maximum health increases by 1", "Increase", healthCardImage, playerImage));
        _cards.Add(new HealthCard(2, player, 2, "Player's maximum health increases by 2", "Increase", healthCardImage, playerImage));
        _cards.Add(new HealthCard(3, player, 3, "Player's maximum health increases by 3", "Increase", healthCardImage, playerImage));
        
        _cards.Add(new HealthCard(0.25f, player, 1, "Player's invulnerable time increases by 0.5", "Invulnerable", healthCardImage, playerImage));
        _cards.Add(new HealthCard(0.5f, player, 2, "Player's invulnerable time increases by 1", "Invulnerable", healthCardImage, playerImage));
        _cards.Add(new HealthCard(0.75f, player, 3, "Player's invulnerable time increases by 1.5", "Invulnerable", healthCardImage, playerImage));
    }
    
    Card GetRandomCard()
    {
        if (_cards.Count != 0)
        {
            Shuffle(_cards);
            int index = Random.Range(0, _cards.Count);
            return _cards[index];
        }
        else
        {
            return null;
        }
    }

    void OnEnable()
    {
        for (int i = 1; i <= 3; i++)
        {
            Card card = GetRandomCard();

            if (card != null)
            {
                var star = transform.Find("Star " + i);
                var cardBack = transform.Find("Card Back " + i);

                switch (card.Level)
                {
                    case 1:
                        star.gameObject.GetComponent<Image>().sprite = star1Image;
                        cardBack.gameObject.GetComponent<Image>().sprite = cardBack1Image;
                        break;
                    case 2:
                        star.gameObject.GetComponent<Image>().sprite = star2Image;
                        cardBack.gameObject.GetComponent<Image>().sprite = cardBack2Image;
                        break;
                    case 3:
                        star.gameObject.GetComponent<Image>().sprite = star3Image;
                        cardBack.gameObject.GetComponent<Image>().sprite = cardBack3Image;
                        break;
                    default: break;
                }
            
                var cardImage = transform.Find("Card Detail " + i + "/" + "Card Image");
                cardImage.GetComponent<Image>().sprite = card.CardImage;
                var cardDescription = transform.Find("Card Detail " + i + "/" + "Card Description");
                cardDescription.GetComponent<TextMeshProUGUI>().text = card.Description;
                var targetImage = transform.Find("Card Detail " + i + "/" + "Target Mask/Target Image");
                targetImage.GetComponent<Image>().sprite = card.TargetImage;
                var button = transform.Find("Card Detail " + i + "/" + "Card Button");
                button.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                button.gameObject.GetComponent<Button>().onClick.AddListener(card.ApplyBuff);
                int currenIndex = i;
                button.gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    gameObject.GetComponent<Animator>().SetTrigger("ChooseCard" + currenIndex);
                    StartCoroutine(WaitForAnimation());
                });
            }
        }
    }
    
    void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    void ChooseCard(int index)
    {
        gameObject.GetComponent<Animator>().SetTrigger("ChooseCard" + index);
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}

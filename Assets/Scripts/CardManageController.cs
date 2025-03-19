using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManageController : MonoBehaviour
{
    public GameObject player;
    public GameObject archer;
    public GameObject dinosaur;
    public GameObject boomerang;
    public GameObject butcher;
    public GameObject demon;
    public GameObject grave;
    public GameObject resurrector;
    public GameObject teleporter;
    public GameObject wizard;    
    
    public Sprite healCardImage;
    public Sprite increaseHealthCardImage;
    public Sprite invulnerableCardImage;
    
    public Sprite increaseEnergyCardImage;
    public Sprite cooldownEnergyCardImage;

    public Sprite increaseWalkSpeedImage;
    public Sprite increaseRunSpeedImage;

    public Sprite playerImage;
    public Sprite archerImage;
    public Sprite dinosaurImage;
    public Sprite boomerangImage;
    public Sprite butcherImage;
    public Sprite demonImage;
    public Sprite graveImage;
    public Sprite resurrectorImage;
    public Sprite teleporterImage;
    public Sprite wizardImage;
    
    
    public Sprite star1Image;
    public Sprite star2Image;
    public Sprite star3Image;
    public Sprite cardBack1Image;
    public Sprite cardBack2Image;
    public Sprite cardBack3Image;
    
    private List<Card> _playerCards = new List<Card>();
    private List<Card> _monsterCards = new List<Card>();
    
    void Awake()
    {
        InitialPlayerCards();
        InitialMonsterCards();
    }

    void InitialPlayerCards()
    {
        /*_playerCards.Add(new HealthCard(2, player, 1, "Player instantly recovers 2 health points", "Heal", healCardImage, playerImage));
        _playerCards.Add(new HealthCard(3, player, 2, "Player instantly recovers 3 health points", "Heal", healCardImage, playerImage));
        _playerCards.Add(new HealthCard(4, player, 3, "Player instantly recovers 4 health points", "Heal", healCardImage, playerImage));

        _playerCards.Add(new HealthCard(1, player, 1, "Player's maximum health increases by 1", "Increase", increaseHealthCardImage, playerImage));
        _playerCards.Add(new HealthCard(2, player, 2, "Player's maximum health increases by 2", "Increase", increaseHealthCardImage, playerImage));
        _playerCards.Add(new HealthCard(3, player, 3, "Player's maximum health increases by 3", "Increase", increaseHealthCardImage, playerImage));

        _playerCards.Add(new HealthCard(0.25f, player, 1, "Player's invulnerable time increases by 0.25", "Invulnerable", invulnerableCardImage, playerImage));
        _playerCards.Add(new HealthCard(0.5f, player, 2, "Player's invulnerable time increases by 0.5", "Invulnerable", invulnerableCardImage, playerImage));
        _playerCards.Add(new HealthCard(0.75f, player, 3, "Player's invulnerable time increases by 0.75", "Invulnerable", invulnerableCardImage, playerImage));
        
        _playerCards.Add(new EnergyCard(0.5f, player, 1, "Player's maximum stamina increases by 0.5 and recovery all stamina immediately", "Increase", increaseEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(1f, player, 2, "Player's maximum stamina increases by 1 and recovery all stamina immediately", "Increase", increaseEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(1.5f, player, 3, "Player's maximum stamina increases by 1.5 and recovery all stamina immediately", "Increase", increaseEnergyCardImage, playerImage));
        
        _playerCards.Add(new EnergyCard(0.1f, player, 1, "Player's stamina recovery time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(0.2f, player, 2, "Player's stamina recovery time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(0.3f, player, 3, "Player's stamina recovery time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, playerImage));*/
        
        _playerCards.Add(new SpeedCard(0.2f, player, 1, "Player's walk speed increases by 0.25", "Walk", increaseWalkSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(0.5f, player, 2, "Player's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(0.75f, player, 3, "Player's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, playerImage));
        
        _playerCards.Add(new SpeedCard(0.1f, player, 1, "Player's run speed increases by 1", "Run", increaseRunSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(1.25f, player, 2, "Player's run speed increases by 1.25", "Run", increaseRunSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(1.5f, player, 3, "Player's run speed increases by 1.5", "Run", increaseRunSpeedImage, playerImage));
    }

    void InitialMonsterCards()
    {
        /*_monsterCards.Add(new HealthCard(1, archer, 1, "Archer's maximum health increases by 1", "Increase", increaseHealthCardImage, archerImage));
        _monsterCards.Add(new HealthCard(2, archer, 2, "Archer's maximum health increases by 2", "Increase", increaseHealthCardImage, archerImage));
        _monsterCards.Add(new HealthCard(3, archer, 3, "Archer's maximum health increases by 3", "Increase", increaseHealthCardImage, archerImage));
        
        _monsterCards.Add(new HealthCard(2, dinosaur, 1, "Dinosaur's maximum health increases by 2", "Increase", increaseHealthCardImage, dinosaurImage));
        _monsterCards.Add(new HealthCard(3, dinosaur, 2, "Dinosaur's maximum health increases by 3", "Increase", increaseHealthCardImage, dinosaurImage));
        _monsterCards.Add(new HealthCard(4, dinosaur, 3, "Dinosaur's maximum health increases by 4", "Increase", increaseHealthCardImage, dinosaurImage));
        
        _monsterCards.Add(new HealthCard(1, boomerang, 1, "Boomerang thrower's maximum health increases by 1", "Increase", increaseHealthCardImage, boomerangImage));
        _monsterCards.Add(new HealthCard(2, boomerang, 2, "Boomerang thrower's maximum health increases by 2", "Increase", increaseHealthCardImage, boomerangImage));
        _monsterCards.Add(new HealthCard(3, boomerang, 3, "Boomerang thrower's maximum health increases by 3", "Increase", increaseHealthCardImage, boomerangImage));
        
        _monsterCards.Add(new HealthCard(1, butcher, 1, "Butcher's maximum health increases by 1", "Increase", increaseHealthCardImage, butcherImage));
        _monsterCards.Add(new HealthCard(2, butcher, 2, "Butcher's maximum health increases by 2", "Increase", increaseHealthCardImage, butcherImage));
        _monsterCards.Add(new HealthCard(3, butcher, 3, "Butcher's maximum health increases by 3", "Increase", increaseHealthCardImage, butcherImage));
        
        _monsterCards.Add(new HealthCard(1, demon, 1, "Demon's maximum health increases by 1", "Increase", increaseHealthCardImage, demonImage));
        _monsterCards.Add(new HealthCard(2, demon, 2, "Demon's maximum health increases by 2", "Increase", increaseHealthCardImage, demonImage));
        _monsterCards.Add(new HealthCard(3, demon, 3, "Demon's maximum health increases by 3", "Increase", increaseHealthCardImage, demonImage));
        
        _monsterCards.Add(new HealthCard(1, grave, 1, "Grave's maximum health increases by 1", "Increase", increaseHealthCardImage, graveImage));
        _monsterCards.Add(new HealthCard(2, grave, 2, "Grave's maximum health increases by 2", "Increase", increaseHealthCardImage, graveImage));
        _monsterCards.Add(new HealthCard(3, grave, 3, "Grave's maximum health increases by 3", "Increase", increaseHealthCardImage, graveImage));
        
        _monsterCards.Add(new HealthCard(1, resurrector, 1, "Resurrector's maximum health increases by 1", "Increase", increaseHealthCardImage, resurrectorImage));
        _monsterCards.Add(new HealthCard(2, resurrector, 2, "Resurrector's maximum health increases by 2", "Increase", increaseHealthCardImage, resurrectorImage));
        _monsterCards.Add(new HealthCard(3, resurrector, 3, "Resurrector's maximum health increases by 3", "Increase", increaseHealthCardImage, resurrectorImage));
        
        _monsterCards.Add(new HealthCard(1, teleporter, 1, "Teleporter's maximum health increases by 1", "Increase", increaseHealthCardImage, teleporterImage));
        _monsterCards.Add(new HealthCard(2, teleporter, 2, "Teleporter's maximum health increases by 2", "Increase", increaseHealthCardImage, teleporterImage));
        _monsterCards.Add(new HealthCard(3, teleporter, 3, "Teleporter's maximum health increases by 3", "Increase", increaseHealthCardImage, teleporterImage));
        
        _monsterCards.Add(new HealthCard(1, wizard, 1, "Wizard's maximum health increases by 1", "Increase", increaseHealthCardImage, wizardImage));
        _monsterCards.Add(new HealthCard(2, wizard, 2, "Wizard's maximum health increases by 2", "Increase", increaseHealthCardImage, wizardImage));
        _monsterCards.Add(new HealthCard(3, wizard, 3, "Wizard's maximum health increases by 3", "Increase", increaseHealthCardImage, wizardImage));
        
        _monsterCards.Add(new EnergyCard(0.1f, archer, 1, "Archer's attack cooldown time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, archerImage));
        _monsterCards.Add(new EnergyCard(0.2f, archer, 2, "Archer's attack cooldown time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, archerImage));
        _monsterCards.Add(new EnergyCard(0.3f, archer, 3, "Archer's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, archerImage));
                              
        _monsterCards.Add(new EnergyCard(0.1f, boomerang, 1, "Boomerang thrower's attack cooldown time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, boomerangImage));
        _monsterCards.Add(new EnergyCard(0.2f, boomerang, 2, "Boomerang thrower's attack cooldown time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, boomerangImage));
        _monsterCards.Add(new EnergyCard(0.3f, boomerang, 3, "Boomerang thrower's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, boomerangImage));
        
        _monsterCards.Add(new EnergyCard(0.1f, butcher, 1, "Butcher's attack cooldown time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, butcherImage));
        _monsterCards.Add(new EnergyCard(0.2f, butcher, 2, "Butcher's attack cooldown time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, butcherImage));
        _monsterCards.Add(new EnergyCard(0.3f, butcher, 3, "Butcher's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, butcherImage));
        
        _monsterCards.Add(new EnergyCard(0.1f, demon, 1, "Demon's attack cooldown time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, demonImage));
        _monsterCards.Add(new EnergyCard(0.2f, demon, 2, "Demon's attack cooldown time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, demonImage));
        _monsterCards.Add(new EnergyCard(0.3f, demon, 3, "Demon's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, demonImage));
        
        _monsterCards.Add(new EnergyCard(0.1f, grave, 1, "Grave's resurrection time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, graveImage));
        _monsterCards.Add(new EnergyCard(0.2f, grave, 2, "Grave's resurrection time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, graveImage));
        _monsterCards.Add(new EnergyCard(0.3f, grave, 3, "Grave's resurrection time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, graveImage));
        
        _monsterCards.Add(new EnergyCard(0.1f, resurrector, 1, "Resurrector's attack cooldown time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, resurrectorImage));
        _monsterCards.Add(new EnergyCard(0.2f, resurrector, 2, "Resurrector's attack cooldown time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, resurrectorImage));
        _monsterCards.Add(new EnergyCard(0.3f, resurrector, 3, "Resurrector's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, resurrectorImage));
        
        _monsterCards.Add(new EnergyCard(0.1f, teleporter, 1, "Teleporter's attack cooldown time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, teleporterImage));
        _monsterCards.Add(new EnergyCard(0.2f, teleporter, 2, "Teleporter's attack cooldown time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, teleporterImage));
        _monsterCards.Add(new EnergyCard(0.3f, teleporter, 3, "Teleporter's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, teleporterImage));
        
        _monsterCards.Add(new EnergyCard(0.1f, wizard, 1, "Wizard's attack cooldown time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, wizardImage));
        _monsterCards.Add(new EnergyCard(0.2f, wizard, 2, "Wizard's attack cooldown time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, wizardImage));
        _monsterCards.Add(new EnergyCard(0.3f, wizard, 3, "Wizard's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, wizardImage));*/
        
        _monsterCards.Add(new SpeedCard(0.5f, archer, 1, "Archer's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, archerImage));
        _monsterCards.Add(new SpeedCard(0.75f, archer, 2, "Archer's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, archerImage));
        _monsterCards.Add(new SpeedCard(1f, archer, 3, "Archer's walk speed increases by 1", "Walk", increaseWalkSpeedImage, archerImage));
        
        _monsterCards.Add(new SpeedCard(0.75f, dinosaur, 1, "Dinosaur's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, dinosaurImage));
        _monsterCards.Add(new SpeedCard(1f, dinosaur, 2, "Dinosaur's walk speed increases by 1", "Walk", increaseWalkSpeedImage, dinosaurImage));
        _monsterCards.Add(new SpeedCard(1.25f, dinosaur, 3, "Dinosaur's walk speed increases by 1.25", "Walk", increaseWalkSpeedImage, dinosaurImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, boomerang, 1, "Boomerang thrower's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, boomerangImage));
        _monsterCards.Add(new SpeedCard(0.75f, boomerang, 2, "Boomerang thrower's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, boomerangImage));
        _monsterCards.Add(new SpeedCard(1f, boomerang, 3, "Boomerang thrower's walk speed increases by 1", "Walk", increaseWalkSpeedImage, boomerangImage));
        
        _monsterCards.Add(new SpeedCard(1f, butcher, 1, "Butcher's walk speed increases by 1", "Walk", increaseWalkSpeedImage, butcherImage));
        _monsterCards.Add(new SpeedCard(1.25f, butcher, 2, "Butcher's walk speed increases by 1.25", "Walk", increaseWalkSpeedImage, butcherImage));
        _monsterCards.Add(new SpeedCard(1.5f, butcher, 3, "Butcher's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, butcherImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, demon, 1, "Demon's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, demonImage));
        _monsterCards.Add(new SpeedCard(0.75f, demon, 2, "Demon's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, demonImage));
        _monsterCards.Add(new SpeedCard(1f, demon, 3, "Demon's walk speed increases by 1", "Walk", increaseWalkSpeedImage, demonImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, resurrector, 1, "Resurrector's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, resurrectorImage));
        _monsterCards.Add(new SpeedCard(0.75f, resurrector, 2, "Resurrector's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, resurrectorImage));
        _monsterCards.Add(new SpeedCard(1f, resurrector, 3, "Resurrector's walk speed increases by 1", "Walk", increaseWalkSpeedImage, resurrectorImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, teleporter, 1, "Teleporter's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, teleporterImage));
        _monsterCards.Add(new SpeedCard(0.75f, teleporter, 2, "Teleporter's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, teleporterImage));
        _monsterCards.Add(new SpeedCard(1f, teleporter, 3, "Teleporter's walk speed increases by 1", "Walk", increaseWalkSpeedImage, teleporterImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, wizard, 1, "Wizard's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, wizardImage));
        _monsterCards.Add(new SpeedCard(0.75f, wizard, 2, "Wizard's walk speed increases by 0.75", "Walk", increaseWalkSpeedImage, wizardImage));
        _monsterCards.Add(new SpeedCard(1f, wizard, 3, "Wizard's walk speed increases by 1", "Walk", increaseWalkSpeedImage, wizardImage));
    }
    
    Card GetRandomPlayerCard()
    {
        if (_playerCards.Count != 0)
        {
            Shuffle(_playerCards);
            int index = Random.Range(0, _playerCards.Count);
            if (_playerCards[index].Type == "Cooldown")
            {
                while (_playerCards[index].Type == "Cooldown")
                {
                    var energyCard = (EnergyCard)_playerCards[index];
                    var currentValue = player.gameObject.GetComponent<PlayerController>().GetStaminaCooldownMax();
                    var newValue = currentValue * (1 - energyCard.GetBuffParameter());
                    
                    if (newValue >= 0.5)
                    {
                        return energyCard;
                    }
                    else
                    {
                        Shuffle(_playerCards);
                        index = Random.Range(0, _playerCards.Count);
                    } 
                }
                
                return _playerCards[index];
            }
            else
            {
                return _playerCards[index];   
            }
        }
        else
        {
            return null;
        }
    }

    Card GetRandomMonsterCard(int level)
    {
        List<Card> cardsByLevel = new List<Card>();
        foreach (Card card in _monsterCards)
        {
            if (card.Level == level)
            {
                cardsByLevel.Add(card);
            }
        }
        
        if (cardsByLevel.Count != 0)
        {
            Shuffle(cardsByLevel);
            int index = Random.Range(0, cardsByLevel.Count);
            if (cardsByLevel[index].Type == "Cooldown")
            {
                while (cardsByLevel[index].Type == "Cooldown")
                {
                    var energyCard = (EnergyCard)cardsByLevel[index];
                    var target = energyCard.Target;
                    Debug.Log(target);
                    var currentValue = target.gameObject.GetComponent<MonsterController>().monsterParameter.attackCooldown;
                    var newValue = currentValue * (1 - energyCard.GetBuffParameter());
                    if (newValue >= 0.5)
                    {
                        return energyCard;
                    }
                    else
                    {
                        Shuffle(cardsByLevel);
                        index = Random.Range(0, cardsByLevel.Count);
                    } 
                }
                
                return cardsByLevel[index];
            }
            else
            {
                return cardsByLevel[index];   
            }
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
            Card card = GetRandomPlayerCard();

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
                    StartCoroutine(WaitForAnimation(currenIndex, card.Level));
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

    IEnumerator WaitForAnimation(int currentIndex, int level)
    {
        if (currentIndex == 1 || currentIndex == 3)
        {
            yield return new WaitForSecondsRealtime(2.5f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(2f);
        }
        
        Card card = GetRandomMonsterCard(level);
        if (card != null)
        {
            var cardImage = transform.Find("Card Detail " + currentIndex + "/" + "Card Image");
            cardImage.GetComponent<Image>().sprite = card.CardImage;
            var cardDescription = transform.Find("Card Detail " + currentIndex + "/" + "Card Description");
            cardDescription.GetComponent<TextMeshProUGUI>().text = card.Description;
            var targetImage = transform.Find("Card Detail " + currentIndex + "/" + "Target Mask/Target Image");
            targetImage.GetComponent<Image>().sprite = card.TargetImage;
            card.ApplyBuff();
        }
        yield return new WaitForSecondsRealtime(4f);
        
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}

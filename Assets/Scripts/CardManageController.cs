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
    public GameObject cinemachineCamera;
    
    public Sprite healCardImage;
    public Sprite increaseHealthCardImage;
    public Sprite invulnerableCardImage;
    
    public Sprite increaseEnergyCardImage;
    public Sprite cooldownEnergyCardImage;

    public Sprite increaseWalkSpeedImage;
    public Sprite increaseRunSpeedImage;

    public Sprite increaseSightImage;

    public Sprite increaseBulletSpeedImage;
    public Sprite increaseBulletDistanceImage;
    public Sprite increaseBulletStorageImage;
    public Sprite decreaseBulletReloadTimeImage;
    public Sprite increaseBulletDamageImage;
    public Sprite increaseBulletEffectImage;
    
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
    
    public Sprite arrowProjectileImage;
    public Sprite boomerangProjectileImage;
    public Sprite demonProjectileImage;
    public Sprite freezeProjectileImage;
    public Sprite missleProjectileImage;
    public Sprite monsterProjectileImage;
    public Sprite normalProjectileImage;
    
    public Sprite star1Image;
    public Sprite star2Image;
    public Sprite star3Image;
    public Sprite cardBack1Image;
    public Sprite cardBack2Image;
    public Sprite cardBack3Image;
    
    private List<Card> _playerCards = new List<Card>();
    private List<Card> _monsterCards = new List<Card>();
    private int _maxPlayerCardIteration;
    private int _maxMonsterCardIteration;
    
    void Awake()
    {
        InitialPlayerCards();
        InitialMonsterCards();
        _maxPlayerCardIteration = _playerCards.Count;
        _maxMonsterCardIteration = _monsterCards.Count;
    }

    void InitialPlayerHealthCard()
    {
        _playerCards.Add(new HealthCard(2, player, 1, "Player instantly recovers 2 health points", "Heal", healCardImage, playerImage));
        _playerCards.Add(new HealthCard(4, player, 2, "Player instantly recovers 4 health points", "Heal", healCardImage, playerImage));
        _playerCards.Add(new HealthCard(6, player, 3, "Player instantly recovers 6 health points", "Heal", healCardImage, playerImage));

        _playerCards.Add(new HealthCard(1, player, 1, "Player's maximum health increases by 1 and heal the equivalent amount", "Increase", increaseHealthCardImage, playerImage));
        _playerCards.Add(new HealthCard(2, player, 2, "Player's maximum health increases by 2 and heal the equivalent amount", "Increase", increaseHealthCardImage, playerImage));
        _playerCards.Add(new HealthCard(3, player, 3, "Player's maximum health increases by 3 and heal the equivalent amount", "Increase", increaseHealthCardImage, playerImage));

        _playerCards.Add(new HealthCard(0.5f, player, 1, "Player's invulnerable time increases by 0.5", "Invulnerable", invulnerableCardImage, playerImage));
        _playerCards.Add(new HealthCard(1f, player, 2, "Player's invulnerable time increases by 1", "Invulnerable", invulnerableCardImage, playerImage));
        _playerCards.Add(new HealthCard(1.5f, player, 3, "Player's invulnerable time increases by 1.5", "Invulnerable", invulnerableCardImage, playerImage));
    }
    void InitialPlayerEnergyCard()
    {
        _playerCards.Add(new EnergyCard(0.5f, player, 1, "Player's maximum stamina increases by 0.5 and recovery all stamina immediately", "Increase", increaseEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(1f, player, 2, "Player's maximum stamina increases by 1 and recovery all stamina immediately", "Increase", increaseEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(1.5f, player, 3, "Player's maximum stamina increases by 1.5 and recovery all stamina immediately", "Increase", increaseEnergyCardImage, playerImage));
        
        _playerCards.Add(new EnergyCard(0.1f, player, 1, "Player's stamina recovery time is reduced by 10%", "Cooldown", cooldownEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(0.2f, player, 2, "Player's stamina recovery time is reduced by 20%", "Cooldown", cooldownEnergyCardImage, playerImage));
        _playerCards.Add(new EnergyCard(0.3f, player, 3, "Player's stamina recovery time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, playerImage));
    }
    void InitialPlayerSpeedCard()
    {
        _playerCards.Add(new SpeedCard(0.5f, player, 1, "Player's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(1f, player, 2, "Player's walk speed increases by 1", "Walk", increaseWalkSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(1.5f, player, 3, "Player's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, playerImage));
        
        _playerCards.Add(new SpeedCard(1f, player, 1, "Player's run speed increases by 1", "Run", increaseRunSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(2f, player, 2, "Player's run speed increases by 2", "Run", increaseRunSpeedImage, playerImage));
        _playerCards.Add(new SpeedCard(3f, player, 3, "Player's run speed increases by 3", "Run", increaseRunSpeedImage, playerImage));
    }
    void InitialPlayerSightCard()
    {
        _playerCards.Add(new SightCard(1f, cinemachineCamera, 1, "Player's maximum vision range increases by 1", "Increase", increaseSightImage, playerImage));
        _playerCards.Add(new SightCard(2f, cinemachineCamera, 2, "Player's maximum vision range increases by 2", "Increase", increaseSightImage, playerImage));
        _playerCards.Add(new SightCard(3f, cinemachineCamera, 3, "Player's maximum vision range increases by 3", "Increase", increaseSightImage, playerImage));
    }
    void InitialPlayerBulletCard()
    {
        /*_playerCards.Add(new BulletCard(1f, player, 1, "Player's normal bullet speed increases by 1",  "Speed", increaseBulletSpeedImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(2f, player, 2, "Player's normal bullet speed increases by 2",  "Speed", increaseBulletSpeedImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(3f, player, 3, "Player's normal bullet speed increases by 3",  "Speed", increaseBulletSpeedImage, normalProjectileImage));
        
        _playerCards.Add(new BulletCard(0.5f, player, 1, "Player's freeze bullet speed increases by 0.5",  "Speed", increaseBulletSpeedImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(1f, player, 2, "Player's freeze bullet speed increases by 1",  "Speed", increaseBulletSpeedImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(1.5f, player, 3, "Player's freeze bullet speed increases by 1.5",  "Speed", increaseBulletSpeedImage, freezeProjectileImage));
        
        _playerCards.Add(new BulletCard(0.5f, player, 1, "Player's missle bullet speed increases by 0.5",  "Speed", increaseBulletSpeedImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(1f, player, 2, "Player's missle bullet speed increases by 1",  "Speed", increaseBulletSpeedImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(1.5f, player, 3, "Player's missle bullet speed increases by 1.5",  "Speed", increaseBulletSpeedImage, missleProjectileImage));
        
        _playerCards.Add(new BulletCard(1f, player, 1, "Player's normal bullet max distance increases by 1",  "Distance", increaseBulletDistanceImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(2f, player, 2, "Player's normal bullet max distance increases by 2",  "Distance", increaseBulletDistanceImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(3f, player, 3, "Player's normal bullet max distance increases by 3",  "Distance", increaseBulletDistanceImage, normalProjectileImage));
        
        _playerCards.Add(new BulletCard(2f, player, 1, "Player's freeze bullet max distance increases by 2",  "Distance", increaseBulletDistanceImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(4f, player, 2, "Player's freeze bullet max distance increases by 4",  "Distance", increaseBulletDistanceImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(6f, player, 3, "Player's freeze bullet max distance increases by 6",  "Distance", increaseBulletDistanceImage, freezeProjectileImage));
        
        _playerCards.Add(new BulletCard(0.5f, player, 1, "Player's missle bullet max distance increases by 0.5",  "Distance", increaseBulletDistanceImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(1f, player, 2, "Player's missle bullet max distance increases by 1",  "Distance", increaseBulletDistanceImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(1.5f, player, 3, "Player's missle bullet max distance increases by 1.5",  "Distance", increaseBulletDistanceImage, missleProjectileImage));
        
        _playerCards.Add(new BulletCard(5f, player, 1, "Player's normal bullet max storage increases by 5",  "Storage", increaseBulletStorageImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(10f, player, 2, "Player's normal bullet max storage increases by 10",  "Storage", increaseBulletStorageImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(15f, player, 3, "Player's normal bullet max storage increases by 15",  "Storage", increaseBulletStorageImage, normalProjectileImage));
        
        _playerCards.Add(new BulletCard(2f, player, 1, "Player's freeze bullet max storage increases by 2",  "Storage", increaseBulletStorageImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(4f, player, 2, "Player's freeze bullet max storage increases by 4",  "Storage", increaseBulletStorageImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(6f, player, 3, "Player's freeze bullet max storage increases by 6",  "Storage", increaseBulletStorageImage, freezeProjectileImage));
        
        _playerCards.Add(new BulletCard(1f, player, 1, "Player's missle bullet max storage increases by 1",  "Storage", increaseBulletStorageImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(2f, player, 2, "Player's missle bullet max storage increases by 2",  "Storage", increaseBulletStorageImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(3f, player, 3, "Player's missle bullet max storage increases by 3",  "Storage", increaseBulletStorageImage, missleProjectileImage));
        
        _playerCards.Add(new BulletCard(0.1f, player, 1, "Player's normal bullet reload time is reduced by 10%",  "Cooldown", decreaseBulletReloadTimeImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(0.2f, player, 2, "Player's normal bullet reload time is reduced by 20%",  "Cooldown", decreaseBulletReloadTimeImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(0.3f, player, 3, "Player's normal bullet reload time is reduced by 30%",  "Cooldown", decreaseBulletReloadTimeImage, normalProjectileImage));

        _playerCards.Add(new BulletCard(0.1f, player, 1, "Player's freeze bullet reload time is reduced by 10%", "Cooldown", decreaseBulletReloadTimeImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(0.2f, player, 2, "Player's freeze bullet reload time is reduced by 20%", "Cooldown", decreaseBulletReloadTimeImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(0.3f, player, 3, "Player's freeze bullet reload time is reduced by 30%", "Cooldown", decreaseBulletReloadTimeImage, freezeProjectileImage));
        
        _playerCards.Add(new BulletCard(0.1f, player, 1, "Player's missle bullet reload time is reduced by 10%", "Cooldown", decreaseBulletReloadTimeImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(0.2f, player, 2, "Player's missle bullet reload time is reduced by 20%", "Cooldown", decreaseBulletReloadTimeImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(0.3f, player, 3, "Player's missle bullet reload time is reduced by 30%", "Cooldown", decreaseBulletReloadTimeImage, missleProjectileImage));
        
        _playerCards.Add(new BulletCard(1f, player, 1, "Player's normal bullet damage increases by 1",  "Damage", increaseBulletDamageImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(2f, player, 2, "Player's normal bullet damage increases by 2",  "Damage", increaseBulletDamageImage, normalProjectileImage));
        _playerCards.Add(new BulletCard(3f, player, 3, "Player's normal bullet damage increases by 3",  "Damage", increaseBulletDamageImage, normalProjectileImage));
       
        _playerCards.Add(new BulletCard(1f, player, 1, "Player's freeze bullet damage increases by 1",  "Damage", increaseBulletDamageImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(2f, player, 2, "Player's freeze bullet damage increases by 2",  "Damage", increaseBulletDamageImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(3f, player, 3, "Player's freeze bullet damage increases by 3",  "Damage", increaseBulletDamageImage, freezeProjectileImage));
        
        _playerCards.Add(new BulletCard(1f, player, 1, "Player's missle bullet damage increases by 1",  "Damage", increaseBulletDamageImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(2f, player, 2, "Player's missle bullet damage increases by 2",  "Damage", increaseBulletDamageImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(3f, player, 3, "Player's missle bullet damage increases by 3",  "Damage", increaseBulletDamageImage, missleProjectileImage));*/
        
        /*_playerCards.Add(new BulletCard(0.1f, player, 1, "Player's freeze bullet effect zone increases by 10%",  "Effect", increaseBulletEffectImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(0.2f, player, 2, "Player's freeze bullet effect zone increases by 20%",  "Effect", increaseBulletEffectImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(0.3f, player, 3, "Player's freeze bullet effect zone increases by 30%",  "Effect", increaseBulletEffectImage, freezeProjectileImage));
        
        _playerCards.Add(new BulletCard(0.1f, player, 1, "Player's missle bullet effect zone increases by 10%",  "Effect", increaseBulletEffectImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(0.2f, player, 2, "Player's missle bullet effect zone increases by 20%",  "Effect", increaseBulletEffectImage, missleProjectileImage));
        _playerCards.Add(new BulletCard(0.3f, player, 3, "Player's missle bullet effect zone increases by 30%",  "Effect", increaseBulletEffectImage, missleProjectileImage));*/
        
        _playerCards.Add(new BulletCard(1f, player, 1, "Player's freeze bullet effect zone exist time increases by 1s",  "Time", increaseBulletEffectImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(2f, player, 2, "Player's freeze bullet effect zone exist time increases by 2s",  "Time", increaseBulletEffectImage, freezeProjectileImage));
        _playerCards.Add(new BulletCard(3f, player, 3, "Player's freeze bullet effect zone exist time increases by 3s",  "Time", increaseBulletEffectImage, freezeProjectileImage));
    }
    void InitialPlayerCards()
    {
        //InitialPlayerHealthCard();
        //InitialPlayerEnergyCard();
        //InitialPlayerSpeedCard();
        //InitialPlayerSightCard();
        InitialPlayerBulletCard();
    }

    void InitialMonsterHealthCard()
    {
        _monsterCards.Add(new HealthCard(1, archer, 1, "Archer's maximum health increases by 1", "Increase", increaseHealthCardImage, archerImage));
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
    }
    void InitialMonsterEnergyCard()
    {
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
        _monsterCards.Add(new EnergyCard(0.3f, wizard, 3, "Wizard's attack cooldown time is reduced by 30%", "Cooldown", cooldownEnergyCardImage, wizardImage));
    }
    void InitialMonsterSpeedCard()
    {
        _monsterCards.Add(new SpeedCard(0.5f, archer, 1, "Archer's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, archerImage));
        _monsterCards.Add(new SpeedCard(1f, archer, 2, "Archer's walk speed increases by 1", "Walk", increaseWalkSpeedImage, archerImage));
        _monsterCards.Add(new SpeedCard(1.5f, archer, 3, "Archer's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, archerImage));
        
        _monsterCards.Add(new SpeedCard(1f, dinosaur, 1, "Dinosaur's walk speed increases by 1", "Walk", increaseWalkSpeedImage, dinosaurImage));
        _monsterCards.Add(new SpeedCard(2f, dinosaur, 2, "Dinosaur's walk speed increases by 2", "Walk", increaseWalkSpeedImage, dinosaurImage));
        _monsterCards.Add(new SpeedCard(3f, dinosaur, 3, "Dinosaur's walk speed increases by 3", "Walk", increaseWalkSpeedImage, dinosaurImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, boomerang, 1, "Boomerang thrower's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, boomerangImage));
        _monsterCards.Add(new SpeedCard(1f, boomerang, 2, "Boomerang thrower's walk speed increases by 1", "Walk", increaseWalkSpeedImage, boomerangImage));
        _monsterCards.Add(new SpeedCard(1.5f, boomerang, 3, "Boomerang thrower's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, boomerangImage));
        
        _monsterCards.Add(new SpeedCard(1f, butcher, 1, "Butcher's walk speed increases by 1", "Walk", increaseWalkSpeedImage, butcherImage));
        _monsterCards.Add(new SpeedCard(2f, butcher, 2, "Butcher's walk speed increases by 2", "Walk", increaseWalkSpeedImage, butcherImage));
        _monsterCards.Add(new SpeedCard(3f, butcher, 3, "Butcher's walk speed increases by 3", "Walk", increaseWalkSpeedImage, butcherImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, demon, 1, "Demon's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, demonImage));
        _monsterCards.Add(new SpeedCard(1f, demon, 2, "Demon's walk speed increases by 1", "Walk", increaseWalkSpeedImage, demonImage));
        _monsterCards.Add(new SpeedCard(1.5f, demon, 3, "Demon's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, demonImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, resurrector, 1, "Resurrector's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, resurrectorImage));
        _monsterCards.Add(new SpeedCard(1f, resurrector, 2, "Resurrector's walk speed increases by 1", "Walk", increaseWalkSpeedImage, resurrectorImage));
        _monsterCards.Add(new SpeedCard(1.5f, resurrector, 3, "Resurrector's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, resurrectorImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, teleporter, 1, "Teleporter's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, teleporterImage));
        _monsterCards.Add(new SpeedCard(1f, teleporter, 2, "Teleporter's walk speed increases by 1", "Walk", increaseWalkSpeedImage, teleporterImage));
        _monsterCards.Add(new SpeedCard(1.5f, teleporter, 3, "Teleporter's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, teleporterImage));
        
        _monsterCards.Add(new SpeedCard(0.5f, wizard, 1, "Wizard's walk speed increases by 0.5", "Walk", increaseWalkSpeedImage, wizardImage));
        _monsterCards.Add(new SpeedCard(1f, wizard, 2, "Wizard's walk speed increases by 1", "Walk", increaseWalkSpeedImage, wizardImage));
        _monsterCards.Add(new SpeedCard(1.5f, wizard, 3, "Wizard's walk speed increases by 1.5", "Walk", increaseWalkSpeedImage, wizardImage));
    }
    void InitialMonsterBulletCard()
    {
        /*_monsterCards.Add(new BulletCard(0.5f, archer, 1, "Archer's projectile speed increases by 0.5", "Speed", increaseBulletSpeedImage, arrowProjectileImage));
        _monsterCards.Add(new BulletCard(1f, archer, 2, "Archer's projectile speed increases by 1", "Speed", increaseBulletSpeedImage, arrowProjectileImage));
        _monsterCards.Add(new BulletCard(1.5f, archer, 3, "Archer's projectile speed increases by 1.5", "Speed", increaseBulletSpeedImage, arrowProjectileImage));
        
        _monsterCards.Add(new BulletCard(0.5f, boomerang, 1, "Boomerang thrower's projectile speed increases by 0.5", "Speed", increaseBulletSpeedImage, boomerangProjectileImage));
        _monsterCards.Add(new BulletCard(1f, boomerang, 2, "Boomerang thrower's projectile speed increases by 1", "Speed", increaseBulletSpeedImage, boomerangProjectileImage));
        _monsterCards.Add(new BulletCard(1.5f, boomerang, 3, "Boomerang thrower's projectile speed increases by 1.5", "Speed", increaseBulletSpeedImage, boomerangProjectileImage));
        
        _monsterCards.Add(new BulletCard(0.5f, demon, 1, "Demon's projectile speed increases by 0.5", "Speed", increaseBulletSpeedImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(1f, demon, 2, "Demon's projectile speed increases by 1", "Speed", increaseBulletSpeedImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(1.5f, demon, 3, "Demon's projectile speed increases by 1.5", "Speed", increaseBulletSpeedImage, demonProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, wizard, 1, "Wizard's projectile speed increases by 1", "Speed", increaseBulletSpeedImage, monsterProjectileImage));
        _monsterCards.Add(new BulletCard(2f, wizard, 2, "Wizard's projectile speed increases by 2", "Speed", increaseBulletSpeedImage, monsterProjectileImage));
        _monsterCards.Add(new BulletCard(3f, wizard, 3, "Wizard's projectile speed increases by 3", "Speed", increaseBulletSpeedImage, monsterProjectileImage));
        
        _monsterCards.Add(new BulletCard(2.5f, archer, 1, "Archer's projectile max distance increases by 2.5", "Distance", increaseBulletDistanceImage, arrowProjectileImage));
        _monsterCards.Add(new BulletCard(5f, archer, 2, "Archer's projectile max distance increases by 5", "Distance", increaseBulletDistanceImage, arrowProjectileImage));
        _monsterCards.Add(new BulletCard(7.5f, archer, 3, "Archer's projectile max distance increases by 7.5", "Distance", increaseBulletDistanceImage, arrowProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, boomerang, 1, "Boomerang thrower's projectile max distance increases by 1", "Distance", increaseBulletDistanceImage, boomerangProjectileImage));
        _monsterCards.Add(new BulletCard(2f, boomerang, 2, "Boomerang thrower's projectile max distance increases by 2", "Distance", increaseBulletDistanceImage, boomerangProjectileImage));
        _monsterCards.Add(new BulletCard(3f, boomerang, 3, "Boomerang thrower's projectile max distance increases by 3", "Distance", increaseBulletDistanceImage, boomerangProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, demon, 1, "Demon's projectile max distance increases by 1", "Distance", increaseBulletDistanceImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(2f, demon, 2, "Demon's projectile max distance increases by 2", "Distance", increaseBulletDistanceImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(3f, demon, 3, "Demon's projectile max distance increases by 3", "Distance", increaseBulletDistanceImage, demonProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, wizard, 1, "Wizard's projectile max distance increases by 1", "Distance", increaseBulletDistanceImage, monsterProjectileImage));
        _monsterCards.Add(new BulletCard(2f, wizard, 2, "Wizard's projectile max distance increases by 2", "Distance", increaseBulletDistanceImage, monsterProjectileImage));
        _monsterCards.Add(new BulletCard(3f, wizard, 3, "Wizard's projectile max distance increases by 3", "Distance", increaseBulletDistanceImage, monsterProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, archer, 1, "Archer's projectile damage increases by 1", "Damage", increaseBulletDamageImage, arrowProjectileImage));
        _monsterCards.Add(new BulletCard(2f, archer, 2, "Archer's projectile damage increases by 2", "Damage", increaseBulletDamageImage, arrowProjectileImage));
        _monsterCards.Add(new BulletCard(3f, archer, 3, "Archer's projectile damage increases by 3", "Damage", increaseBulletDamageImage, arrowProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, boomerang, 1, "Boomerang thrower's projectile damage increases by 1", "Damage", increaseBulletDamageImage, boomerangProjectileImage));
        _monsterCards.Add(new BulletCard(2f, boomerang, 2, "Boomerang thrower's projectile damage increases by 2", "Damage", increaseBulletDamageImage, boomerangProjectileImage));
        _monsterCards.Add(new BulletCard(3f, boomerang, 3, "Boomerang thrower's projectile damage increases by 3", "Damage", increaseBulletDamageImage, boomerangProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, demon, 1, "Demon's projectile and its effect damage increases by 1", "Damage", increaseBulletDamageImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(2f, demon, 2, "Demon's projectile and its effect damage increases by 2", "Damage", increaseBulletDamageImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(3f, demon, 3, "Demon's projectile and its effect damage increases by 3", "Damage", increaseBulletDamageImage, demonProjectileImage));
        
        _monsterCards.Add(new BulletCard(1f, wizard, 1, "Wizard's projectile damage increases by 1", "Damage", increaseBulletDamageImage, monsterProjectileImage));
        _monsterCards.Add(new BulletCard(2f, wizard, 2, "Wizard's projectile damage increases by 2", "Damage", increaseBulletDamageImage, monsterProjectileImage));
        _monsterCards.Add(new BulletCard(3f, wizard, 3, "Wizard's projectile damage increases by 3", "Damage", increaseBulletDamageImage, monsterProjectileImage));
        
        _monsterCards.Add(new BulletCard(0.1f, demon, 1, "Demon's projectile effect zone increases by 10%", "Effect", increaseBulletEffectImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(0.2f, demon, 2, "Demon's projectile effect zone increases by 20%", "Effect", increaseBulletEffectImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(0.3f, demon, 3, "Demon's projectile effect zone increases by 30%", "Effect", increaseBulletEffectImage, demonProjectileImage));
        
        _monsterCards.Add(new BulletCard(0.05f, resurrector, 1, "Altar's effect zone increases by 5%", "Effect", increaseBulletEffectImage, resurrectorImage));
        _monsterCards.Add(new BulletCard(0.1f, resurrector, 2, "Altar's effect zone increases by 10%", "Effect", increaseBulletEffectImage, resurrectorImage));
        _monsterCards.Add(new BulletCard(0.15f, resurrector, 3, "Altar's effect zone increases by 15%", "Effect", increaseBulletEffectImage, resurrectorImage));*/
        
        _monsterCards.Add(new BulletCard(1f, demon, 1, "Demon's projectile effect zone exist time increases by 1s", "Time", increaseBulletEffectImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(2f, demon, 2, "Demon's projectile effect zone exist time increases by 2s", "Time", increaseBulletEffectImage, demonProjectileImage));
        _monsterCards.Add(new BulletCard(3f, demon, 3, "Demon's projectile effect zone exist time increases by 3s", "Time", increaseBulletEffectImage, demonProjectileImage));
        
        _monsterCards.Add(new BulletCard(0.5f, resurrector, 1, "Altar's effect zone exist time increases by 0.5s", "Time", increaseBulletEffectImage, resurrectorImage));
        _monsterCards.Add(new BulletCard(1f, resurrector, 2, "Altar's effect zone exist time increases by 1s", "Time", increaseBulletEffectImage, resurrectorImage));
        _monsterCards.Add(new BulletCard(1.5f, resurrector, 3, "Altar's effect zone exist time increases by 1.5s", "Time", increaseBulletEffectImage, resurrectorImage));
    }
    void InitialMonsterCards()
    {
        //InitialMonsterHealthCard();
        //InitialMonsterEnergyCard();
        //InitialMonsterSpeedCard();
        InitialMonsterBulletCard();
    }
    
    Card GetRandomPlayerCard()
    {
        if (_playerCards.Count == 0)
        {
            InitialPlayerCards();
        }
        
        Shuffle(_playerCards);
        int index = Random.Range(0, _playerCards.Count);
        if (_playerCards[index].Type == "Cooldown" || _playerCards[index].Type == "Damage" || _playerCards[index].Type == "Walk")
        {
            var i = 0;
            
            while (_playerCards[index].Type == "Cooldown")
            {
                i++;
                
                if (_playerCards[index] is EnergyCard)
                {
                    var energyCard = (EnergyCard)_playerCards[index];
                    var currentValue = player.gameObject.GetComponent<PlayerController>().GetStaminaCooldownMax();
                    var newValue = currentValue * (1 - energyCard.GetBuffParameter());
                
                    if (newValue >= 0.5)
                    {
                        _playerCards.RemoveAt(index);
                        return energyCard;
                    }
                    else
                    {
                        Shuffle(_playerCards);
                        index = Random.Range(0, _playerCards.Count);
                    }   
                }
                
                if (_playerCards[index] is BulletCard)
                {
                    var bulletCard = (BulletCard)_playerCards[index];
                    
                    if (bulletCard.TargetImage == normalProjectileImage)
                    {
                        bulletCard.SetBulletIndex(0);
                    } else if (bulletCard.TargetImage == freezeProjectileImage)
                    {
                        bulletCard.SetBulletIndex(1);
                    } else if (bulletCard.TargetImage == missleProjectileImage)
                    {
                        bulletCard.SetBulletIndex(2);
                    }

                    var currentValue = player.gameObject.GetComponent<PlayerController>().GetReloadTime(bulletCard.GetBulletIndex());
                    var newValue = currentValue * (1 - bulletCard.GetBuffParameter());
                
                    if (newValue >= 0.5)
                    {
                        _playerCards.RemoveAt(index);
                        return bulletCard;
                    }
                    else
                    {
                        Shuffle(_playerCards);
                        index = Random.Range(0, _playerCards.Count);
                    }   
                }

                if (i == _maxPlayerCardIteration)
                {
                    InitialPlayerCards();
                }
            }

            while (_playerCards[index].Type == "Damage")
            {
                i++;
                
                var bulletCard = (BulletCard)_playerCards[index];
                    
                if (bulletCard.TargetImage == normalProjectileImage)
                {
                    bulletCard.SetBulletIndex(0);
                } else if (bulletCard.TargetImage == freezeProjectileImage)
                {
                    bulletCard.SetBulletIndex(1);
                } else if (bulletCard.TargetImage == missleProjectileImage)
                {
                    bulletCard.SetBulletIndex(2);
                }

                var currentValue = player.gameObject.GetComponent<PlayerController>().GetProjectileDamage(bulletCard.GetBulletIndex());
                var newValue = currentValue + bulletCard.GetBuffParameter();
                
                if (newValue <= 10)
                {
                    _playerCards.RemoveAt(index);
                    return bulletCard;
                }
                else
                {
                    Shuffle(_playerCards);
                    index = Random.Range(0, _playerCards.Count);
                }
                
                if (i == _maxPlayerCardIteration)
                {
                    InitialPlayerCards();
                }
            } 
            
            while (_playerCards[index].Type == "Walk")
            {
                i++;
                
                var speedCard = (SpeedCard)_playerCards[index];
                var walkSpeed = player.gameObject.GetComponent<PlayerController>().GetWalkSpeed();
                var runSpeed = player.gameObject.GetComponent<PlayerController>().GetRunSpeed();
                var newWalkSpeed = walkSpeed + speedCard.GetBuffParameter();
                var newValue = runSpeed - newWalkSpeed;
                
                if (newValue >= 3)
                {
                    _playerCards.RemoveAt(index);
                    return speedCard;
                }
                else
                {
                    Shuffle(_playerCards);
                    index = Random.Range(0, _playerCards.Count);
                }
                
                if (i == _maxPlayerCardIteration)
                {
                    InitialPlayerCards();
                }
            }
            
            if (_playerCards[index] is BulletCard)
            {
                var bulletCard = (BulletCard)_playerCards[index];
                
                if (bulletCard.TargetImage == normalProjectileImage)
                {
                    bulletCard.SetBulletIndex(0);
                } else if (bulletCard.TargetImage == freezeProjectileImage)
                {
                    bulletCard.SetBulletIndex(1);
                } else if (bulletCard.TargetImage == missleProjectileImage)
                {
                    bulletCard.SetBulletIndex(2);
                }
            }
            
            var returnCard = _playerCards[index];
            _playerCards.RemoveAt(index);
            return returnCard;
        }
        else
        {
            if (_playerCards[index] is BulletCard)
            {
                var bulletCard = (BulletCard)_playerCards[index];
                
                if (bulletCard.TargetImage == normalProjectileImage)
                {
                    bulletCard.SetBulletIndex(0);
                } else if (bulletCard.TargetImage == freezeProjectileImage)
                {
                    bulletCard.SetBulletIndex(1);
                } else if (bulletCard.TargetImage == missleProjectileImage)
                {
                    bulletCard.SetBulletIndex(2);
                }
            }
            
            var returnCard = _playerCards[index];
            _playerCards.RemoveAt(index);
            return returnCard;   
        }
    }
    Card GetRandomMonsterCard(int level)
    {
        if (_monsterCards.Count == 0)
        {
            InitialMonsterCards();
        }
        
        List<Card> cardsByLevel = new List<Card>();
        foreach (Card card in _monsterCards)
        {
            if (card.Level == level)
            {
                cardsByLevel.Add(card);
            }
        }
        for (int i = _monsterCards.Count - 1; i >= 0; i--)
        {
            if (_monsterCards[i].Level == level)
            {
                _monsterCards.RemoveAt(i);
            }
        }

        if (cardsByLevel.Count == 0)
        {
            InitialMonsterCards();
            cardsByLevel = new List<Card>();
            foreach (Card card in _monsterCards)
            {
                if (card.Level == level)
                {
                    cardsByLevel.Add(card);
                }
            }
            for (int i = _monsterCards.Count - 1; i >= 0; i--)
            {
                if (_monsterCards[i].Level == level)
                {
                    _monsterCards.RemoveAt(i);
                }
            }
        }
        
        Shuffle(cardsByLevel);
        int index = Random.Range(0, cardsByLevel.Count);
        if (cardsByLevel[index].Type == "Cooldown")
        {
            var i = 0;
            
            while (cardsByLevel[index].Type == "Cooldown")
            {
                i++;
                
                var energyCard = (EnergyCard)cardsByLevel[index];
                var target = energyCard.Target;
                    
                var currentValue = target.gameObject.GetComponent<MonsterController>().monsterParameter.attackCooldown;
                var newValue = currentValue * (1 - energyCard.GetBuffParameter());
                if (newValue >= 0.5)
                {
                    _monsterCards.AddRange(cardsByLevel);
                    RemoveSameOriginCard(cardsByLevel[index], _monsterCards);
                    return energyCard;
                }
                else
                {
                    Shuffle(cardsByLevel);
                    index = Random.Range(0, cardsByLevel.Count);
                }
                
                if (i == _maxMonsterCardIteration)
                {
                    InitialMonsterCards();
                }
            }
                
            var returnCard = cardsByLevel[index];
            _monsterCards.AddRange(cardsByLevel);
            RemoveSameOriginCard(returnCard, _monsterCards);
            return returnCard;
        }
        else
        {
            var returnCard = cardsByLevel[index];
            _monsterCards.AddRange(cardsByLevel);
            RemoveSameOriginCard(returnCard, _monsterCards);
            return returnCard;   
        }
    }

    void RemoveSameOriginCard(Card card, List<Card> cards)
    {
        var type = card.GetType();
        var target = card.TargetImage;
        cards.RemoveAll(tempCard => (tempCard.GetType() == type && tempCard.TargetImage == target));
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

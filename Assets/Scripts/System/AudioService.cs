using CardSystem;
using EventBusSystem;
using TileSystem;
using UnityEngine;

public class AudioService : IService, 
    IPlayerHoldsCardHandler, 
    IGameEndHandler, 
    ICardEquipedHandler, 
    INestDestroyed
{
    private AudioClip playerPickCard;
    private AudioClip cardEquip;
    private AudioClip defeat;
    private AudioClip nestBreack;
    private AudioClip nestBuild;
    private AudioClip Victory;
    private AudioClip mainTheme;

    private AudioSource _audioSource;

    public void StartWork()
    {
        EventBus.Subscribe(this);
        CreateAudioSourse();
        LoadAudioClips();
        PlayMainTheme();
    }

    public void EndWork()
    {
        _audioSource.Stop();
        Object.Destroy(_audioSource);
    }

    public void CardEquiped(ICard card, ICard previousCard)
    {
        _audioSource.PlayOneShot(cardEquip);
    }

    public void OnNestDestroyed(Region region, TerrainCell cell)
    {
        _audioSource.PlayOneShot(nestBreack);
    }

    public void PlayerLose()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(defeat);
    }

    public void PlayerStartHoldCard()
    {
        _audioSource.PlayOneShot(playerPickCard);
    }

    public void PlayerStopHoldCard()
    {

    }

    public void PlayerWin()
    {
        if (_audioSource != null)
            _audioSource.PlayOneShot(Victory);
    }

    private void CreateAudioSourse()
    {
        _audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
    }

    private void LoadAudioClips()
    {
        playerPickCard = Resources.Load<AudioClip>("Audio/PickCard");
        cardEquip = Resources.Load<AudioClip>("Audio/CardEquip");
        defeat = Resources.Load<AudioClip>("Audio/Defeat");
        nestBreack = Resources.Load<AudioClip>("Audio/NestBreack");
        nestBuild = Resources.Load<AudioClip>("Audio/nestBuild");
        Victory = Resources.Load<AudioClip>("Audio/Victory");
        mainTheme = Resources.Load<AudioClip>("Audio/Saund1");
    }

    private void PlayMainTheme()
    {
        _audioSource.clip = mainTheme;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}

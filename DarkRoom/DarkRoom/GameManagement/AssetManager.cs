using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

public class AssetManager
{
    //Used for loading content and playing sound
    protected ContentManager contentManager;
    protected Song[] songs;
    protected int currentSong, previousSong;
    protected float sfxVolume;

    public AssetManager(ContentManager Content)
    {
        this.contentManager = Content;
        songs = new Song[5];
        currentSong = 0;
        previousSong = 0;
        MediaPlayer.Volume = 0.5f;
        sfxVolume = 1f;
    }

    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
            return null;
        return contentManager.Load<Texture2D>(assetName);
    }

    public void PlaySound(string assetName, float volume, bool loop = false)
    {
        SoundEffect soundEffect = contentManager.Load<SoundEffect>(assetName);
        soundEffect.Play(volume * sfxVolume, 0, 0);
    }

    public SoundEffectInstance GetSound(string assetName, float volume)
    {
        SoundEffect soundEffect = contentManager.Load<SoundEffect>(assetName);
        SoundEffectInstance soundInstance;
        soundInstance = soundEffect.CreateInstance();
        soundInstance.Volume = volume * sfxVolume;
        return soundInstance;
    }

    public SoundEffectInstance GetLoopSound(string assetName, float volume)
    {
        SoundEffect soundEffect = contentManager.Load<SoundEffect>(assetName);
        SoundEffectInstance soundInstance;
        soundInstance = soundEffect.CreateInstance();
        soundInstance.Volume = volume * sfxVolume;
        soundInstance.IsLooped = true;
        return soundInstance;
    }

    public void PlayMusic(string assetName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(contentManager.Load<Song>(assetName));
    }

    public void PlayGameMusic()
    {
        int nextSong = GameEnvironment.Random.Next(0, 5);
        if (nextSong == currentSong)
        {
            PlayGameMusic();
            return;
        }
        else
        {
            MediaPlayer.Play(songs[nextSong]);
            currentSong = nextSong;
        }

    }

    public void AddGameMusic(string assetName, int arrayIndex)
    {
        songs[arrayIndex] = contentManager.Load<Song>(assetName);
    }

    public ContentManager Content
    {
        get { return contentManager; }
    }

    public MediaState MediaState
    {
        get { return MediaPlayer.State; }
    }

    public float SFXVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = value; }
    }
}
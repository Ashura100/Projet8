using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicGame;
    public AudioSource sfxPlayer;

    [SerializeField]
    private List<AudioClip> playlist; // Liste des musiques pour la playlist
    private int currentTrackIndex = 0;

    [SerializeField]
    private TextMeshProUGUI musicTitleText; // Référence au TextMeshPro pour afficher le titre de la musique

    [SerializeField]
    private AudioClip carSound, klaxonSound;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    // Détermine quelle musique jouer en fonction du nom de la scène
    private void PlayMusicForScene(string sceneName)
    {
        if (sceneName == "New Scene")
        {
            PlayTrack(0); // Par exemple, jouer le premier morceau
        }
        else
        {
            StopCurrentSound();
        }
    }

    // Joue une piste spécifique dans la playlist
    public void PlayTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < playlist.Count)
        {
            currentTrackIndex = trackIndex;
            musicGame.clip = playlist[trackIndex];
            musicGame.Play();
            UpdateMusicTitle(); // Met à jour le titre de la musique
        }
        else
        {
            Debug.LogWarning("Track index out of bounds");
        }
    }

    // Passe à la piste suivante dans la playlist
    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % playlist.Count;
        PlayTrack(currentTrackIndex);
    }

    // Revient à la piste précédente dans la playlist
    public void PlayPreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + playlist.Count) % playlist.Count;
        PlayTrack(currentTrackIndex);
    }

    // Met à jour le titre de la musique dans le TextMeshPro
    private void UpdateMusicTitle()
    {
        if (musicTitleText != null && playlist[currentTrackIndex] != null)
        {
            musicTitleText.text = playlist[currentTrackIndex].name; // Affiche le nom du fichier audio
        }
    }

    public void PlayEngine()
    {
        sfxPlayer.clip = carSound;
        sfxPlayer.Play();
    }

    public void PlayHorn()
    {
        sfxPlayer.clip = klaxonSound;
        sfxPlayer.Play();
    }

    // Arrête la musique en cours
    public void StopCurrentSound()
    {
        musicGame.Stop();
        if (musicTitleText != null)
        {
            musicTitleText.text = ""; // Vide le texte si aucune musique ne joue
        }
    }

    // Permet de lire ou d'arrêter la musique en fonction de son état actuel
    public void TogglePlayPause()
    {
        if (musicGame.isPlaying)
        {
            musicGame.Pause();
        }
        else
        {
            musicGame.UnPause();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
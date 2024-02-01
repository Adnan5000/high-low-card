using System;
using System.Collections.Generic;
using System.Linq;
using Arch.AssetReferences;
using Arch.Signals;
using Arch.SoundManager.Config;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Object = UnityEngine.Object;

namespace Arch.SoundManager
{
    public class SoundManager : ISoundManager, IInitializable
    {
        private const string EffectsMixerKey = "EffectsMixer";
        private const string MusicMixerKey = "MusicMixer";
        private const string VoiceMixerKey = "VoiceMixer";
        
        private AudioSource _musicSource;
        private AudioMixer _masterMixer;
        private SoundManagerModel _soundManagerModel = new SoundManagerModel();

        public string CurrentMusicClip { get; private set; }

        private IAssetReferenceDownloader _assetReferenceDownloader;

        //private ICachingService _cachingService;
        private SoundsConfig _soundsConfig;

        private readonly List<AudioSource> _activeAudioSources;

        private string _musicClipNow;
        private bool _startMusicTrack;
        
        private AudioSource _clipInstance;

        public AudioMixerGroup EffectsOutputGroup { get; private set; }
        public AudioMixerGroup MusicOutputGroup { get; private set; }
        public AudioMixerGroup VoiceOutputGroup { get; private set; }

        private ISignalService _signalService;
        [Inject]
        public SoundManager(ISignalService SignalService)
        {
            _activeAudioSources = new List<AudioSource>();
            _signalService = SignalService;
            //_signalService.Receive<PlayAudioClipAddressableSignal>().Subscribe(PlayAddressableSound);
        }

        // private void PlayAddressableSound(PlayAudioClipAddressableSignal audioSignal)
        // {
        //    PlayAudioClip(new AudioClipManagerModel()
        //     {
        //         ClipName = audioSignal.ClipName,
        //         ClipType = audioSignal.ClipType
        //     });
        // }

        public AudioSource MusicSource
        {
            set => _musicSource = value;
        }

        public AudioMixer MasterMixer
        {
            set
            {
                _masterMixer = value;
                EffectsOutputGroup = _masterMixer.FindMatchingGroups(EffectsMixerKey)[0];
                MusicOutputGroup = _masterMixer.FindMatchingGroups(MusicMixerKey)[0];
                VoiceOutputGroup = _masterMixer.FindMatchingGroups(VoiceMixerKey)[0];
            }
        }

        public SoundsConfig SoundConfig
        {
            set => _soundsConfig = value; 
        }
        
        public bool SoundOn {
            get => _soundManagerModel.soundVolumeOn;
            set {
                if(_masterMixer == null)
                    return;
                
                _soundManagerModel.soundVolumeOn = value;
                _masterMixer.SetFloat("SoundVolume", _soundManagerModel.soundVolumeOn ? _soundManagerModel.soundVolume : -60f);
            }
        }
        
        
        public bool MusicOn {
            get => _soundManagerModel.musicVolumeOn;
            set {
                if(_masterMixer == null)
                    return;
                
                _soundManagerModel.musicVolumeOn = value;
                _masterMixer.SetFloat("MusicVolume", _soundManagerModel.musicVolumeOn ? _soundManagerModel.musicVolume : -60f);
            }
        }

        public float SoundVolume {
            get => _soundManagerModel.soundVolume;
            set
            {
                if (_masterMixer == null)
                    return;
                _soundManagerModel.soundVolume = value;
                _masterMixer.SetFloat("SoundVolume",
                    _soundManagerModel.soundVolumeOn ? (Mathf.Log(_soundManagerModel.soundVolume) * 20f)+1 : -80f);
            }
        }
        
        public float MusicVolume {
            get => _soundManagerModel.musicVolume;
            set
            {
                if (_masterMixer == null)
                    return;

                _soundManagerModel.musicVolume = value;
                _masterMixer.SetFloat("MusicVolume",
                    _soundManagerModel.musicVolumeOn ? (Mathf.Log(_soundManagerModel.musicVolume) * 20f)+1 : -80f);
            }
        }

        [Inject]
        public void Init(
            IAssetReferenceDownloader assetReferenceDownloader
            //ICachingService cachingService
            )
        {
            _assetReferenceDownloader = assetReferenceDownloader;
           // _cachingService = cachingService;
        }

        public void Initialize()
        {
            // _cachingService.Read<SoundManagerModel>("SoundVolume", value =>
            // {
            //     _soundManagerModel = value ?? new SoundManagerModel();
            // });
        }
        
        public string GetMusicClipNow()
        {
            return _musicClipNow;
        }

        public void StopPlayAudio(AudioSource clipSource)
        {
            Delete2DAudioClip(clipSource);
        }

        
        public void PlayAudioClip(AudioClipManagerModel clipInfo)
        {
            if (_soundsConfig == null) return;
            AudioClipConfig clip;
            if (_soundsConfig.ClipsDictionary.TryGetValue(clipInfo.ClipName, out clip))
            {
                if (clip.Reference != null)
                {
                    PlayExistedAudioClip(clipInfo, clip);
                    return;
                }
            }

            _assetReferenceDownloader.DownloadAudioById(clipInfo.ClipName, model =>
            {
                if (model == null)
                {
                    Debug.LogError($"No clip config found for {clipInfo.ClipName} audio clip type");
                    return;
                }

                PlayExistedAudioClip(clipInfo, new AudioClipConfig()
                {
                    Reference = model,
                    OutputAudioMixerGroup = EffectsOutputGroup
                });
            });
        }


        private void PlayExistedAudioClip(AudioClipManagerModel clipInfo, AudioClipConfig info)
        {
            var config = new ClipInfo
            {
                Clip = info.Reference,
                Volume = info.Volume,
                OutputAudioMixerGroup = info.OutputAudioMixerGroup,
                SourceCallback = clipInfo.Source,
                MaxSimultaneouslyCalls = info.MaxSimultaneouslyCalls
            };
            
            switch (clipInfo.ClipType)
            {
                case SoundType.Default:
                    PlayAudioClip(config, clipInfo.Pitch);
                    break;
                case SoundType.Loop:
                    PlayAudioClipLoop(config, clipInfo.Pitch, clipInfo.PitchTime);
                    break;
                case SoundType.Singleton:
                    if (clipInfo.Delay > 0)
                        PlayAudioClipSingleton(config, clipInfo.Delay);
                    else
                        PlayAudioClipSingleton(config);
                    break;
                case SoundType.Delayed:
                    PlayAudioClipDelayed(config, clipInfo.Delay);
                    break;
                case SoundType.Music:
                    if (_startMusicTrack) return;
                    _startMusicTrack = true;
                   PlayMusic(config);
                    break;
                default:
                    PlayAudioClip(config);
                    break;
            }
        }

        private void PlayAudioClip(ClipInfo clip, float pitch = 1)
        {
           if (!SoundOn) return;
     
            if (clip.Clip == null)
                return;

            var soundsPlaying = _activeAudioSources.Count(a => a != null && a.clip == clip.Clip);
            if(soundsPlaying >= clip.MaxSimultaneouslyCalls) return;

            CreateAndPlay2DAudioClip(clip, false, false, pitch);
        }

        private void PlayAudioClipLoop(ClipInfo clip, float pitch = 1f, float pitchTime = 0.2f) {
            if (!SoundOn) return;
     
            if (clip.Clip == null)
                return;
            int index = _activeAudioSources.FindIndex(audio => audio != null && audio.clip == clip.Clip);

            if (index != -1) {
                if (pitch != _activeAudioSources[index].pitch)
                {
                    //todo needs dotween
                    /*
                    _activeAudioSources[index].DOPitch(pitch, pitchTime);
                    clip.SourceCallback?.Invoke(_activeAudioSources[index]);
                    */
                }
            }
            else {
                CreateAndPlay2DAudioClip(clip, false, true, pitch);
            }
        }
        
        private void PlayAudioClipSingleton(ClipInfo clip) {
            if (!SoundOn) return;

            if (clip.Clip == null)
                return;

            if (_clipInstance != null) {
                _activeAudioSources.Remove(_clipInstance);
                Object.Destroy(_clipInstance.gameObject);
            }

            _clipInstance = CreateAndPlay2DAudioClip(clip, true);
        }

        private void PlayAudioClipSingleton(ClipInfo clip, float delay) {
            
            if (!SoundOn || clip.Clip == null) return;
            Observable.Timer(TimeSpan.FromSeconds(delay))
                .Subscribe(_ =>
                {
                    if (_clipInstance != null)
                    {
                        _activeAudioSources.Remove(_clipInstance);
                        Object.Destroy(_clipInstance.gameObject);
                    }
                    _clipInstance = CreateAndPlay2DAudioClip(clip, true);
                });
        }
        
        private AudioSource CreateAndPlay2DAudioClip(ClipInfo clipConfig, bool cancelWithGo = false,
            bool isOnLoop = false, float pitch = 1f)
        {
            var audioSource = new GameObject($"{clipConfig.Clip.name}").AddComponent<AudioSource>();
            
            audioSource.playOnAwake = false;
            audioSource.loop = isOnLoop;
            audioSource.spatialize = false;
            audioSource.clip = clipConfig.Clip;
            audioSource.volume = clipConfig.Volume; 
            audioSource.outputAudioMixerGroup = clipConfig.OutputAudioMixerGroup;
            audioSource.pitch = pitch;
            audioSource.Play();

            _activeAudioSources.Add(audioSource);
            
            DestroyClipOnFinish(audioSource, clipConfig);
            clipConfig.SourceCallback?.Invoke(audioSource);
            return audioSource;
        }

        private void Delete2DAudioClip(AudioSource source) {
            if ( source == null || _activeAudioSources == null || _activeAudioSources.Count == 0)
                return;

            if (!_activeAudioSources.Contains(source))
                return;
            
            _activeAudioSources.Remove(source);
            Object.Destroy(source.gameObject);
        }

        private void DestroyClipOnFinish(AudioSource audioSource, ClipInfo clipConfig)
        {
            Observable.Timer(TimeSpan.FromSeconds(clipConfig.Clip.length))
                .Subscribe(_ =>
                {
                    CompositeDisposable disposeOnExit = new CompositeDisposable();
                    Observable.Timer(TimeSpan.FromTicks(1))
                        .Repeat()
                        .Subscribe(_ =>
                        {
                            if (audioSource != null && !audioSource.isPlaying) {
                                _activeAudioSources.Remove(audioSource);
                                Object.Destroy(audioSource.gameObject);
                                disposeOnExit.Dispose();
                            }
                        }).AddTo(disposeOnExit);
                });
            
        }
        
        /*
        private IEnumerator<float> DestroyClipOnFinish(AudioSource audioSource, ClipInfo clipConfig) {
            yield return Timing.WaitForSeconds(clipConfig.Clip.length);
            if (audioSource == null) yield break;
            if (!audioSource.isPlaying) {
                _activeAudioSources.Remove(audioSource);
                Object.Destroy(audioSource.gameObject);
            }
            else {
                while (audioSource != null && audioSource.isPlaying) {
                    yield return Timing.WaitForOneFrame;
                }

                if (audioSource == null) yield break;
                _activeAudioSources.Remove(audioSource);
                Object.Destroy(audioSource.gameObject);
            }
        }
        */
        
        private void PlayAudioClipDelayed(ClipInfo clipConfig, float delay) {
            if (!SoundOn || clipConfig == null || clipConfig.Clip == null) return;
            
            Observable.Timer(TimeSpan.FromSeconds(delay))
                .Subscribe(_ =>
                {
                    CreateAndPlay2DAudioClip(clipConfig);
                });
        }
        
        private void PlayMusic(ClipInfo clip)
        {
            _musicSource.Stop();
            _musicSource.clip = clip.Clip;
            _musicSource.volume = clip.Volume;
            _musicSource.outputAudioMixerGroup = clip.OutputAudioMixerGroup;
            _musicSource.loop = true;
            CurrentMusicClip = clip.Clip.name;
            _musicSource.Play();
        }

        public void StopMusic() {
            _musicSource.Stop();
            _musicSource.clip = null;
        }

        public void DestroyActiveAudioSources()
        {
            foreach (var source in _activeAudioSources.Where(source => source != null))
            {
                Object.Destroy(source.gameObject);
            }

            _activeAudioSources.Clear();
        }

    }
}
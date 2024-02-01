using System;
using System.Collections.Generic;
using Arch.AssetReferences.Signals;
using Arch.Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using Zenject;
using Object = UnityEngine.Object;

namespace Arch.AssetReferences
{
    public class AssetReferenceDownloader : IAssetReferenceDownloader
    {
        private static int _maxUploaders = 10;
        private readonly List<string> _uploadingPool = new List<string>();
        private readonly List<UploadingModel> _workers = new List<UploadingModel>();

        private int _inUploading;

        public bool AllUploded => _workers.Count == 0;
        public int WorkersCount => _workers.Count;

        private ISignalService _signalService;

        [Inject]
        public void Init(ISignalService signalService)
        {
            _signalService = signalService;
        }

        public void SpawnScriptableById(string id, Action<ScriptableObject> callback)
        {
            Debug.Log("SpawnScriptableById " + id);

            Addressables.LoadAssetAsync<ScriptableObject>(id).Completed += (obj =>
            {
                ScriptableObject myGameObject = obj.Result;
                callback?.Invoke(myGameObject);
            });
        }
        
        private void TryUpload()
        {
            _signalService.Publish(new AssetDownloaderProgressSignal(){Value = _workers.Count + _inUploading});
            if (_inUploading < _maxUploaders && _workers.Count > _inUploading )
            {
                UploadingModel uploadingModel = _workers[_inUploading];
                Debug.Log("TryUpload worker name " + uploadingModel.Id + ", type " + uploadingModel.Worker.ToString());
                uploadingModel.Worker.Invoke(uploadingModel);
                _workers.Remove(uploadingModel);
                _inUploading++;
            }
        }
        
        public void SpawnById(string id, Action<GameObject> callback)
        {
            if (id == null)
            {
                Debug.Log("Id couldn't be null");
                return;
            }
            UploadingModel uploadingModel = new UploadingModel
            {
                Id = id,
                CallbackGameObject = callback,
                Worker = SpawnByIdWorker
            };

            _workers.Add(uploadingModel);
            TryUpload();
        }

        private void SpawnByIdWorker(UploadingModel uploadingModel)
        {
            Debug.Log("SpawnByIdWorker try upload " + uploadingModel.Id);
            Addressables.LoadAssetAsync<GameObject>(uploadingModel.Id).Completed += (obj =>
            {
                if(!_uploadingPool.Contains(uploadingModel.Id))
                    _uploadingPool.Add(uploadingModel.Id);
                GameObject myGameObject = obj.Result;
                uploadingModel.CallbackGameObject?.Invoke(myGameObject);
                _inUploading--;
                TryUpload();
            });
        }

        public void SpawnSpriteById(string id, Action<Sprite> callback)
        {
            if (id == null)
            {
                Debug.Log("Id couldn't be null");
                return;
            }
            UploadingModel uploadingModel = new UploadingModel
            {
                Id = id,
                CallbackSprite = callback,
                Worker = SpawnSpriteByIdWorker
            };

            _workers.Add(uploadingModel);
            TryUpload();
        }

        private void SpawnSpriteByIdWorker(UploadingModel uploadingModel)
        {
            Addressables.LoadAssetAsync<Sprite>(uploadingModel.Id).Completed += (obj =>
            {
                if(!_uploadingPool.Contains(uploadingModel.Id))
                    _uploadingPool.Add(uploadingModel.Id);
                Sprite sprite = obj.Result;
                uploadingModel.CallbackSprite?.Invoke(sprite);
                _inUploading--;
                TryUpload();
            });
        }
        
        private void SpawnObjectById(string id, Action<Object> callback)
        {
            if (id == null)
            {
                Debug.Log("Id couldn't be null");
                return;
            }
            UploadingModel uploadingModel = new UploadingModel
            {
                Id = id,
                CallbackObject = callback,
                Worker = SpawnObjectByIdWorker
            };

            _workers.Add(uploadingModel);
            TryUpload();
        }

        private void SpawnObjectByIdWorker(UploadingModel uploadingModel)
        {
            Addressables.LoadAssetAsync<Object>(uploadingModel.Id).Completed += (obj =>
            {
                if(!_uploadingPool.Contains(uploadingModel.Id))
                    _uploadingPool.Add(uploadingModel.Id);
                Object result = obj.Result;
                uploadingModel.CallbackObject?.Invoke(result);
                _inUploading--;
                TryUpload();
            });
        }
        
        public void SpawnObjectByIdAndDispose(string id, Action<Object> callback)
        {
            if (id == null)
            {
                Debug.Log("Id couldn't be null");
                return;
            }
            UploadingModel uploadingModel = new UploadingModel
            {
                Id = id,
                CallbackObject = callback,
                Worker = SpawnObjectByIdAndDisposeWorker
            };

            _workers.Add(uploadingModel);
            TryUpload();
        }

        public void CheckPreloadUpdate(Action<bool> callbackResult)
        {
            Addressables.InitializeAsync().Completed += completed =>
            {
                Addressables.CheckForCatalogUpdates().Completed += checkForUpdates=>
                {
                    if(checkForUpdates.Status == AsyncOperationStatus.Failed)
                    {
                        Debug.LogWarning("Fetch failed!");
                    }
 
                    if (checkForUpdates.Result.Count > 0)
                    {
                        Debug.Log("Available Update:");
                        foreach(var update in checkForUpdates.Result)
                        {
                            Debug.Log(update);
                        }
                    }
                    else
                    {
                        Debug.LogError("No Available Update");
                    }
                
                    callbackResult?.Invoke(checkForUpdates.Result.Count > 0);
                };
            };
        }
        
        private void SpawnObjectByIdAndDisposeWorker(UploadingModel uploadingModel)
        {
            Addressables.LoadAssetAsync<Object>(uploadingModel.Id).Completed += (obj =>
            {
                if (!_uploadingPool.Contains(uploadingModel.Id))
                    _uploadingPool.Add(uploadingModel.Id);
                Object result = obj.Result;
                uploadingModel.CallbackObject?.Invoke(result);
                _inUploading--;
                Addressables.Release(obj);

                TryUpload();
            });
        }

        public void SpawnUnknownById(string id, 
            Action<Sprite> callbackSprite, 
            Action<GameObject> callbackGameObject,
            Action<SpriteAtlas> callbackSpriteAtlas = null)
        {
            SpawnObjectById(id, o =>
            {
                if (o is Sprite sprite)
                {
                    callbackSprite?.Invoke(sprite);
                }

                if (o is GameObject gameObject)
                {
                    callbackGameObject?.Invoke(gameObject);
                }

                if (o is Texture2D)
                {
                    SpawnSpriteById(id, callbackSprite);
                }

                if (o is SpriteAtlas spriteAtlas)
                {
                    callbackSpriteAtlas?.Invoke(spriteAtlas);
                }
            });
        }

        public void DownloadAudioById(string id, Action<AudioClip> callback)
        {
            UploadingModel uploadingModel = new UploadingModel
            {
                Id = id,
                CallbackObjectAudio = callback,
                Worker = SpawnAudioByIdWorker
            };
            _workers.Add(uploadingModel);
            TryUpload();
        }

        private void SpawnAudioByIdWorker(UploadingModel uploadingModel)
        {
            Addressables.LoadAssetAsync<AudioClip>(uploadingModel.Id).Completed += (obj =>
            {
                if(!_uploadingPool.Contains(uploadingModel.Id))
                    _uploadingPool.Add(uploadingModel.Id);
                AudioClip sprite = obj.Result;
                uploadingModel.CallbackObjectAudio?.Invoke(sprite);
                _inUploading--;
                TryUpload();
            });
        }
        
        private class UploadingModel
        {
            public string Id;
            public Action<UploadingModel> Worker;
            public Action<Sprite> CallbackSprite;
            public Action<GameObject> CallbackGameObject;
            public Action<Object> CallbackObject;
            public Action<AudioClip> CallbackObjectAudio;
        }
    }
}
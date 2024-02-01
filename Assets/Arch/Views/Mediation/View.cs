using System.Collections;
using UnityEngine;
using Zenject;

namespace Arch.Views.Mediation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class View: MonoBehaviour, IView
    {
        private IMediator _mediator;
        private CanvasGroup _canvasGroup;
        public float fadeDuration = 0.5f;

        public GameObject GetGameObject => this.gameObject;

        [Inject]
        public void Init(IMediator mediator)
        {
            _mediator = mediator;
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            StartCoroutine(FadeIn());
        }

        public virtual void Remove()
        {
            StartCoroutine(FadeOut());
        }
        
        IEnumerator FadeOut()
        {
            float elapsedTime = 0;

            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);

                _canvasGroup.alpha = alpha;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _canvasGroup.alpha = 0;
            
            Destroy(gameObject);
        }
        
        IEnumerator FadeIn()
        {
            _canvasGroup.alpha = 0;
            float elapsedTime = 0;

            while (elapsedTime < fadeDuration)
            {
                // Calculate the alpha value based on the elapsed time and fade duration
                float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);

                // Set the alpha value to the CanvasGroup
                _canvasGroup.alpha = alpha;

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                yield return null; // Wait for the next frame
            }

            // Ensure the alpha is set to 1 at the end to avoid any rounding errors
            _canvasGroup.alpha = 1;
        }

        protected virtual void OnEnable() => _mediator?.Enable();

        protected virtual void OnDisable() => _mediator?.Disable();

        protected virtual void OnDestroy() => _mediator?.Dispose();
    }
}
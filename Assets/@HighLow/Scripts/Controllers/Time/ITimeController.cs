using System.Threading.Tasks;

namespace HighLow.Scripts.Controllers.Time
{
    public interface ITimeController
    {
        public string ChoiceTimeText { get; }
        public Task StartTimer();
        public void StopTimer();
        public void ResetChoiceTimer();
    }
}
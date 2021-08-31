using System.Collections.Generic;
using UniRx;


namespace Code.Fight.BuildingDungeon
{
    public class BuildStatusCheckerController : BaseController
    {
        public ReactiveCommand CompleteCommand = new ReactiveCommand();
        private Queue<IVerifiable> _queue;
        private ReactiveProperty<int> _counter;

        public BuildStatusCheckerController()
        {
            _queue = new Queue<IVerifiable>();
            _counter = new ReactiveProperty<int>();
            _counter.Subscribe((count =>
            {
                // Dbg.Log($"Checking counter : {_counter.Value}");
                if (count <= 0)
                    CompleteCommand.Execute();
            })).AddTo(_subscriptions);
        }
        
        public void AddToQueue(IVerifiable verifiable)
        {
            _queue.Enqueue(verifiable);
            verifiable.Complete += VerifiableOnComplete;
            _counter.Value++;
        }

        private void VerifiableOnComplete(IVerifiable sender)
        {
            // Dbg.Log($"Sender'{sender}' changed value:{sender.Status}");
            // Dbg.Log($"Start checking, Queue.Count:{_queue.Count}");
            _counter.Value--;
            // foreach (var verifiable in _queue)
            // {
            // Dbg.Log($"{verifiable}.Status:{verifiable.Status}");
            // }
            // Dbg.Log($"finish checking");
        }

        public override void Dispose()
        {
            foreach (var item in _queue)
            {
                item.Complete -= VerifiableOnComplete;
            }
            _queue.Clear();
            base.Dispose();
        }
    }
}
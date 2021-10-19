using UniRx;


namespace Code.UI.UniversalTemplates{
    public class CellCommand<T>{
        private T _body;

        public T Body{
            get => _body;
            set => _body = value;
        }

        public ReactiveCommand<T> Command = new ReactiveCommand<T>();

        public CellCommand(T body){
            _body = body;
        }
    }
}
// Spotifei/Interfaces/INotificavel.cs
namespace Spotifei
{
    public interface INotificavel
    {
        void EnviarNotificacao(string mensagem);
        void ReceberNotificacao(string mensagem);
    }
}
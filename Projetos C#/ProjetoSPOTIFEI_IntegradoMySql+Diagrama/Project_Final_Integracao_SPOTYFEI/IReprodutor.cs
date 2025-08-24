

// Spotifei/Interfaces/IReprodutor.cs
namespace Spotifei
{
    public interface IReprodutor
    {
        void Reproduzir();
        void Pausar();
        void Avancar();
        void Retroceder();
        void AjustarVolume(int nivel);
    }
}
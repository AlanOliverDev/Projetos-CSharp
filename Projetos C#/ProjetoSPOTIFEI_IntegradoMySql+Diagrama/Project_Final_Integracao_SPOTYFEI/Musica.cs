namespace Spotifei
  {
      public class Musica : Conteudo
      {
          public string Album { get; set; } = string.Empty;
          public new string Artista { get; set; } = string.Empty; // Adicionado 'new'

          public override void Reproduzir()
          {
              Console.WriteLine($"Reproduzindo música: {Titulo} do álbum {Album} por {Artista}");
          }
      }
  }
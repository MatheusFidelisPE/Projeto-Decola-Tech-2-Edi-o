using Series.Enums;

namespace Series.Classes
{
    [Serializable]
    public class Filme : EntidadeBase
    {
        private List<Genero> Genero { get; set; }
        private string Titulo { get; set; }
        private string Descricao { get; set; }
        private decimal Bilheteria { get; set; }
        private decimal NotaOmelete { get; set; }
        private bool Excluido { get; set; }
        public Filme(int Id, string titulo, string descricao, decimal bilheteria, decimal notaOmelete, List<Genero> genero)
        {
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Bilheteria = bilheteria;
            this.NotaOmelete = notaOmelete;
            this.Genero = genero;
            this.Excluido = false;
        }
        public override string ToString()
        {   
            string retorno = "";
            retorno += "Título: " + this.Titulo;
            retorno += "Descrição: " + this.Descricao;
            retorno += "Gêneros: " + this.RetornarGeneros();
            retorno += "Bilheteria: " + this.Bilheteria;
            retorno += "Nota Omelete: " + this.NotaOmelete;

            return retorno;
        }
        public string RetornaTitulo()
        {
            return this.Titulo;
        }
        public string getDescricao()
        {
            return this.Descricao;
        }
        public int RetornaId()
        {
            return this.Id;
        }
        public void Exluir()
        {
            this.Excluido = true;
        }
        public bool RetornaExcluido()
        {
            return this.Excluido;
        }
        private string RetornarGeneros()
        {
            string retorno = "";
            
            foreach (var item in this.Genero)
            {
                retorno += item + " ";
            }
            return retorno;
        }
        
    }
}
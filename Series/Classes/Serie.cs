using Series.Enums;

namespace Series.Classes
{
    [Serializable]
    public class Serie : EntidadeBase
    {
        private List<Genero> Genero { get; set; }
        private string Titulo { get; set; }
        private string Descricao { get; set; } 
        private int Ano { get; set; }
        private bool Excluido { get; set; }
        public Serie(int Id, List<Genero> genero, string titulo, string descricao, int ano)
        {
            this.Id = Id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "Gênero: " + this.RetornarGeneros() + Environment.NewLine;
            retorno += "Título: " + this.Titulo + Environment.NewLine;
            retorno += "Descrição: " + this.Descricao + Environment.NewLine;
            retorno += "Ano: " + this.Ano;
            return retorno;
        }
        public string RetornaTitulo()
        {
            return this.Titulo;
        }
        public int RetornaId()
        {
            return this.Id;
        }
        public bool RetornaExcluido()
        {
            return this.Excluido;
        }
        public void Excluir()
        {   
            this.Excluido = true;
        }
        public string getDescricao()
        {
            return this.Descricao;
        }
        public int getAno()
        {
            return this.Ano;
        }
        public string getGeneros()
        {
            return this.RetornarGeneros();
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


namespace Series.Classes
{
    public class RepositorioFilmesSeries
    {   
        public SerieRepositorio Series;
        public FilmeRepositorio Filmes;
        public RepositorioFilmesSeries(SerieRepositorio series, FilmeRepositorio filmes)
        {
            this.Series = series;
            this.Filmes = filmes;
        }
        
        public override string ToString()
        {
            List<Serie> listaSeries = Series.Lista();
            List<Filme> listaFilmes = Filmes.Lista();

            string retorno = "--------------------------------------" + Environment.NewLine;
            foreach (Serie item in listaSeries)
            {   

                retorno += "Título: " + item.RetornaTitulo() + Environment.NewLine; 
                retorno += "Descrição: " + item.getDescricao() + Environment.NewLine;
                retorno += "Tipo: Serie" + Environment.NewLine;
                retorno += "--------------------------------------" + Environment.NewLine;
            }

            foreach (Filme item in listaFilmes)
            {   
                retorno += "Título: " + item.RetornaTitulo() + Environment.NewLine; 
                retorno += "Descrição: " + item.getDescricao() + Environment.NewLine;
                retorno += "Tipo: Filme" + Environment.NewLine;
                retorno += "--------------------------------------" + Environment.NewLine;
            }
            return retorno;
        }
    }
}
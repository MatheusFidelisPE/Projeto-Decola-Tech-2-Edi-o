using Series.Interfaces;

namespace Series.Classes
{   
    [Serializable]
    public class FilmeRepositorio : IRepositorio<Filme>
    {
        private List<Filme> listaFilmes = new List<Filme>();
        public void Atualiza(int id, Filme entidade)
        {
            this.listaFilmes[id] = entidade;
        }

        public void Exclui(int id)
        {
            listaFilmes[id].Exluir();
        }

        public void Insere(Filme entidade)
        {
            listaFilmes.Add(entidade);
        }

        //Os filmes e séries estão salvos, mas só é exibido para o program as entidades que não foram excluídas.
        public List<Filme> Lista()
        {
            return listaFilmes.FindAll(x => x.RetornaExcluido() == false);
        }

        public int ProximoId()
        {
            return listaFilmes.Count;
        }

        public Filme RetornaPorId(int id)
        {
            return listaFilmes[id];
        }
    }
}
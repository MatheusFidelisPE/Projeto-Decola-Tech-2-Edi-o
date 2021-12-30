using Series.Interfaces;

namespace Series.Classes
{
    [Serializable]
    public class SerieRepositorio : IRepositorio<Serie>
    {   
        private List<Serie> listaSerie = new List<Serie>();
        public void Atualiza(int id, Serie entidade)
        {
            listaSerie[id] = entidade;
        }
        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }
        public void Insere(Serie entidade)
        {
            listaSerie.Add(entidade);
        }
        public List<Serie> Lista()
        {
            List<Serie> listandoSeries = new List<Serie>();
            foreach (var item in listaSerie)
            {
                if(!item.RetornaExcluido())
                {
                    listandoSeries.Add(item);
                }
            }
            return listandoSeries;
        }
        public int ProximoId()
        {
            return listaSerie.Count;
        }
        public Serie RetornaPorId(int id)
        {
            return listaSerie[id];
        }
    
    }
}

using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Series.Classes;
using Series.Enums;

namespace Series
{
    public class Program
    {        
        public static void Main(string[] args)
        {   
            SerieRepositorio repositorioSerie;   
            FilmeRepositorio repositorioFilme;
            
            if(!File.Exists("Series.bin"))
            {
                repositorioSerie = new SerieRepositorio();
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();  
                Stream stream = new FileStream("Series.bin", FileMode.Open, FileAccess.Read, FileShare.Read);  
                repositorioSerie = (SerieRepositorio) formatter.Deserialize(stream);  
                stream.Close();  
            }
            if(!File.Exists("Filmes.bin"))
            {
                repositorioFilme = new FilmeRepositorio();
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();  
                Stream stream = new FileStream("Filmes.bin", FileMode.Open, FileAccess.Read, FileShare.Read);  
                repositorioFilme = (FilmeRepositorio) formatter.Deserialize(stream);  
                stream.Close();  
            }         

            Console.Write("INICIANDO PROGRAMA\nINFORME SEU NOME: ");
            string nome = Console.ReadLine();
            string opcaoUsuario = "";
            while(true)
            {   
                opcaoUsuario = MenuPrincipal();
                switch(opcaoUsuario)
                {
                    case "1":
                        do{
                            opcaoUsuario = MenuEscolha(nome);
                            switch(opcaoUsuario)
                            {
                                case "1":
                                    ListarSeries(repositorioSerie);
                                    break;
                                case "2":
                                    InserirSerie(repositorioSerie);
                                    break;
                                case "3":
                                    AtualizarSerie(repositorioSerie);
                                    break;
                                case "4":
                                    ExcluirSerie(repositorioSerie);
                                    break;
                                case "5":
                                    VisualizarSerie(repositorioSerie);
                                    break;
                                case "V":                     
                                    break;
                                default:
                                    //Console.Clear();
                                    break;
                            }
                        } while(opcaoUsuario != "V");
                        break;
                    case "2":
                        do{
                            opcaoUsuario = MenuEscolhaFilme(nome);
                            switch(opcaoUsuario)
                            {
                                case "1":
                                    ListarFilmes(repositorioFilme);
                                    break;
                                case "2":
                                    InserirFilme(repositorioFilme);
                                    break;
                                case "3":
                                    AlterarFilme(repositorioFilme);
                                    break;
                                case "4":
                                    ExcluirFilme(repositorioFilme);
                                    break;
                                case "5":
                                    VisualizarFilme(repositorioFilme);
                                    break;
                                case "V":
                                    break;
                                default:
                                    break;
                            }
                        } while(opcaoUsuario != "V");
                        break;
                    case "3":
                        VisualizarTodos(repositorioSerie, repositorioFilme);
                        break;
                    case "S":
                        SalvarFilmes(repositorioFilme);
                        SalvarSerie(repositorioSerie);
                        return;
                }
               
            }
        }
        private static void VisualizarFilme(FilmeRepositorio repositorioFilme)
        {
            Console.Write("Informe o Id: ");
            int id = int.Parse(Console.ReadLine());

            Filme filme = repositorioFilme.RetornaPorId(id);
            Console.WriteLine(filme);
        }
        private static void ExcluirFilme(FilmeRepositorio repositorioFilme)
        {
            Console.Write("Informe o Id do filme que será excluído: ");
            int idExclusao = int.Parse(Console.ReadLine());
            repositorioFilme.Exclui(idExclusao);
        }
        private static void AlterarFilme(FilmeRepositorio repositorioFilme)
        {
            int genero = 0;
            List<int> generos = new List<int>();

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero),i)}");
            }
            Console.Write("Informe os Gêneros [Digite 0 para sair]: ");
            
            do{                
                genero = int.Parse(Console.ReadLine());
                if(genero >= 1 && genero <= 13)
                {
                    generos.Add(genero);
                }
            }while(genero != 0);

            Console.Write("Informe o Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Informe a descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Informe o bilheteria: ");
            decimal bilheteria = decimal.Parse(Console.ReadLine());

            Console.Write("Informe a nota omelete: ");
            decimal notaOmelete = decimal.Parse(Console.ReadLine());
            
            Console.Write("Informe o Id de modificação: ");
            int idLista = int.Parse(Console.ReadLine());

            Filme novoFilme = new Filme(Id: idLista,
                                    genero: generos.ConvertAll<Genero>(x => (Genero)x),
                                    titulo: titulo,
                                    descricao: descricao,
                                    notaOmelete: notaOmelete,
                                    bilheteria: bilheteria);
            repositorioFilme.Atualiza(idLista, novoFilme);
        }
        private static void InserirFilme(FilmeRepositorio repositorioFilme)
        {
            int genero = 0;
            List<int> generos = new List<int>();

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero),i)}");
            }
            Console.Write("Informe os Gêneros [Digite 0 para sair]: ");
            
            do{                
                genero = int.Parse(Console.ReadLine());
                if(genero >= 1 && genero <= 13)
                {
                    generos.Add(genero);
                }
            }while(genero != 0);

            Console.Write("Informe o Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Informe a descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Informe o bilheteria: ");
            decimal bilheteria = decimal.Parse(Console.ReadLine());

            Console.Write("Informe a nota omelete: ");
            decimal notaOmelete = decimal.Parse(Console.ReadLine());

            Filme novoFilme = new Filme(Id: repositorioFilme.ProximoId(),
                                    genero: generos.ConvertAll<Genero>(x => (Genero)x),
                                    titulo: titulo,
                                    descricao: descricao,
                                    notaOmelete: notaOmelete,
                                    bilheteria: bilheteria);

            repositorioFilme.Insere(novoFilme);
        }
        private static void ListarFilmes(FilmeRepositorio repositorioFilme)
        {
            Console.WriteLine("-=-=-=-=-=-=-=-=LISTAR FILMES-=-=-=-=-=-=-=-=");

            var listagem = repositorioFilme.Lista();

            if(listagem.Count == 0)
            {
                Console.WriteLine("-=-=A LISTA DE FILMES AINDA ESTÁ VAZIA-=-=");
            }else
            {
                foreach (var item in listagem)
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine($"Id: {item.RetornaId()}\nTítulo: {item.RetornaTitulo()}");
                    Console.WriteLine("----------------------------------");
                }
            }
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        }
        private static string MenuEscolhaFilme(string nome)
        {
            Console.WriteLine($"Olá, {nome}!\nSeja muito bem vindo ao nosso aplicativo de séries!");
            Console.WriteLine("--------------------MENU--------------------");
            Console.WriteLine($"Séries e Filmes de {nome}!\nInforme a opção desejada: ");
            

            Console.WriteLine("1- Listar Filmes");
            Console.WriteLine("2- Inserir Filme");
            Console.WriteLine("3- Atualizar Filme");
            Console.WriteLine("4- Excluir Filme");
            Console.WriteLine("5- Visualizar Filme");
            //Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("V- VOLTAR");
            Console.WriteLine("--------------------------------------------");

            string escolhaUsuario = Console.ReadLine().ToUpper();
            return escolhaUsuario;
        }
        private static void SalvarSerie(SerieRepositorio repositorio)
        {
            IFormatter formatter = new BinaryFormatter();  
            Stream stream = new FileStream("Series.bin", FileMode.Create, FileAccess.Write, FileShare.None);  
            formatter.Serialize(stream, repositorio);  
            stream.Close();  
        }
        private static void SalvarFilmes(FilmeRepositorio repositorio)
        {
            IFormatter formatter = new BinaryFormatter();  
            Stream stream = new FileStream("Filmes.bin", FileMode.Create, FileAccess.Write, FileShare.None);  
            formatter.Serialize(stream, repositorio);  
            stream.Close();  
        }
        private static string MenuPrincipal()
        {
            Console.WriteLine("-=-=-=-=-=-MENU PRINCIPAL-=-=-=-=-=-");
            Console.WriteLine("1- Séries\n2- Filmes\n3- Listar Todos\nS- Sair");
            string escolha = Console.ReadLine().ToUpper();
            //Console.Clear();
            return escolha;
        }
        private static string MenuEscolha(string nome)
        {      
            
            Console.WriteLine($"Olá, {nome}!\nSeja muito bem vindo ao nosso aplicativo de séries!");
            Console.WriteLine("--------------------MENU--------------------");
            Console.WriteLine($"Séries e Filmes de {nome}!\nInforme a opção desejada: ");
            

            Console.WriteLine("1- Listar Séries");
            Console.WriteLine("2- Inserir Série");
            Console.WriteLine("3- Atualizar Série");
            Console.WriteLine("4- Excluir Série");
            Console.WriteLine("5- Visualizar Série");
            //Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("V- VOLTAR");
            Console.WriteLine("--------------------------------------------");

            string escolhaUsuario = Console.ReadLine().ToUpper();
            return escolhaUsuario;
        }
        private static void ListarSeries(SerieRepositorio repositorio)
        {
            Console.WriteLine("-=-=-=-=-=-=-=-=LISTAR SÉRIES-=-=-=-=-=-=-=-=");

            var listagem = repositorio.Lista();

            if(listagem.Count == 0)
            {
                Console.WriteLine("-=-=A LISTA DE SÉRIES AINDA ESTÁ VAZIA-=-=");
            }else
            {
                foreach (var item in listagem)
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine($"Id: {item.RetornaId()}\nTítulo: {item.RetornaTitulo()}");
                    Console.WriteLine("----------------------------------");
                }
            }
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        }
        private static void InserirSerie(SerieRepositorio repositorio)
        {   
            int genero = 0;
            List<int> generos = new List<int>();

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero),i)}");
            }
            Console.Write("Informe os Gêneros [Digite 0 para sair]: ");
            
            do{                
                genero = int.Parse(Console.ReadLine());
                if(genero >= 1 && genero <= 13)
                {
                    generos.Add(genero);
                }
            }while(genero != 0);

            Console.Write("Informe o Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Informe a descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Informe o ano: ");
            int ano = int.Parse(Console.ReadLine());

            Serie novaSerie = new Serie(Id: repositorio.ProximoId(),
                                    genero: generos.ConvertAll<Genero>(x => (Genero)x),
                                    titulo: titulo,
                                    descricao: descricao,
                                    ano: ano);

            repositorio.Insere(novaSerie);
        }
        private static void AtualizarSerie(SerieRepositorio repositorio)
        {
            int genero = 0;
            List<int> generos = new List<int>();
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero),i)}");
            }
           
            Console.Write("Informe os Gêneros [Digite 0 para sair]: ");
            do{                
                genero = int.Parse(Console.ReadLine());
                if(genero >= 1 && genero <= 13)
                {
                    generos.Add(genero);
                }
            }while(genero == 0);
            
            Console.Write("Informe o Id: ");
            int idSerie = int.Parse(Console.ReadLine());

            Console.Write("Informe o Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Informe a descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Informe o ano: ");
            int ano = int.Parse(Console.ReadLine());

            Serie novaSerie = new Serie(Id: idSerie,
                                    genero:generos.ConvertAll<Genero>(x => (Genero)x),
                                    titulo: titulo,
                                    descricao: descricao,
                                    ano: ano);

            repositorio.Atualiza(idSerie,novaSerie);
        }
        private static void ExcluirSerie(SerieRepositorio repositorio)
        {   
            bool existe = false;
            Console.Write("Informe o Id da série a ser excluída: ");
            int id = int.Parse(Console.ReadLine());

            List<Serie> listagemSeriesNaoExcluidas = repositorio.Lista();
            foreach (var item in listagemSeriesNaoExcluidas)
            {
                if(item.Id == id)
                {
                    existe = true;
                    break;
                }
            }

            if(!existe)
            {
                Console.WriteLine("O Id que você inseriu não existe ou já foi excluido!");
            } 
            else
            {
                repositorio.Exclui(id);
            }
        }
        private static void VisualizarSerie(SerieRepositorio repositorio)
        {
            Console.Write("Informe o Id: ");
            int id = int.Parse(Console.ReadLine());

            Serie serie = repositorio.RetornaPorId(id);
            Console.WriteLine(serie);
       }
        private static void VisualizarTodos(SerieRepositorio repSerie, FilmeRepositorio repFilmes)
        {
            var repSeriesFilmes = new RepositorioFilmesSeries(repSerie, repFilmes);
            Console.WriteLine(repSeriesFilmes);
        }    
    }
}

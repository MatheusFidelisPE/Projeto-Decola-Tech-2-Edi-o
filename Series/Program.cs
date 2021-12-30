
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
            SerieRepositorio repositorio;   

            if(!File.Exists("MyFile.bin"))
            {
                repositorio = new SerieRepositorio();
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();  
                Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);  
                repositorio = (SerieRepositorio) formatter.Deserialize(stream);  
                stream.Close();  
            }

            Console.Write("INICIANDO PROGRAMA\nINFORME SEU NOME: ");
            string nome = Console.ReadLine();
            string opcaoUsuario = "";
            while(true)
            {
                opcaoUsuario = MenuEscolha(nome);
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries(repositorio);
                        break;
                    case "2":
                        InserirSerie(repositorio);
                        break;
                    case "3":
                        AtualizarSerie(repositorio);
                        break;
                    case "4":
                        ExcluirSerie(repositorio);
                        break;
                    case "5":
                        VisualizarSerie(repositorio);
                        break;
                    case "S":
                        Salvar(repositorio);
                        return;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private static void Salvar(SerieRepositorio repositorio)
        {
            IFormatter formatter = new BinaryFormatter();  
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);  
            formatter.Serialize(stream, repositorio);  
            stream.Close();  
        }
        private static string MenuEscolha(string nome)
        {      
            //Console.Clear();
            Console.WriteLine($"Olá, {nome}!\nSeja muito bem vindo ao nosso aplicativo de séries!");
            Console.WriteLine("--------------------MENU--------------------");
            Console.WriteLine($"Séries de {nome}!\nInforme a opção desejada: ");

            Console.WriteLine("1- Listar Séries");
            Console.WriteLine("2- Inserir Série");
            Console.WriteLine("3- Atualizar Série");
            Console.WriteLine("4- Excluir Série");
            Console.WriteLine("5- Visualizar Série");
            //Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("S- SAIR");
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
    
        
    }
}

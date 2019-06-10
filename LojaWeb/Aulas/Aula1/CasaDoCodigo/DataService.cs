using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    // Classe para inicializar o banco de dados
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            //comando para inicializar o banco, caso não exista na inicialização do projeto
            contexto.Database.EnsureCreated();

            List<Livro> livros = GetLivros();

            produtoRepository.SaveProdutos(livros);
        }

       

        private static List<Livro> GetLivros()
        {
            //coamndo para ler os dados do arquivo .json e povoar as tabelas do banco com dados.
            // pode ser um arquivo .txt também
            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livros;
        }
    }
}

using CasaDoCodigo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    // Classe para inicializar o banco de dados
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;

        public DataService(ApplicationContext contexto)
        {
            this.contexto = contexto;
        }

        public void InicializaDB()
        {
            //comando para inicializar o banco, caso não exista na inicialização do projeto
            contexto.Database.EnsureCreated();

            //coamndo para ler os dados do arquivo .json e povoar as tabelas do banco com dados.
            // pode ser um arquivo .txt também
            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);

            // o foreach é usado pasa instaciar os produtos já com os dados do arquivo .json
            foreach (var livro in livros)
            {
                contexto.Set<Produto>().Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
            }
            // comando para salvar as alterações no banco
            contexto.SaveChanges();
        }
    }
}

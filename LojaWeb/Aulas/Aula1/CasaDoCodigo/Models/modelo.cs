using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }

    public class Produto : BaseModel
    {
        public Produto()
        {

        }

        [Required]
        public string Codigo { get; private set; }
        [Required]
        public string Nome { get; private set; }
        [Required]
        public decimal Preco { get; private set; }

        public Produto(string codigo, string nome, decimal preco)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
        }
    }

    public class Cadastro : BaseModel
    {
        public Cadastro()
        {
        }

        public virtual Pedido Pedido { get; set; }
        [MinLength(5, ErrorMessage ="O Nome deve ter no mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "O Nome deve ter no máximo 50 caracteres")]
        [Required(ErrorMessage ="O Nome é Obrigatório")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "O Email é Obrigatório")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "O Telefone é Obrigatório")]
        public string Telefone { get; set; } = "";
        [Required(ErrorMessage = "O Endereço é Obrigatório")]
        public string Endereco { get; set; } = "";
        [Required(ErrorMessage = "O Complemento é Obrigatório")]
        public string Complemento { get; set; } = "";
        [Required(ErrorMessage = "O Bairro é Obrigatório")]
        public string Bairro { get; set; } = "";
        [Required(ErrorMessage = "O Município é Obrigatório")]
        public string Municipio { get; set; } = "";
        [Required(ErrorMessage = "A UF é Obrigatória")]
        public string UF { get; set; } = "";
        [Required(ErrorMessage = "O CEP é Obrigatório")]
        public string CEP { get; set; } = "";
    }

    [DataContract]
    public class ItemPedido : BaseModel
    {   
        [Required]
        [DataMember]
        public Pedido Pedido { get; private set; }
        [Required]
        [DataMember]
        public Produto Produto { get; private set; }
        [Required]
        [DataMember]
        public int Quantidade { get; private set; }
        [Required]
        [DataMember]
        public decimal PrecoUnitario { get; private set; }

        public ItemPedido()
        {

        }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        internal void AtualizaQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
    }

    public class Pedido : BaseModel
    {
        public Pedido()
        {
            Cadastro = new Cadastro();
        }

        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        public List<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();
        [Required]
        public virtual Cadastro Cadastro { get; private set; }
    }
}

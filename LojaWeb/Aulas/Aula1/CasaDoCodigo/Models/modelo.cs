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
        [MinLength(5, ErrorMessage ="O Nome deve ter no m�nimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = "O Nome deve ter no m�ximo 50 caracteres")]
        [Required(ErrorMessage ="O Nome � Obrigat�rio")]
        public string Nome { get; set; } = "";
        [Required(ErrorMessage = "O Email � Obrigat�rio")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "O Telefone � Obrigat�rio")]
        public string Telefone { get; set; } = "";
        [Required(ErrorMessage = "O Endere�o � Obrigat�rio")]
        public string Endereco { get; set; } = "";
        [Required(ErrorMessage = "O Complemento � Obrigat�rio")]
        public string Complemento { get; set; } = "";
        [Required(ErrorMessage = "O Bairro � Obrigat�rio")]
        public string Bairro { get; set; } = "";
        [Required(ErrorMessage = "O Munic�pio � Obrigat�rio")]
        public string Municipio { get; set; } = "";
        [Required(ErrorMessage = "A UF � Obrigat�ria")]
        public string UF { get; set; } = "";
        [Required(ErrorMessage = "O CEP � Obrigat�rio")]
        public string CEP { get; set; } = "";

        internal void Update(Cadastro novoCadastro)
        {
            this.Bairro = novoCadastro.Bairro;
            this.CEP    = novoCadastro.CEP;
            this.Complemento = novoCadastro.Complemento;
            this.Email = novoCadastro.Email;
            this.Endereco = novoCadastro.Endereco;
            this.Municipio = novoCadastro.Municipio;
            this.Nome = novoCadastro.Nome;
            this.Telefone = novoCadastro.Telefone;
            this.UF = novoCadastro.UF;
        }
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

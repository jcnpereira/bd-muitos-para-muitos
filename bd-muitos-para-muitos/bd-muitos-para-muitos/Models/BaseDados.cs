using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace bd_muitos_para_muitos.Models {
   public class BaseDados : DbContext {

      public BaseDados() : base("DefaultConnection") { }
      
      // 'tabelas' a serem criadas na Base de Dados
      public virtual DbSet<A> A { get; set; }
      public virtual DbSet<B> B { get; set; }
      public virtual DbSet<C> C { get; set; }
      public virtual DbSet<D> D { get; set; }
      public virtual DbSet<CD> CD { get; set; } // tabela que irá exprimir o relacionamento entre as classes C e D 



      protected override void OnModelCreating(DbModelBuilder modelBuilder) {
         modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();  // impede a EF de 'pluralizar' os nomes das tabelas
         modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  // força a que a chave forasteira não tenha a propriedade 'on delete cascade'
         modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();  // força a que a chave forasteira não tenha a propriedade 'on delete cascade'

         base.OnModelCreating(modelBuilder);
      }
   }
}
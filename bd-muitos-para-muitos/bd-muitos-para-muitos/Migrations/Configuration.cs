namespace bd_muitos_para_muitos.Migrations {
	using Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<bd_muitos_para_muitos.Models.BaseDados> {
      public Configuration() {
         AutomaticMigrationsEnabled = true;
      }

      protected override void Seed(bd_muitos_para_muitos.Models.BaseDados context) {
			//  This method will be called after migrating to the latest version.

			// adicionar atributos às classes
			///////////////////////////////////////////////
			// classe A
			///////////////////////////////////////////////
			var registosDeA = new List<A> {
				new A  {ID=1, NomeA1="a11", NomeA2="a12"},
				new A  {ID=2, NomeA1="a21", NomeA2="a22"},
				new A  {ID=3, NomeA1="a31", NomeA2="a32"},
				new A  {ID=4, NomeA1="a41", NomeA2="a42"},
				new A  {ID=5, NomeA1="a51", NomeA2="a52"},
				new A  {ID=6, NomeA1="a61", NomeA2="a62"}
			};

			registosDeA.ForEach(aa => context.A.AddOrUpdate(a => a.ID,aa));
			context.SaveChanges();

			///////////////////////////////////////////////
			// classe B
			// como o relacionamento entre a classe A e a classe B
			// é do tipo M-N, não existe tabela do relacionamento
			// (dentro da Entity Framework)
			// Assim, para exprimir esse relacionamento, 
			// temos de criar uma lista de elementos de A
			// e associá-los aos elementos de B
			///////////////////////////////////////////////
			var registosDeB = new List<B> {
				new B  {ID=1, NomeB="b11", DataB=new DateTime(2017,05,01), ListaDeObjetosDeA=new List<A>{ registosDeA[0], registosDeA[1] } },
				new B  {ID=2, NomeB="b21", DataB=new DateTime(2017,05,01), ListaDeObjetosDeA=new List<A>{ registosDeA[2], registosDeA[3] } },
				new B  {ID=3, NomeB="b31", DataB=new DateTime(2017,05,01), ListaDeObjetosDeA=new List<A>{ registosDeA[4], registosDeA[5] } },
				new B  {ID=4, NomeB="b41", DataB=new DateTime(2017,05,01), ListaDeObjetosDeA=new List<A>{} }
			};

			registosDeB.ForEach(bb => context.B.AddOrUpdate(b => b.ID,bb));
			context.SaveChanges();


		}
	}
}

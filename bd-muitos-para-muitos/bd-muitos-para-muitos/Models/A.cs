using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bd_muitos_para_muitos.Models {

   public class A {
      //***********************************************************************
      // A classe A e a classe B têm um relacionamento Muitos-para-Muitos
      // em que só existem atributos nas classes A e B.
      //
      // Nesta circunstância, a forma de exprimir o relacionamento
      // é criar uma 'lista' de objetos de uma das classes na outra classe,
      // e vice-versa.
      // Aqui, NÃO é necessário criar uma classe para exprimir o relacionamento.
      //
      //
      // Os atributos destas classes são atributos genéricos e servem apenas
      // para ilustrar este processo.
      //***********************************************************************

      public A() {
         ListaDeObjetosDeB = new HashSet<B>();
      }
      public int ID { get; set; }

      public string NomeA1 { get; set; }

      public string NomeA2 { get; set; }

      //***********************************************************************
      // definição do atributo que será utilizado para exprimir o relacionamento
      // com os objetos da classe B
      public virtual ICollection<B> ListaDeObjetosDeB { get; set; }
      //***********************************************************************
   }
}
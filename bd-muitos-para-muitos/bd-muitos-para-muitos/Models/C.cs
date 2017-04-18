using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bd_muitos_para_muitos.Models {
   public class C {
      //***********************************************************************
      // A classe C e a classe D têm um relacionamento Muitos-para-Muitos,
      // mas ao contrário do relacionamento entre as classes A e B,
      // neste caso EXISTEM atributos do relacionamento.
      // 
      // Nesta circunstância, a forma de exprimir o relacionamento,
      // é DIVIDIR o relacionamento em dois relacionamentos do tipo 1-Muitos: C-CD e CD-D
      // 
      // A classe CD  é uma classe para exprimir o relacionamento entre as classes C e D.
      //
      //
      // Os atributos destas classes são atributos genéricos e servem apenas
      // para ilustrar este processo.
      //***********************************************************************

      public C() {
         ListaDeObjetosDeCD = new HashSet<CD>();
      }


      public int ID { get; set; }

      public string NomeC1 { get; set; }


      //***********************************************************************
      // definição do atributo que será utilizado para exprimir o relacionamento
      // com os objetos da classe CD
      public virtual ICollection<CD> ListaDeObjetosDeCD { get; set; }
      //***********************************************************************

   }
}
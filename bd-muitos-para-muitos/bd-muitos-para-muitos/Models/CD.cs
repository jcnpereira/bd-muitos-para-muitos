using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bd_muitos_para_muitos.Models {
   public class CD {
      /* ***********************************************************************
       Nesta classe é que está o 'segredo' para representar o relacionamento
       entre a classe C e a classe D num relacionamento Muitos-para-Muitos,
       com atributos do relacionamento.
       
       Nesta classe vão ser representados dois relacionamentos 1-Muitos,
       com as respetivas chaves forasteiras (FK).
          - Uma, para referenciar a classe C
          - e outra, para referenciar a classe D
      
       Ao contrário do que é caraterístico de um relacionamento M-N, em SQL,
       aqui existirá uma chave primária com um único atributo.
       A chave primária com os dois atributos (que são, também, chave forasteira)
       não é aqui permitida.

      
       Os atributos destas classes são atributos genéricos e servem apenas
       para ilustrar este processo.
      *********************************************************************** */

      public int ID { get; set; } // PK, por exigência da Entity Framework

      // atributos específicos do relacionamento
      public int AtributoDoRelacionamento1 { get; set; }
      public string AtributoDoRelacionamento2 { get; set; }



      //***********************************************************************
      // definição da chave forasteira (FK) que referencia a classe C
      //***********************************************************************
      [ForeignKey("Cfk")]
      public C C { get; set; }
      public int Cfk { get; set; }

      //***********************************************************************
      // definição da chave forasteira (FK) que referencia a classe D
      //***********************************************************************
      [ForeignKey("Dfk")]
      public D D { get; set; }
      public int Dfk { get; set; }
      //***********************************************************************

   }
}
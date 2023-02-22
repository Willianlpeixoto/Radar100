using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radar100
{

    public class Post
    {

        public int seq_snipe { get; set; } // ID
        public int cod_pkm { get; set; } // Numero pokedex
        public string nom_pkm { get; set; } // Nome
        public int iv_pkm { get; set; } // IV
        public int cp_pkm { get; set; } // CP
        public int lvl_pkm { get; set; } // Nivel
        public string des_coordenada { get; set; } // Coordenada
        public string dta_inclusao { get; set; } // Data Hora inclusao // "2019-11-09T12:35:15.000Z"
        public int cat_pkm { get; set; } // ???
        public string ind_shiny { get; set; } // pode ser shiny
        public string sgl_hex { get; set; } //
        public string sgl_pais { get; set; } //
        public string des_origem { get; set; } //
        public int per_raridade { get; set; } //

    }

}

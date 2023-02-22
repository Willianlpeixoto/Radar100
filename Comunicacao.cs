using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Telegram.Bot;

namespace Radar100
{

    public class Comunicacao 
    {


        //https://www.oanalista.com.br/2017/07/17/telegram-notify-servidor-telegram/
        //https://www.youtube.com/watch?v=YcEyvUf22AQ

        public static readonly TelegramBotClient bot = new TelegramBotClient("Insira aqui seu token do telegram");

        public static void AtualizaListaPK(ref int nUltimoId, DataTable dtPK = null)
        {

            if (dtPK != null)
            {

                dtPK.Clear();

            }

            // ---

            var requisicaoWeb = WebRequest.CreateHttp("http://megasnipe.ddns.net/api/snipes");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {

                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                List<Post> result = JsonConvert.DeserializeObject<List<Post>>(objResponse.ToString());

                result = result.OrderBy(c => c.seq_snipe).ToList();

                foreach (Post itemPost in result)
                {

                    bool bFiltro = false;

                    // testa o filtro

                    #region Filtros


                    #region 100 >30

                    if (itemPost.iv_pkm == 100 && itemPost.lvl_pkm >= 30)
                    {

                        bFiltro = true;

                        if (itemPost.seq_snipe > nUltimoId)
                        {

                            bot.SendTextMessageAsync("@ArgosMonitor100_35", itemPost.cod_pkm + " - " + itemPost.nom_pkm + " - CP: " + itemPost.cp_pkm + " - " +
                            itemPost.iv_pkm + " - " + itemPost.lvl_pkm);

                            System.Threading.Thread.Sleep(2 * 1000);
                            System.Threading.Thread.Sleep(2 * 1000);

                            bot.SendTextMessageAsync("@ArgosMonitor100_35", itemPost.des_coordenada);

                            System.Threading.Thread.Sleep(2 * 1000);
                            System.Threading.Thread.Sleep(2 * 1000);

                        }

                    }

                    #endregion

                    #region Deino = 100 >= 20

                    if (itemPost.iv_pkm == 100 && itemPost.lvl_pkm >= 20 && (itemPost.cod_pkm == 633 || itemPost.cod_pkm == 634 || itemPost.cod_pkm == 635))
                    {

                        bFiltro = true;

                        if (itemPost.seq_snipe > nUltimoId)
                        {

                            bot.SendTextMessageAsync("@deino10030", itemPost.cod_pkm + " - " + itemPost.nom_pkm + " - CP: " + itemPost.cp_pkm + " - " +
                            itemPost.iv_pkm + " - " + itemPost.lvl_pkm);

                            System.Threading.Thread.Sleep(2 * 1000);
                            System.Threading.Thread.Sleep(2 * 1000);

                            bot.SendTextMessageAsync("@deino10030", itemPost.des_coordenada);

                            System.Threading.Thread.Sleep(2 * 1000);
                            System.Threading.Thread.Sleep(2 * 1000);

                        }

                    }

                    #endregion 

                    // ---

                    #region 100 >= 30 e lista

                    if (itemPost.iv_pkm == 100 && itemPost.lvl_pkm >= 30)
                    {

                        if (
                            itemPost.cod_pkm == 127 || itemPost.cod_pkm == 123 || itemPost.cod_pkm == 212 ||                                                                                                                                                                   // 01 - Inseto   = 127           - Pinsir           | 123, 212      - Scizor          | 
                            itemPost.cod_pkm == 633 || itemPost.cod_pkm == 634 || itemPost.cod_pkm == 635 || itemPost.cod_pkm == 246 || itemPost.cod_pkm == 247 || itemPost.cod_pkm == 248 ||                                                                                  // 02 - Dark     =                 Darkrai          | 633, 634, 635 - Hydregon        | 246, 247, 248 - Tyranitar
                            itemPost.cod_pkm == 443 || itemPost.cod_pkm == 444 || itemPost.cod_pkm == 445 || itemPost.cod_pkm == 147 || itemPost.cod_pkm == 148 || itemPost.cod_pkm == 149 ||                                                                                  // 03 - Dragao   =                 Rayquaza         |                 Palkia          | 443, 444, 445 - Garchomp           |                 Dialga          | 147, 148, 149 - Dragonite                             | // RETIRADO | 371, 372, 373 - Salamence Legado itemPost.cod_pkm == 371 || itemPost.cod_pkm == 372 || itemPost.cod_pkm == 373 || 
                            itemPost.cod_pkm == 239 || itemPost.cod_pkm == 125 || itemPost.cod_pkm == 466 || itemPost.cod_pkm == 81  || itemPost.cod_pkm == 82  || itemPost.cod_pkm == 462 || itemPost.cod_pkm == 133 || itemPost.cod_pkm == 135 ||                            // 04 - Eletrico =                 Raikou           |                 Zapdos          | 239, 125, 466 - Electivire         | 81, 82, 462   - Magnezone       |                 Luxray    | 133, 135      - Jolteon   |
                            itemPost.cod_pkm == 175 || itemPost.cod_pkm == 176 || itemPost.cod_pkm == 468 || itemPost.cod_pkm == 280 || itemPost.cod_pkm == 281 || itemPost.cod_pkm == 282 ||                                                                                  // 05 - Fada     = 175, 176, 468 - Togekiss         | 280, 281, 282 - Gardevoir       |                                


                            itemPost.cod_pkm == 532 || itemPost.cod_pkm == 533 || itemPost.cod_pkm == 534 || itemPost.cod_pkm == 66  || itemPost.cod_pkm == 67  || itemPost.cod_pkm == 68  || itemPost.cod_pkm == 296 || itemPost.cod_pkm == 297 || itemPost.cod_pkm == 285 || itemPost.cod_pkm == 286 ||                                                       // 06 - Lutador  = 66, 67, 68    - Machamp          | 296, 297      - Hariyama        | 285, 286      - Breloom            |
                            itemPost.cod_pkm == 607 || itemPost.cod_pkm == 608 || itemPost.cod_pkm == 609 ||                                                                                                                                                                   // 07 - Fogo     =               - Moltres          | 607, 608, 609 - Chandelure      |               - Entei              | Heatam                          |                                                         // RETIRADO | 255, 256, 257 - Blazikem Legado itemPost.cod_pkm == 255 || itemPost.cod_pkm == 256 || itemPost.cod_pkm == 257 ||   4, 5, 6 - Charizard Legado  itemPost.cod_pkm == 4   || itemPost.cod_pkm == 5 || itemPost.cod_pkm == 6 || 

                            itemPost.cod_pkm == 566 || itemPost.cod_pkm == 567 ||
                            itemPost.cod_pkm == 198 || itemPost.cod_pkm == 430 || itemPost.cod_pkm == 519 || itemPost.cod_pkm == 520 || itemPost.cod_pkm == 521 ||                                                                                                             // 08 - Voador   =               - Moltres          |               - Rayquaza        | 198, 430      - Honchkrow          | 519, 520, 521 - Unfezant    	|
                            itemPost.cod_pkm == 607 || itemPost.cod_pkm == 608 || itemPost.cod_pkm == 609 || itemPost.cod_pkm == 353 || itemPost.cod_pkm == 354 ||                                                                                                             // 09 - Fantasma =               - Giratina         | 607, 608, 609 - Chandelure      | 92, 93, 94    - Gengar Legado 2017 | 353, 354      - Banett          |
                            itemPost.cod_pkm == 406 || itemPost.cod_pkm == 315 || itemPost.cod_pkm == 407 ||                                                                                                                                                                   // 10 - Planta   = 406, 315, 407 - Roserade                                                                                                                                                                            // RETIRADO | 1, 2, 3       - Venusaur Legado |                                  itemPost.cod_pkm == 1   || itemPost.cod_pkm == 2   || itemPost.cod_pkm == 3 ||
                            itemPost.cod_pkm == 529 || itemPost.cod_pkm == 530 || itemPost.cod_pkm == 443 || itemPost.cod_pkm == 444 || itemPost.cod_pkm == 445 || itemPost.cod_pkm == 111 || itemPost.cod_pkm == 112 || itemPost.cod_pkm == 464 ||                            // 11 - Terra    =               - Groundoun        | 529, 530      - Excadrill       | 443, 444, 445 - Garchomp           | 111, 112, 464 - Rhyperior       |
                            itemPost.cod_pkm == 220 || itemPost.cod_pkm == 221 || itemPost.cod_pkm == 473 || itemPost.cod_pkm == 133 || itemPost.cod_pkm == 471 ||                                                                                                             // 12 - Gelo     = 220, 221, 473 - Mamoswine        | 133, 471      - Glaceon         // RETIRADO | 215, 461      - Weavile          | itemPost.cod_pkm == 215 || itemPost.cod_pkm == 461 ||
                            itemPost.cod_pkm == 190 || itemPost.cod_pkm == 424 ||                                                                                                                                                                                              // 13 - Normal   = 190, 424      - Ambipom          |                                                                    
                            itemPost.cod_pkm == 406 || itemPost.cod_pkm == 315 || itemPost.cod_pkm == 407 || itemPost.cod_pkm == 453 || itemPost.cod_pkm == 454 ||                                                                                                             // 14 - Veneno   = 406, 315, 407 - Roserade         | 453, 454      - Toxicroak       |                                  
                            itemPost.cod_pkm == 482 || itemPost.cod_pkm == 133 || itemPost.cod_pkm == 196 ||                                                                                                                                                                   // 15 - Psiquico =               - Mewtwo           | 482           - Azelf           | 133, 196      - Espeon             |
                            itemPost.cod_pkm == 408 || itemPost.cod_pkm == 409 || itemPost.cod_pkm == 246 || itemPost.cod_pkm == 247 || itemPost.cod_pkm == 248 || itemPost.cod_pkm == 111 || itemPost.cod_pkm == 112 || itemPost.cod_pkm == 464 ||                            // 16 - Pedra    = 408, 409      - Rampardos        | 246, 247, 248 - Tyranitar Leg   | 111, 112, 464 - Rhyperior          |
                            itemPost.cod_pkm == 529 || itemPost.cod_pkm == 530 || itemPost.cod_pkm == 123 || itemPost.cod_pkm == 212 ||                                                                                                                                        // 17 - Aço      = 374, 375, 376 - Metagross Legado |               - Dialga          | 529, 530      - Excadrill          | 123, 212 - Scizor               |
                            itemPost.cod_pkm == 99  || itemPost.cod_pkm == 100                                                                                                                                                                                                 // 18 - Agua     =               - Kyogre           | 99, 100        - Kingler         | // RETIRADO  | 258, 259, 260 - Swampert Legado itemPost.cod_pkm == 258 || itemPost.cod_pkm == 259 || itemPost.cod_pkm == 260 || 



                            )
                        {

                            bFiltro = true;

                            if (itemPost.seq_snipe > nUltimoId)
                            {

                                bot.SendTextMessageAsync("@RadarArgos10030Lista", itemPost.cod_pkm + " - " + itemPost.nom_pkm + " - " + itemPost.cp_pkm + " - " +
                                itemPost.iv_pkm + " - " + itemPost.lvl_pkm);

                                System.Threading.Thread.Sleep(2 * 1000);
                                System.Threading.Thread.Sleep(2 * 1000);

                                bot.SendTextMessageAsync("@RadarArgos10030Lista", itemPost.des_coordenada);

                                System.Threading.Thread.Sleep(2 * 1000);
                                System.Threading.Thread.Sleep(2 * 1000);

                            }

                        }

                    }

                    #endregion


                    #region 100 >= 25 e legacy year

                    if (itemPost.iv_pkm == 100 && itemPost.lvl_pkm >= 25) // os que repetem muito, estou colocando apenas os nivel 35
                    {

                        if (
                            itemPost.cod_pkm == 371 || itemPost.cod_pkm == 372 || itemPost.cod_pkm == 373 || // 03 - Dragao   = 371, 372, 373 - Salamence 
                            ( itemPost.cod_pkm == 255 && itemPost.lvl_pkm == 35 ) || itemPost.cod_pkm == 256 || itemPost.cod_pkm == 257 || // 07 - Fogo     = 255, 256, 257 - Blazikem 
                            itemPost.cod_pkm == 1   || itemPost.cod_pkm == 2   || itemPost.cod_pkm == 3   || // 10 - Planta   = 1,   2,   3   - Venusaur
                            itemPost.cod_pkm == 374 || itemPost.cod_pkm == 375 || itemPost.cod_pkm == 376 || // 17 - Aço      = 374, 375, 376 - Metagross 
                            itemPost.cod_pkm == 258 || itemPost.cod_pkm == 259 || itemPost.cod_pkm == 260    // 18 - Agua     = 258, 259, 260 - Swampert 
                            )
                        {

                            bFiltro = true;

                            if (itemPost.seq_snipe > nUltimoId)
                            {

                                bot.SendTextMessageAsync("@Legado100Ano", itemPost.cod_pkm + " - " + itemPost.nom_pkm + " - " + itemPost.cp_pkm + " - " +
                                itemPost.iv_pkm + " - " + itemPost.lvl_pkm);

                                System.Threading.Thread.Sleep(2 * 1000);
                                System.Threading.Thread.Sleep(2 * 1000);

                                bot.SendTextMessageAsync("@Legado100Ano", itemPost.des_coordenada);

                                System.Threading.Thread.Sleep(2 * 1000);
                                System.Threading.Thread.Sleep(2 * 1000);

                            }

                        }

                    }

                    #endregion

                    // ---

                    #region Completar 5ª Geracao

                    if (itemPost.iv_pkm == 100 && itemPost.lvl_pkm >= 20 &&

                        (
                        //itemPost.cod_pkm == 524 || itemPost.cod_pkm == 525 || itemPost.cod_pkm == 526 || // 100 capturado
                        itemPost.cod_pkm == 527 || itemPost.cod_pkm == 528 ||
                        itemPost.cod_pkm == 531 ||
                        itemPost.cod_pkm == 532 || itemPost.cod_pkm == 533 || itemPost.cod_pkm == 534 ||
                        
                        //itemPost.cod_pkm == 535 || 
                        itemPost.cod_pkm == 536 || itemPost.cod_pkm == 537 ||

                        itemPost.cod_pkm == 538 ||
                        //itemPost.cod_pkm == 539 ||
                        
                        //itemPost.cod_pkm == 543 || 
                        itemPost.cod_pkm == 544 || itemPost.cod_pkm == 545 ||

                        itemPost.cod_pkm == 550 ||
                        itemPost.cod_pkm == 554 || itemPost.cod_pkm == 555 ||
                        itemPost.cod_pkm == 557 || itemPost.cod_pkm == 558 ||
                        itemPost.cod_pkm == 559 || itemPost.cod_pkm == 560 ||
                        itemPost.cod_pkm == 561 ||
                        
                        //itemPost.cod_pkm == 564 || 
                        itemPost.cod_pkm == 565 || //100 capturado

                        itemPost.cod_pkm == 566 || itemPost.cod_pkm == 567 ||
                        itemPost.cod_pkm == 568 || itemPost.cod_pkm == 569 ||
                        
                        //itemPost.cod_pkm == 588 || 
                        itemPost.cod_pkm == 589 || //100 capturado
                        
                        itemPost.cod_pkm == 594 ||
                        
                        //itemPost.cod_pkm == 595 || 
                        itemPost.cod_pkm == 596 ||

                        itemPost.cod_pkm == 610 || itemPost.cod_pkm == 611 || itemPost.cod_pkm == 612 ||
                        
                        //itemPost.cod_pkm == 613 ||
                        itemPost.cod_pkm == 614 || // 100 capturado

                        itemPost.cod_pkm == 615 ||
                        
                        //itemPost.cod_pkm == 616  
                        itemPost.cod_pkm == 617 // 100 capturado

                        )

                        )
                    {

                        bFiltro = true;

                        if (itemPost.seq_snipe > nUltimoId)
                        {

                            bot.SendTextMessageAsync("@completar5geracao", itemPost.cod_pkm + " - " + itemPost.nom_pkm + " - CP: " + itemPost.cp_pkm + " - " +
                            itemPost.iv_pkm + " - " + itemPost.lvl_pkm);

                            System.Threading.Thread.Sleep(2 * 1000);
                            System.Threading.Thread.Sleep(2 * 1000);

                            bot.SendTextMessageAsync("@completar5geracao", itemPost.des_coordenada);

                            System.Threading.Thread.Sleep(2 * 1000);
                            System.Threading.Thread.Sleep(2 * 1000);

                        }

                    }

                    #endregion 


                    #endregion 










                    // ---


                    if (itemPost.seq_snipe > nUltimoId)
                    {

                        nUltimoId = itemPost.seq_snipe;

                    }

                    //adiciona

                    ////Console.WriteLine(itemPost.seq_snipe + " " + itemPost.cod_pkm + " " + itemPost.nom_pkm + " " + itemPost.lvl_pkm
                    ////    + " " + itemPost.cp_pkm + " " + itemPost.des_coordenada);
                    ////

                    if (dtPK != null)
                    {

                        DataRow a = dtPK.NewRow();

                        a["FILTRO"] = bFiltro;
                        a["GERACAO"] = 0;

                        a["seq_snipe"] = itemPost.seq_snipe;
                        a["cod_pkm"] = itemPost.cod_pkm;
                        a["nom_pkm"] = itemPost.nom_pkm;
                        a["iv_pkm"] = itemPost.iv_pkm;
                        a["cp_pkm"] = itemPost.cp_pkm;
                        a["lvl_pkm"] = itemPost.lvl_pkm;
                        a["des_coordenada"] = itemPost.des_coordenada;
                        a["dta_inclusao"] = itemPost.dta_inclusao;
                        a["cat_pkm"] = itemPost.cat_pkm;
                        a["ind_shiny"] = itemPost.ind_shiny;
                        a["sgl_hex"] = itemPost.sgl_hex;
                        a["sgl_pais"] = itemPost.sgl_pais;
                        a["des_origem"] = itemPost.des_origem;
                        a["per_raridade"] = itemPost.per_raridade;

                        dtPK.Rows.Add(a);

                    }

                }

            }

        }

        public static void IniciaPK()
        {

            bot.OnMessage += Bot_OnMessage;
            bot.OnMessageEdited += Bot_OnMessage;

            bot.StartReceiving();

        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {

                if (e.Message.Text == "/teste deino")
                {

                    ////bot.SendTextMessageAsync(e.Message.Chat.Id, "outra msg"); // envia msg para o bot

                    bot.SendTextMessageAsync("@deino10030", "Funcionando"); // envia msg para o canal


                }
                else if (e.Message.Text == "/teste 100 30")
                {

                    bot.SendTextMessageAsync("@ArgosMonitor100_35", "Funcionando"); // envia msg para o canal

                }
                else if (e.Message.Text == "/teste lista")
                { 


                    bot.SendTextMessageAsync("@RadarArgos10030Lista", "Funcionando"); // envia msg para o canal
                    

                }
                else if (e.Message.Text == "/teste legado")
                {


                    bot.SendTextMessageAsync("@Legado100Ano", "Funcionando"); // envia msg para o canal


                }
                else if (e.Message.Text == "/teste g5")
                {


                    bot.SendTextMessageAsync("@completar5geracao", "Funcionando"); // envia msg para o canal


                }

            }

        }

    }

}

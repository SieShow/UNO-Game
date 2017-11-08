﻿using MauMau.Classes.Background.Estruturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background
{
    class Turno
    {
        private List<Player> jogadores = new List<Player>();
        private Player current;

        public Turno(List<Player> jogadores)
        {
            this.jogadores = jogadores;
            current = jogadores[0];
        }
        /// <summary>
        /// Pega o jogador da vez
        /// </summary>
        /// <returns></returns>
        public Player GetCurrentPlayer()
        {
            return this.current;
        }
        /// <summary>
        /// Finaliza o turno do jogador definindo o proximo na lista
        /// </summary>
        public void End()
        {
            //if (jogadores.GetIndexOf(current) + 1 > jogadores.Count) current = jogadores[0];   
            //else current = jogadores[jogadores.GetIndexOf(current) + 1];
        }
    }
}

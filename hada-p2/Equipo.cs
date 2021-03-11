using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Equipo
    {
        public static int minJugadores { get; set; }
        public static int maxNumeroMovimientos { get; set; }
        public int movimientos { get; private set; }
        public string nombreEquipo { get; private set; }
        private List<Jugador> jugadores { get; set; }
        private List<Jugador> jugadoresExpulsados { get; set; }
        private List<Jugador> jugadoresLesionados { get; set; }
        private List<Jugador> jugadoresRetirados { get; set; }

        public Equipo(int nj, string nom)
        {
            nombreEquipo = nom;
            for(int i = 1; i <= nj; i++)
            {
                Jugador jugadorAux = new Jugador("Jugador_" + i.ToString(), 0, 0, 50, 0);
                jugadores.Add(jugadorAux);
            }
        }

        public bool moverJugadores()
        {
            for (int i = 0; i < jugadores.Count(); i++)
            {
                jugadores[i].mover();
            }

            movimientos++;

            if (jugadores.Count() >= minJugadores)
            {
                return true;
            }
            else return false;
        }

        public void moverJugadoresEnBucle()
        {
            while (jugadores.Count() > 0)
            {
                moverJugadores();
            }
        }

        public int sumarPuntos()
        {
            int sumaPuntos = 0;

            for (int i = 0; i < jugadores.Count(); i++)
            {
                sumaPuntos += jugadores[i].puntos;
            }

            for (int i = 0; i < jugadoresExpulsados.Count(); i++)
            {
                sumaPuntos += jugadores[i].puntos;
            }

            for (int i = 0; i < jugadoresLesionados.Count(); i++)
            {
                sumaPuntos += jugadoresLesionados[i].puntos;
            }

            for (int i = 0; i < jugadoresRetirados.Count(); i++)
            {
                sumaPuntos += jugadoresRetirados[i].puntos;
            }

            return sumaPuntos;
        }

        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return jugadoresExpulsados;
        }

        public List<Jugador> getJugadoresExcedenLimiteFaltas()
        {
            return jugadoresLesionados;
        }

        public List<Jugador> getJugadoresExcedenMinimoEnergia()
        {
            return jugadoresRetirados;
        }

        public override string ToString()
        {
            string s = "";
            s += $"[{nombreEquipo}] Puntos: {sumarPuntos()}; Expulsados: {jugadoresExpulsados.Count()}; Lesionados: {jugadoresLesionados.Count()}; Retirados: {jugadoresRetirados.Count()}";
            
            for(int i = 0; i < jugadores.Count(); i++)
            {
                s += jugadores[i].ToString();
            }

            //hacer también para las otras listas de jugadores

            return s;
        }

        private void cuandoAmonestacionesMaximoExcedido(/*args*/)
        {
        }

        private void cuandoFaltasMaximoExcedido(/*args*/)
        {
        }

        private void cuandoEnergiaMinimaExcedida(/*args*/)
        {

        } 


    }
}

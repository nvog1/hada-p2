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
        }

        public int sumarPuntos()
        {

        }

        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {

        }

        public List<Jugador> getJugadoresExcedenLimiteFaltas()
        {

        }

        public List<Jugador> getJugadoresExcedenMinimoEnergia()
        {

        }

        public override string ToString()
        {
            return base.ToString();//rehacer. esto es lo que viene por defecto
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

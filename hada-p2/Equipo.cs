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
            for(int i = 0; i < nj; i++)
            {
                Jugador jugadorAux = new Jugador("Jugador_" + i.ToString(), 0, 0, 50, 0);
                jugadorAux.amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
                jugadorAux.energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
                jugadorAux.faltasMaximoExcedido += cuandoFaltasMaximoExcedido;
                jugadores.Add(jugadorAux);
            }
        }

        public bool moverJugadores()
        {
            int jugadoresOk = 0; //jugadores que aun pueden jugar despues de este movimiento
            
            for (int i = 0; i < jugadores.Count(); i++)
            {
                if (jugadores[i].todoOk())
                {
                    jugadores[i].mover();
                    if (jugadores[i].todoOk()) jugadoresOk++;
                }
            }

            movimientos++;

            if (jugadoresOk >= minJugadores)
            {
                return true;
            }
            else return false;
        }

        public void moverJugadoresEnBucle()
        {
            while (moverJugadores()) ;
        }

        public int sumarPuntos()
        {
            int sumaPuntos = 0;

            for (int i = 0; i < jugadores.Count(); i++)
            {
                sumaPuntos += jugadores[i].puntos;
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
            s += $"[{nombreEquipo}] Puntos: {sumarPuntos()}; Expulsados: {jugadoresExpulsados.Count()}; Lesionados: {jugadoresLesionados.Count()}; Retirados: {jugadoresRetirados.Count()}\n";
            
            for(int i = 0; i < jugadores.Count(); i++)
            {
                s += jugadores[i].ToString() + "\n";
            }

            return s;
        }

        private void cuandoAmonestacionesMaximoExcedido(object sender, AmonestacionesMaximoExcedidoArgs args)
        {
            Jugador j = (Hada.Jugador)sender;
            jugadoresExpulsados.Add(j);
            Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
            Console.WriteLine("Jugador: " + j.nombre);
            Console.WriteLine("Equipo: " + nombreEquipo);
            Console.WriteLine("Amonestaciones: " + args.amonestaciones.ToString());
        }

        private void cuandoFaltasMaximoExcedido(object sender, FaltasMaximoExcedidoArgs args)
        {
            Jugador j = (Hada.Jugador)sender;
            jugadoresLesionados.Add(j);
            Console.WriteLine("¡¡Número máximo excedido de faltas recibidas. Jugador lesionado!!");
            Console.WriteLine("Jugador: " + j.nombre);
            Console.WriteLine("Equipo: " + nombreEquipo);
            Console.WriteLine("Faltas: " + args.faltas);
        }

        private void cuandoEnergiaMinimaExcedida(object sender, EnergiaMinimaExcedidaArgs args)
        {
            Jugador j = (Hada.Jugador)sender;
            jugadoresRetirados.Add(j);
            Console.WriteLine("¡¡Energía mínima excedida. Jugador retirado!!");
            Console.WriteLine("Jugador: " + j.nombre);
            Console.WriteLine("Equipo: " + nombreEquipo);
            Console.WriteLine("Energía: " + args.energia.ToString() + " %");
        } 


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
   Classe Linha aerea - Uma linha aerea possui um dicionario onde são armazenados todos os Voos
   Cada voo possui um numero de voo unico, que é a chave para ser encontrado no dicionario
          
*/
namespace projetointerdisciplinar
{
    #region Classe Linha Aerea - Representa a abstração de uma linha aerea, com um conjunto de 50 Voos.
    class Airline
    {
        public Dictionary<int, Fligth> airlineFlights { get; private set; } = new Dictionary<int, Fligth>();

        #region Metodos principais da classe
        /// <summary>
        /// Construtor que cria todos os 50 voos com numero de voo fixo.
        /// </summary>
        public Airline(List<string> destinations)
        {
            var rnd = new Random();

            for (int cont = 1, flightNumber = 1011; cont <= 50; cont++)
            {
                flightNumber += 21; // criando numeros de voo fixos
                airlineFlights.Add(flightNumber, new Fligth(destinations[rnd.Next(destinations.Count())]));
            }
            
        }
       

        /*Sobrecarga de metodo Realizar reserva, o cliente pode escolher um assento
        Ou pode reservar sem escolher assento, o assento sendo esolhido aleatoriamente. */
        /// <summary>
        /// Metodo que realiza reserva com um assento escolhido pelo úsuario.
        /// </summary>
        public bool MakeReservation(int flightNumber, string passenger_ID, int seat)
        {
            CheckSeat(flightNumber, ref seat); //Regra 5 - Verificar se o assento é um assento valido e se esta vago.
            return airlineFlights[flightNumber].ReserveSeat(seat, passenger_ID);

        }

        /// <summary>
        /// Metodo que realiza reserva com assento aleatorio, quando o cliente não deseja escolher assento.
        /// </summary>
        public bool MakeReservation(int flightNumber, string passenger_ID)
        {
            Random r = new Random();
            int seat = r.Next(1, 11); //gerando assento aleatorio
            bool hasReserved = false;

            //gerando assento aleatorio.
            while (!hasReserved)
            {
                if (airlineFlights[flightNumber].VacantSeat(seat)) //Regra 5 - Verificar se o assento está disponivel
                {
                    return (airlineFlights[flightNumber].ReserveSeat(seat, passenger_ID));

                }
                seat = r.Next(1, 11);
            }


            return false;
        }

        /// <summary>
        /// Metodo que exclui uma reserva em um determinado voo.
        /// </summary>
        public bool DeleteReservation(int flightNumber, string passenger_ID)
        {
            return airlineFlights[flightNumber].DeleteReservation(passenger_ID);
        }

        /// <summary>
        /// Metodo que imprime os dados de determinado voo especifico
        /// </summary>
        public void Print(int flightNumber)
        {
            if (ExistsFlight(flightNumber))
            {
                Console.Clear();
                Console.Write("\nImpressão de dados do Voo de numero: [" + flightNumber + "]\n");
                Console.Write(airlineFlights[flightNumber].Print());
            }
        }

        /// <summary>
        /// Metodo que imprime os dados de todos voos.
        /// </summary>
        public void PrintAll()
        {
            Console.Clear();
            //iteração para imprimir todos voos do dicionario
            foreach (KeyValuePair<int, Fligth> airlineFlights in airlineFlights)
            {
                Console.Write("\n_________________________________________________________");
                Console.Write("\n\nDados do voo de numero: [" + airlineFlights.Key + "]");
                Console.Write(airlineFlights.Value.Print());
            }
        }

        /// <summary>
        /// Metodo que imprime a ocupação de todos os voos.
        /// </summary>
        public void PrintOccupation()
        {
            foreach (KeyValuePair<int, Fligth> airlineFlights in airlineFlights)
            {
                Console.Write("\nOcupação do voo de numero [" + airlineFlights.Key + "]");
                Console.Write("\nNumero de reservas: " + airlineFlights.Value.GetNumberReservations());
                Console.Write("\nStatus: " + airlineFlights.Value.status + "\n\n");
            }
        }

        #endregion

        #region Metodos extra
        /// <summary>
        /// Metodo que verifica o assento escolhido pelo usuario, esse metodo possibilita a nunca acontecer overbooking.
        /// </summary>
        private void CheckSeat(int flightNumber, ref int seat)
        {
            if (seat >= 1 && seat <= 10) // se o numero de assento é valido
            {
                if (!airlineFlights[flightNumber].VacantSeat(seat)) // se o assento escolhido não está vago
                {
                    Console.Clear();
                    Console.WriteLine("O assento escolhido já está reservado por outra pessoa, escolha um assento valido: \n\nAssentos disponiveis: " + airlineFlights[flightNumber].VacantSeats());
                    Console.Write("\n\nDigite um numero de assento: ");
                    seat = int.Parse(Console.ReadLine());
                    CheckSeat(flightNumber, ref seat);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("O assento escolhido não é valido!!\n\nEscolha um numero entre os assentos disponiveis: " + airlineFlights[flightNumber].VacantSeats());
                Console.Write("\n\nDigite um numero de assento: ");
                seat = int.Parse(Console.ReadLine());
                CheckSeat(flightNumber, ref seat);
            }


            //o metodo é recursivo, ira rodar até o assento ser um assento valido.

        }

        /// <summary>
        /// Metodo que verifica se o voo existe no dicionario
        /// </summary>
        public bool ExistsFlight(int numberFlight)
        {
            if (airlineFlights.ContainsKey(numberFlight))
                return true;
            return false;
        }   
        #endregion




    }
    #endregion
}

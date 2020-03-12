using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace projetointerdisciplinar
{
    class Voo
    {
        private int numberReservations;
        Dictionary<int, string> seats = new Dictionary<int, string>();
        private Queue waitingLine = new Queue(5);

        public string status { get; private set; } = "Open fligth";

        public Voo()
        {
            for(int i =1; i<=10; i++)
                seats.Add(i, null);
        }

        public bool ReservaAssento(int numberSeat, string passenger_ID)
        {
            seats[numberSeat] = passenger_ID;
            numberReservations++;
            //chamar metodo de atualizar voo lotado

            return true;
        }
    }
}

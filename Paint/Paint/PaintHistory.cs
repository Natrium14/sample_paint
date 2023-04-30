using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Paint
{
    class PaintHistory
    {
        
    }

    class Originator
    {
        Bitmap state;
        public Bitmap State
        {
            get { return state; }
            set
            {
                state = value;
            }
        }
        // Creates memento 
        public Memento CreateMemento()
        {
            return (new Memento(state));
        }
        // Restores original state
        public void SetMemento(Memento memento)
        {
            State = memento.State;
        }
    }

    class Memento
    {
        Bitmap state;
        // Constructor
        public Memento(Bitmap state)
        {
            this.state = state;
        }
        public Bitmap State
        {
            get { return state; }
        }
    }

    class Caretaker
    {
        Memento memento;
        public Memento Memento
        {
            set { memento = value; }
            get { return memento; }
        }

        public string Action { get; set; }
        public int Number { get; set; }

        public string Display
        {
            get { return Number.ToString() + " " + Action; }
        }
    }
}


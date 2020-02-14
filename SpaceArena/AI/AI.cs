using System;
using SpaceArena.GameObjects;
using SpaceOnLine;

namespace SpaceArena.AI
{
    public class AI
    {
        public Ship ship = new Ship();
        public SpaceObject target;

        private StateType state;
        private string stateMessage = String.Empty;
        private float time;
        private float dangerFactor = 0;

        public AI()
        {
            state = StateType.IDLE;
            ship.damegeTakenEvent += OnDamage;
        }

        public void OnDamage(Object sender, EventArgs e)
        {
            Console.WriteLine("AI: I'm hitted!!!");
            dangerFactor = 1.0f;
            state = StateType.THINK;
        }

        public void Think()
        {
        }

        public void Idle(float duration)
        {
            stateMessage = "Doing nothing";
        }

        public void Update()
        {
            switch (state)
            {
                case StateType.IDLE:
                    Idle(5);
                    break;

                case StateType.THINK:
                    Think();
                    break;

                case StateType.SCAN:

                    break;

                case StateType.MOVETO:
                    break;

                case StateType.FOLLOW:

                    break;

                case StateType.RUNAWAY:

                    break;

                case StateType.HUNT:

                    break;
            }
        }
    }

    public enum StateType
    {
        IDLE,
        SCAN,
        THINK,
        MOVETO,
        HARVEST,
        FOLLOW,
        RUNAWAY,
        HUNT
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Models.Model;
using Xamarin.Forms;

namespace app.seed
{
    public static class ExtensionMethods
    {

        public static string Str(this Machine machine1)
        {
            return
                String.Format("Machine {0}, type {1}, name {2}, x = {3}, y = {4}",
                machine1.machineId, machine1.Type, machine1.Name, machine1.X, machine1.Y);
        }

        public static bool IsEqual(this Machine machine1, Machine machine2)
        {
            if (machine1.machineId == machine2.machineId &&
                machine1.Type == machine2.Type &&
                machine1.Name.Equals(machine2.Name) &&
                machine1.X == machine2.X && machine1.Y == machine2.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Machine ToClone(this Machine machine)
        {
            Machine new_machine = new Machine();

            new_machine.machineId = machine.machineId;
            new_machine.Type = machine.Type;
            new_machine.Name = machine.Name;
            new_machine.X = machine.X;
            new_machine.Y = machine.Y;

            return new_machine;
        }

        public static Machine MoveMachineLeft(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine.ToClone();

            if (channel != null)
            {
                temp_machine.X--;

                if (MainPage.MinX <= temp_machine.X &&
                    temp_machine.X < MainPage.MaxX &&
                    temp_machine.Y % 2 != 0
                    )
                {
                    Console.WriteLine("Before sending " + machine.Str());
                    channel.ChangeMachinePosition(temp_machine);

                    return temp_machine;
                }
            }

            return machine;
        }

        public static Machine MoveMachineRight(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine.ToClone();

            if (channel != null)
            {
                temp_machine.X++;

                if (MainPage.MinX <= temp_machine.X &&
                    temp_machine.X < MainPage.MaxX &&
                    temp_machine.Y % 2 != 0
                    )
                {
                    channel.ChangeMachinePosition(temp_machine);

                    return temp_machine;
                }
            }

            return machine;
        }

        public static Machine MoveMachineUp(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine.ToClone();

            if (channel != null)
            {
                temp_machine.Y--;

                if (MainPage.MinY <= temp_machine.Y &&
                    temp_machine.Y < MainPage.MaxY &&
                    temp_machine.X % 2 != 0
                    )
                {
                    channel.ChangeMachinePosition(temp_machine);

                    return temp_machine;
                }
            }

            return machine;
        }

        public static Machine MoveMachineDown(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine.ToClone();

            if (channel != null)
            {
                temp_machine.Y++;

                if (MainPage.MinY <= temp_machine.Y &&
                    temp_machine.Y < MainPage.MaxY &&
                    temp_machine.X % 2 != 0
                    )
                {
                    channel.ChangeMachinePosition(temp_machine);

                    return temp_machine;
                }
            }

            return machine;
        }


    }

}

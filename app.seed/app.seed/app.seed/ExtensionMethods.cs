using System;
using System.Collections.Generic;
using System.Text;
using Models.Model;
using Xamarin.Forms;

namespace app.seed
{
    public static class ExtensionMethods
    {

        public static bool Equals(this Machine machine1, Machine machine2)
        {
            if (machine1.machineId == machine2.machineId &&
                machine1.Type == machine2.Type &&
                machine1.Name == machine2.Name &&
                machine1.X == machine2.X && machine1.Y == machine2.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static Machine MoveMachineLeft(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine;

            if (channel != null)
            {
                temp_machine.X--;

                if (MainPage.MinX <= temp_machine.X &&
                    temp_machine.X < MainPage.MaxX &&
                    temp_machine.Y % 2 != 0
                    )
                {
                    Console.WriteLine("Conditions for moveleft are working");
                    //channel.ChangeMachinePosition(machine);
                }
            }

            return temp_machine;
        }

        public static Machine MoveMachineRight(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine;
            
            if (channel != null)
            {
                temp_machine.X++;
                
                if (MainPage.MinX <= temp_machine.X &&
                    temp_machine.X < MainPage.MaxX &&
                    temp_machine.Y % 2 != 0
                    )
                {
                    //channel.ChangeMachinePosition(machine);
                }
            }

            return temp_machine;
        }

        public static Machine MoveMachineUp(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine;
            
            if (channel != null)
            {
                temp_machine.Y--;
                
                if (MainPage.MinY <= temp_machine.Y &&
                    temp_machine.Y < MainPage.MaxY &&
                    temp_machine.X % 2 != 0
                    )
                {
                    //channel.ChangeMachinePosition(machine);
                }
            }

            return temp_machine;
        }

        public static Machine MoveMachineDown(this Machine machine, IContractXam channel)
        {
            Machine temp_machine = machine;
            
            if (channel != null)
            {
                temp_machine.Y++;

                if (MainPage.MinY <= temp_machine.Y &&
                    temp_machine.Y < MainPage.MaxY &&
                    temp_machine.X % 2 != 0
                    )
                {
                    //channel.ChangeMachinePosition(machine);
                }
            }

            return temp_machine;
        }


        public static bool Pause(this Machine machine, IContractXam channel)
        {
            /*
            if (channel != null)
            {
                int temp_y = machine.Y + 1;

                if (RootModel.MinY <= temp_y &&
                    temp_y < RootModel.MaxY &&
                    machine.X % 2 != 0
                    )
                {
                    machine.Y++;

                    return channel.ChangeMachinePosition(machine);
                }

                return false;
            }
            */
            return false;
        }

    }
}

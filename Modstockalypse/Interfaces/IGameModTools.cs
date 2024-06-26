using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modstockalypse.Interfaces
{
    internal interface IGameModTools
    {
        void CarLoadDetails(int carIndex, TextBox detailsText, PictureBox image);
        void CarGetBitmap(string carName, string letter, int index);
        void CarInstall(int carIndex);
        void CarUninstall(int carIndex);
        void SaveOpponentsText();

    }
}

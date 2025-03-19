using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lab1.Core.Models;

namespace WinFormsUi
{
    public partial class MainForm : Form
    {
        List<Production> productions;

        public MainForm()
        {
            InitializeComponent();

            var (shifts, scheduleElements) = StandardSchedules.ThreeShiftFiveBrigade;

            productions = new List<Production>
            {
                new Workshop(
                    name: "Global AutoWorks",
                    manager: "Michael Reynolds",
                    workerCount: 1200,
                    productList: new List<string>
                    {
                        "Engine Blocks",
                        "Transmission Systems",
                        "Brake Discs",
                        "Suspension Components",
                        "Electric Vehicle Batteries"
                    },
                    id: 1,
                    new List<Brigade> {
                        new Brigade(1, "Alpha"),
                        new Brigade(2, "Bronson"),
                        new Brigade(3, "Chuck-Norris"),
                        new Brigade(4, "Vortex"),
                        new Brigade(5, "Titan"),
                    },
                    shifts: shifts,
                    schedule: scheduleElements)
            };

            listBoxProductionItems.Items.Add(productions);

        }

        /*
        // Обработчик клика по элементу списка
        private void listBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем выбранный элемент
            var selectedItem = listBoxItems.SelectedItem;

            if (selectedItem != null)
            {
                // Отображаем информацию о выбранном элементе
                MessageBox.Show($"Selected: {selectedItem.ToString()}");
            }
        }
        */

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void listBoxProductionItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

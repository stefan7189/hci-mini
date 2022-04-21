﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace mini_projekat {
    public partial class MainWindow : Window {

        private bool isChartOn = true;  // true - prikazuje se CHART ; false - prikazuje se TABLE

        private ChartDataSet? chartDataSet;
        private TableDataSet? tableDataSet;
        private StructuredAPIData apiData;

        private string cryptCurr = "";
        private string marketCurr = "";
        private string interval = "";
        private string dataType = "";


        public MainWindow() {
            InitializeComponent();
            ComboBoxSetup();
            apiData = new StructuredAPIData(callAPI());
            chartDataSet = new ChartDataSet(apiData);

            double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };
            WpfPlot1.Plot.AddScatter(dataX, dataY);
            WpfPlot1.Refresh();
        }

        private void ComboBoxSetup() {
            SetUpMarketComboBox(FileReader.GetMarketComboBoxSelections());
            SetUpCurrencyComboBox(FileReader.GetCurrencyComboBoxSelections());
        }

        private void SetUpMarketComboBox(string[] fileLines) {
            prepareComboData(fileLines);
/*
            marketCurrComboBox.ItemsSource = ListData;
            marketCurrComboBox.DisplayMemberPath = "Value";
            marketCurrComboBox.SelectedValuePath = "Id";
*/
        }

        private void SetUpCurrencyComboBox(string[] fileLines) {
            prepareComboData(fileLines);
/*
            cryptoCurrComboBox.ItemsSource = ListData;
            cryptoCurrComboBox.DisplayMemberPath = "Value";
            cryptoCurrComboBox.SelectedValuePath = "Id";
*/
        }

        private List<ComboData> prepareComboData(string[] fileLines) {
            List<ComboData> listData = new();
            foreach (string line in fileLines)
                listData.Add(
                    new ComboData() {
                        Id = line.Split(" -> ")[0],
                        Value = line
                    }
                );
            return listData;
        }

        private void showChartBtn(object sender, RoutedEventArgs e) {
            isChartOn = true;
        }

        private void showTableBtn(object sender, RoutedEventArgs e) {
            isChartOn = false;
        }

        private void submitFormBtn(object sender, RoutedEventArgs e) {
            //chartDataSet = new ChartDataSet();
            tableDataSet = new TableDataSet();

        }

        private void exitAppBtn() {
            Application.Current.Shutdown();
        }

        private Dictionary<string, dynamic> callAPI() {
            return APIController.GetCryptoData("DIGITAL_CURRENCY_DAILY", "ETH", "USD", null);
        }

        private void batn(object sender, RoutedEventArgs e) { }
    }

    public class ComboData {
        public string? Id { get; set; }
        public string? Value { get; set; }
    }

}

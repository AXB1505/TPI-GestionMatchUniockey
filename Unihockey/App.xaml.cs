﻿using Unihockey.Pages;

namespace Unihockey
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}

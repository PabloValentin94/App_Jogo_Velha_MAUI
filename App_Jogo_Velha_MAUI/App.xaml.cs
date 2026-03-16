using Microsoft.Extensions.DependencyInjection;

namespace App_Jogo_Velha_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = new Window(new AppShell());

            window.Height = 650;
            window.Width = 400;

            return window;
        }
    }
}
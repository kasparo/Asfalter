using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace Asfalter.Engine
{
    [RunInstaller(true)]
    public partial class MainServiceInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller _serviceInstaller;
        ServiceProcessInstaller _serviceProcessInstaller;

        public MainServiceInstaller()
        {
            InitializeComponent();

            _serviceInstaller = new ServiceInstaller();
            _serviceProcessInstaller = new ServiceProcessInstaller();
            
            _serviceInstaller.StartType = ServiceStartMode.Manual;
            _serviceInstaller.ServiceName = "AsfalterService";
            _serviceInstaller.DisplayName = "Asfalter Service";
            _serviceInstaller.Description = "Engine service for processing data from units and hosting for API";
            
            _serviceProcessInstaller.Account = ServiceAccount.LocalService;

            Installers.Add(_serviceInstaller);
            Installers.Add(_serviceProcessInstaller);
        }
    }
}

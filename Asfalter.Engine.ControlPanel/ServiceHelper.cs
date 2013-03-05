using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Collections;

namespace Asfalter.Engine.ControlPanel
{
    internal static class ServiceHelper
    {
        static IDictionary _stateCache = new Hashtable();
        static AssemblyInstaller _installer = new AssemblyInstaller("Asfalter.Engine.exe", null);

        public static void Install()
        {
            try
            {
                _installer.Install(_stateCache);
                _installer.Commit(_stateCache);
            }
            catch
            {
                _installer.Rollback(_stateCache);
            }
        }

        public static void Uninstall()
        {
            try
            {
                _installer.Uninstall(_stateCache);
                _installer.Commit(_stateCache);
            }
            catch
            {
                _installer.Rollback(_stateCache);
            }
        }

        public static void Stop()
        {

        }

        public static void Start()
        {

        }
    }
}

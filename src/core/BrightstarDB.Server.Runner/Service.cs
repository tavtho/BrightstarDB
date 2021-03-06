﻿using System;
using System.Linq;
using System.ServiceProcess;
using Nancy.Hosting.Self;

namespace BrightstarDB.Server.Runner
{
    public partial class Service : ServiceBase
    {
        private NancyHost _nancyHost;
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var serviceArgs = new ServiceArgs();
            CommandLine.Parser.ParseArguments(args, serviceArgs); // TODO: parser error reporting should not go to console
            var bootstrapper = ServiceBootstrap.GetBootstrapper(serviceArgs);
            _nancyHost = new NancyHost(bootstrapper, 
                new HostConfiguration{AllowChunkedEncoding = false},
                serviceArgs.BaseUris.Select(x=>new Uri(x)).ToArray());
            _nancyHost.Start();
        }

        protected override void OnStop()
        {
            _nancyHost.Stop();
        }
    }
}

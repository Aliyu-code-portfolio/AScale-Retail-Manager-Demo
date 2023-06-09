﻿using ASRMDesktopUI.Library;
using ASRMDesktopUI.Library.Api;
using ASRMDesktopUserInterface.Helpers;
using ASRMDesktopUserInterface.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ASRMDesktopUserInterface
{
    public class Bootstrapper:BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IApiHelper, ApiHelper>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(classType => classType.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModel => _container.RegisterPerRequest(viewModel, viewModel.ToString(), viewModel));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}

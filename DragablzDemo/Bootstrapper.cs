using Autofac;
using Caliburn.Micro;
using DragablzDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DragablzDemo
{
	public class Bootstrapper : BootstrapperBase
	{
		private Autofac.IContainer container;
		public Bootstrapper() : base( true )
		{
			Initialize();
		}

        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            var executingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes( executingAssembly )
                .AsSelf()
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes( AssemblySource.Instance.ToArray() )
                .Where( type => type.Name.EndsWith( "ViewModel", StringComparison.Ordinal ) )
                .Where( type => !( string.IsNullOrWhiteSpace( type.Namespace ) ) && type.Namespace.EndsWith( nameof( ViewModels ), StringComparison.Ordinal ) )
                .Where( type => type.GetInterface( typeof( INotifyPropertyChanged ).Name ) != null )
                .AsSelf()
                .InstancePerDependency();
            builder.RegisterAssemblyTypes( AssemblySource.Instance.ToArray() )
                .Where( type => type.Name.EndsWith( "View", StringComparison.Ordinal ) )
                .Where( type => !( string.IsNullOrWhiteSpace( type.Namespace ) ) && type.Namespace.EndsWith( "Views", StringComparison.Ordinal ) )
                .AsSelf()
                .InstancePerDependency();
            builder.RegisterType<WindowManager>().As<IWindowManager>().InstancePerLifetimeScope();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().InstancePerLifetimeScope();
            
            builder.RegisterType<Models.InterTabClient>().As<Dragablz.IInterTabClient>().InstancePerLifetimeScope();
            builder.RegisterType<Models.InterLayoutClient>().As<Dragablz.IInterLayoutClient>().InstancePerLifetimeScope();
            container = builder.Build();
        }

        protected override void OnStartup( object sender, StartupEventArgs e )
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance( Type service, string key )
        {
            if ( string.IsNullOrWhiteSpace( key ) )
            {
                if ( container.IsRegistered( service ) )
                    return container.Resolve( service );
            }
            else
            {
                if ( container.IsRegisteredWithKey( key, service ) )
                    return container.ResolveKeyed( key, service );
            }
            throw new Exception( $"Could not locate any instances of contract {key ?? service.Name}." );
        }

        protected override IEnumerable<object> GetAllInstances( Type service )
        {
            return container.Resolve( typeof( IEnumerable<> ).MakeGenericType( service ) ) as IEnumerable<object>;
        }

        protected override void BuildUp( object instance )
        {
            container.InjectProperties( instance );
        }

        protected override void OnUnhandledException( object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e )
        {
            Console.WriteLine( $"Unhandled exception: {e.Exception.ToString()}" );
        }
    }
}

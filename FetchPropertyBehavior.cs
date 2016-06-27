using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Selection;
using loctool.Client.Classes.Request;

namespace loctool.Client.Behaviors
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class FetchPropertyBehavior : Behavior<DependencyObject>
    {
        private DependencyObject Object
        {
            get { return this.AssociatedObject; }
        }

        public string PropertyPath { get; set; }
        
        public static readonly DependencyProperty FetchProperty = DependencyProperty.Register("Fetch",
        typeof(IRequest), typeof(FetchPropertyBehavior),
        new PropertyMetadata(OnFetchPropertyChanged)); //only one way to source

        public IRequest Fetch
        {
            get { return (IRequest)GetValue(FetchProperty); }
            set { SetValue(FetchProperty, value); }
        }

        private static void OnFetchPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            //clean up old command
            IRequest oldedit = args.OldValue as IRequest;
            if (oldedit != null)
            {
                oldedit.FetchMethod = null;
            }
            FetchPropertyBehavior behavior = target as FetchPropertyBehavior;
            if (behavior == null)
                return;

            //setup new command
            IRequest newedit = args.NewValue as IRequest;
            if (newedit == null)
                return;

            
            newedit.FetchMethod = behavior.FetchPropertyMethod;
        }

        private object FetchPropertyMethod(object parameters)
        {
            if (string.IsNullOrEmpty(PropertyPath))
                return null;

            var propertynamepath = PropertyPath.Split(new[] { '.' });
            object currentproperty = this.Object;
            for (int i = 0; i < propertynamepath.Length; i++)
            {
                var propertyinfo = currentproperty.GetType().GetProperty(propertynamepath[i]);
                if (propertyinfo == null)
                {
                    Debug.WriteLine(string.Format("Could not find property {0} in path {1}", propertynamepath[i], PropertyPath));
                    return null;
                }
                currentproperty = propertyinfo.GetValue(currentproperty);
            }
            return currentproperty;
        }
    }
}

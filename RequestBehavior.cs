using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using loctool.Client.Classes.Request;

namespace loctool.Client.Behaviors
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public abstract class RequestBehavior<T> : Behavior<T> where T : DependencyObject
    {
        public IRequest BehaviorRequest
        {
            get { return (IRequest)this.GetValue(BehaviorRequestProperty); }
            set { this.SetValue(BehaviorRequestProperty, value); }
        }

        public static readonly DependencyProperty BehaviorRequestProperty =
            DependencyProperty.Register("BehaviorRequest", typeof(IRequest), typeof(RequestBehavior<T>), new PropertyMetadata(OnRequestPropertyChanged));

        private static void OnRequestPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RequestBehavior<T> behavior = target as RequestBehavior<T>;
            if (behavior == null)
                return;

            //setup new command
            IRequest newedit = args.NewValue as IRequest;
            if (newedit == null)
                return;

            newedit.FetchMethod = behavior.executed;
        }

        protected abstract object executed(object parameters);

        public ICommand BehaviorCommand
        {
            get { return (ICommand)this.GetValue(BehaviorCommandProperty); }
            set { this.SetValue(BehaviorCommandProperty, value); }
        }

        public static readonly DependencyProperty BehaviorCommandProperty =
            DependencyProperty.Register("BehaviorCommand", typeof(ICommand), typeof(RequestBehavior<T>), new PropertyMetadata(OnCommandPropertyChanged));

        private static void OnCommandPropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
        {
            RequestBehavior<T> behavior = target as RequestBehavior<T>;
            if (behavior == null)
                return;
        }

        
    }
}

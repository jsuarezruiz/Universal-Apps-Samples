namespace GesturesBehavior.Behaviors
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;
    using Base;
    using System.Windows.Input;

    public enum SwipeDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public class SwipeBehavior : Behavior<UIElement>
    {
        private const double Min = 0.5;
        private const double Max = 1;
        
        public ICommand SwipeCommand
        {
            get { return (ICommand)GetValue(SwipeCommandProperty); }
            set { SetValue(SwipeCommandProperty, value); }
        }

        public static readonly DependencyProperty SwipeCommandProperty =
            DependencyProperty.Register("SwipeCommand",
            typeof(ICommand),    
            typeof(SwipeBehavior),  
            new PropertyMetadata(null));

        public static ICommand GetSwipeCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(SwipeCommandProperty);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.ManipulationMode =
                this.AssociatedObject.ManipulationMode |
                ManipulationModes.TranslateX |
                ManipulationModes.TranslateY;

            this.AssociatedObject.ManipulationCompleted += OnManipulationCompleted;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.ManipulationCompleted -= OnManipulationCompleted;
        }

        private void OnManipulationCompleted(object sender,
            ManipulationCompletedRoutedEventArgs e)
        {
            bool right = e.Velocities.Linear.X.CompareTo(Min) >= 0 && e.Velocities.Linear.X.CompareTo(Max) <= 0;
            bool left = e.Velocities.Linear.X.CompareTo(-Max) >= 0 && e.Velocities.Linear.X.CompareTo(-Min) <= 0;
            bool up = e.Velocities.Linear.Y.CompareTo(-Max) >= 0 && e.Velocities.Linear.Y.CompareTo(-Min) <= 0;
            bool down = e.Velocities.Linear.Y.CompareTo(Min) >= 0 && e.Velocities.Linear.Y.CompareTo(Max) <= 0;

            var swipeCommand = GetSwipeCommand(this);

            if (swipeCommand != null)
            {
                if (right && !(up || down))
                    swipeCommand.Execute(SwipeDirection.Right);
                if (left && !(up || down))
                    swipeCommand.Execute(SwipeDirection.Left);
                if (up && !(right || left))
                    swipeCommand.Execute(SwipeDirection.Up);
                if (down && !(right || left))
                    swipeCommand.Execute(SwipeDirection.Down);
            }
        }
    }
}

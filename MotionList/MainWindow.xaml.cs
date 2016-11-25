using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dragablz;

namespace SweetNewItemsControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object[] _order;

        public MainWindow()
        {
            InitializeComponent();

            AddHandler(DragablzItem.DragStarted, new DragablzDragStartedEventHandler(ItemDragStarted), true);
            AddHandler(DragablzItem.DragCompleted, new DragablzDragCompletedEventHandler(ItemDragCompleted), true);
        }

        private void ItemDragStarted(object sender, DragablzDragStartedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)e.DragablzItem.DataContext;

            System.Diagnostics.Trace.WriteLine($"User started to drag item with Tag {frameworkElement.Tag}.");           
        }

        private void ItemDragCompleted(object sender, DragablzDragCompletedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)e.DragablzItem.DataContext;
            System.Diagnostics.Trace.WriteLine($"User finished dragging item with Tag {frameworkElement.Tag}.");

            if (_order == null) return;

            System.Diagnostics.Trace.Write("Order is now: ");
            foreach (var fe in _order.OfType<FrameworkElement>())
            {
                System.Diagnostics.Trace.Write(fe.Tag + " ");
            }
            System.Diagnostics.Trace.WriteLine("");
        }

        private void StackPositionMonitor_OnOrderChanged(object sender, OrderChangedEventArgs e)
        {
            _order = e.NewOrder;
        }
    }
}

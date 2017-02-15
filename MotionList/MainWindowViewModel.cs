using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Dragablz;

namespace SweetNewItemsControl
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Data = new ObservableCollection<int>
            {
                1,
                2,
                3
            };

            AddCommand = new AnotherCommandImplementation(dic => Add((DragablzItemsControl)dic));
        }

        public IEnumerable<int> Data { get; }

        public ICommand AddCommand { get; }

        private void Add(DragablzItemsControl dragablzItemsControl)
        {
            //the items control does not keep your source collection sorted as per what is on screen.
            //this causes too much implementation complexity, and creates a dog-chasing-its-own tail scenario.
            //so, you can use a PositionMonitor (defined in MainWindow.xaml) to monitor rendering order.
            //However, if you want to add a new item to your underlying source (where the sort is not applied)
            //then you can use the .AddToSource() method which allows to to insert and control the rendered sort of the new item.

            //if you're in MVVM you might not consider this "pure", but hey, sometimes you gotta compromise. In this example
            //I pass in the dragablzItemsControl from the Button CommandParameter. You _could_ implement a custom IItemsOrganiser 
            //to go full MVVM...but personally I would consider this overkill for minimal gain.

            var newItem = Data.Max() + 1;
            dragablzItemsControl.AddToSource(newItem, AddLocationHint.Last);

            //your source collection will have the new item added (but sort is mnever applied, for that use dragablzItemsControl.PositionMonitor, example also in the XAML).
            Debug.Assert(Data.Contains(newItem));
        }
    }
}

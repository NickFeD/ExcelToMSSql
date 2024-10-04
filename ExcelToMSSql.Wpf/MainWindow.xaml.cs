using DB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelToMSSql.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DatabaseContext _context;

    private CollectionViewSource clientViewSource;
    public MainWindow(DatabaseContext context)
    {
        InitializeComponent();
        _context = context;
        clientViewSource =
                (CollectionViewSource)FindResource(nameof(clientViewSource));
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

        // load the entities into EF Core
        _context.Clients.Load();

        // bind to the source
        clientViewSource.Source =
            _context.Clients.Local.ToObservableCollection();
    }

    private void SaveClick(object sender, RoutedEventArgs e)
    {
        if (clientDataGrid.CommitEdit())
            clientDataGrid.CancelEdit();
        // all changes are automatically tracked, including
        // deletes!
        _context.SaveChanges();

        // this forces the grid to refresh to latest values
        clientDataGrid.Items.Refresh();
    }

    private void AddClick(object sender, RoutedEventArgs e)
    {
        _context.Clients.Add(new());
        _context.SaveChanges();
        // this forces the grid to refresh to latest values
        clientDataGrid.Items.Refresh();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        // clean up database connections
        _context.Dispose();
        base.OnClosing(e);
    }

    private void AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        string headername = e.Column.Header.ToString();

        //Cancel the column you don't want to generate
        if (headername == "Id")
            e.Cancel = true;
    }
}
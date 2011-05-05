using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Xml.Linq;
using Rbi.Search;
using Rbi.Search.Configuration;
using Rbi.Search.Formatters;

namespace API_Test_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //Get a singleton Idol Server for the target URI
            var idolServer = IdolServer.GetInstance<RawResultSet>(new Uri("http://10.55.109.89:8210"),
                                                          new Uri("http://10.55.109.89:8212"),
                                                          new Uri("http://10.55.109.89:8211"));
         
            //Get a new query of the right return type
            var query = idolServer.GetQuery(RawResultSet.FormatResults);
            //query.Parametric.Where()
            //var parametric = idolServer.GetParametrics(RawResultSet.FormatResults);
            
            query.Where("test");
            query.Where(ExampleDocument.ArticleId.IsEqual(243990) & ExampleDocument.StatusId.IsEqual(30));
            //query.Select(CWArticle.ArticleId, CWArticle.StatusId);

            //Execute query
            var results = query.Execute();
            textBlockResultXml.Text = query.GetCommand() + Environment.NewLine + results.Result;

            Field field = idolServer["Title"];
            Field field2 = idolServer["number"];



            Term term = ExampleDocument.Location.IsInRadius(1.55d, 1.2d, 40d) |
                        ExampleDocument.Location.IsInRadius(10, 10, 50);

            Term term2 = Term.Or(ExampleDocument.Location.IsInRadius(1.55d, 1.2d, 40d),
                                 ExampleDocument.Location.IsInRadius(10, 10, 50));

            Term term3 =
                ExampleDocument.Location.IsInRadius(1.55d, 1.2d, 40d).Or(ExampleDocument.Location.IsInRadius(10, 10, 50));


            //textBlock1.Text = term.ToString() + Environment.NewLine + term2.ToString();

            //textBlock1.Text = term + Environment.NewLine + term2 + Environment.NewLine + term3;

            


            
        }
    }
}

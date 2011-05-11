using System;
using System.Windows;
using Rbi.Search;
using Rbi.Search.Configuration;
using Rbi.Search.Formatters;

namespace API_Test_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            //Get a singleton Idol Server for the target URI
            var idolServer = IdolServer.GetInstance<RawResultSet>(new Uri("http://10.55.109.89:8210"),
                                                          new Uri("http://10.55.109.89:8212"),
                                                          new Uri("http://10.55.109.89:8211"));
         
            //Get a new query of the right return type
            var query = idolServer.GetQuery(RawResultSet.FormatResults);
            
            query.Where(TextBoxQueryText.Text);
            query.Where(ExampleDocument.ArticleId.IsEqual(243990) & ExampleDocument.StatusId.IsEqual(30));
            query.OrderBy(Sort.ByRelevance.And(Sort.ByFieldIncreasing(ExampleDocument.ArticleId))); //Long hand for field sorts
            query.OrderBy(ExampleDocument.ArticleId.SortIncreasing()); //Short for field sorts

            //query.Where(ExampleDocument.Author.IsEqual("test"));

            //Execute query
            var results = query.Execute();
            textBlockResultXml.Text = query.Command + Environment.NewLine + results.Result;

            query.ExecuteCompleted += QueryExecuteCompleted;
            query.ExecuteAsync();

            /*Field field = idolServer["Title"];
            Field field2 = idolServer["number"];

            Term term = ExampleDocument.Location.IsInRadius(1.55d, 1.2d, 40d) |
                        ExampleDocument.Location.IsInRadius(10, 10, 50);

            Term term2 = Term.Or(ExampleDocument.Location.IsInRadius(1.55d, 1.2d, 40d),
                                 ExampleDocument.Location.IsInRadius(10, 10, 50));

            Term term3 =
                ExampleDocument.Location.IsInRadius(1.55d, 1.2d, 40d).Or(ExampleDocument.Location.IsInRadius(10, 10, 50));


            //textBlock1.Text = term.ToString() + Environment.NewLine + term2.ToString();

            //textBlock1.Text = term + Environment.NewLine + term2 + Environment.NewLine + term3;
            */
            
        }

        void QueryExecuteCompleted(object sender, ExecuteCompletedEventArgs<RawResultSet> e)
        {
            textBlockResultXml.Text = e.Result.Result.ToString();
        }
    }
}

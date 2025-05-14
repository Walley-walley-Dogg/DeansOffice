using DeansOffice.Database.Models;
using DeansOffice.Pages;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DeansOffice.ViewModels
{
    public class MainViewModel
    {
        public ICommand NavigateToStudentsCommand { get; }
        public ICommand NavigateToGroupsCommand { get; }

        public MainViewModel()
        {
            NavigateToStudentsCommand = new RelayCommand(NavigateToStudents);
            NavigateToGroupsCommand = new RelayCommand(NavigateToGroups);
        }

        private void NavigateToStudents()
        {
            NavigationService.Navigate(new StudentsPage());
        }  
        private void NavigateToGroups()
        {
            NavigationService.Navigate(new GroupsPage());
        }
        public static class NavigationService
        {
            public static Frame MainFrame { get; set; }

            public static void Navigate(Page page)
            {
                if (MainFrame != null)
                {
                    MainFrame.Navigate(page);
                }
            }

            public static void Navigate<T>() where T : Page, new()
            {
                Navigate(new T());
            }
        }
    }
}

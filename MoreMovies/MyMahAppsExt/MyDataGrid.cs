using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyMahAppsExt
{
    public class MyDataGrid :  DataGrid
    {
        public MyDataGrid() : base()
        {
            this.ContextMenuOpening += MyDataGrid_ContextMenuOpening;
        }

        public void MyDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            
            if (this.ItemsSource == null || this.Items == null || this.Items.Count == 0 || this.SelectedIndex == -1) return;
            FrameworkElement fe = e.Source as FrameworkElement;

            ContextMenu theMenu = new ContextMenu();
            DataGridRow dataRow = (DataGridRow)this.ItemContainerGenerator.ContainerFromIndex(this.SelectedIndex);
            DataGridCell cell = null;
            DataGridColumnHeader columnHeader;
            List<DataGridColumnHeader> columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(this);
            string cellValue = "";
            int iCell = 0;
            for (int i = 0; i < columnHeaders.Count(); i++)
            {
                columnHeader = columnHeaders[i];
                if (columnHeader.Content == null) continue;
                if (columnHeader.Visibility == System.Windows.Visibility.Hidden) continue;
                MenuItem mia = new MenuItem();
                while (iCell < columnHeaders.Count())
                {
                    cell = this.GetCell(dataRow, iCell++);
                    if (cell == null || cell.Column.Header == null) continue;
                    if ((cell.Column.Header.GetType() == typeof(string)) && ((string)cell.Column.Header).Equals(columnHeader.Content) == false)
                        continue;
                    break;
                }
                cellValue = "";
                if (cell != null)
                    if (cell.Content.GetType() == typeof(MahApps.Metro.Controls.NumericUpDown))
                        cellValue = (((MahApps.Metro.Controls.NumericUpDown)cell.Content).Value != null) ? ((MahApps.Metro.Controls.NumericUpDown)cell.Content).Value.ToString() : "";
                    else if (cell.ToString().IndexOf("DataGridCell:") >= 0)
                    {
                        cellValue = cell.ToString();
                        cellValue = cellValue.Substring(cellValue.IndexOf("DataGridCell:") + 14);
                    }
                mia.Header = columnHeader.Content.ToString() + ":" + cellValue;
                mia.Tag = cellValue;
                mia.Click += mia_Click;
                theMenu.Items.Add(mia);
            }

            fe.ContextMenu = theMenu;
            fe.ContextMenu.IsOpen = true;
        }

        void mia_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                MenuItem mia = (MenuItem)sender;
                Clipboard.SetText((string)mia.Tag);
            }
            catch (Exception) {}
        }



        public List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

    }
}

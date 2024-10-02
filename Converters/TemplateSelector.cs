using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Chinh_C1_Cinemas
{
    public class TemplateSelector8 : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item != null && item is Schedule)
            {
                var nguoidung = item as Schedule;
                switch (nguoidung.Status)
                {
                    case true: // đang sử dụng
                        return (container as FrameworkElement).FindResource("SuDung") as DataTemplate;
                    case false: // chưa sử dụng
                        return (container as FrameworkElement).FindResource("ChuaSuDung") as DataTemplate;
                    default:
                        return (container as FrameworkElement).FindResource("ChuaSuDung") as DataTemplate;
                }
            }
            return null;
        }
    }
}

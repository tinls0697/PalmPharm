//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("tabbedpage.Views.AlarmPage.xaml", "Views/AlarmPage.xaml", typeof(global::tabbedpage.Views.AlarmPage))]

namespace tabbedpage.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\AlarmPage.xaml")]
    public partial class AlarmPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ContentPage AlarmViewPage;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView AlarmlistView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label EmptyMessage;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(AlarmPage));
            AlarmViewPage = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ContentPage>(this, "AlarmViewPage");
            AlarmlistView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "AlarmlistView");
            EmptyMessage = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "EmptyMessage");
        }
    }
}

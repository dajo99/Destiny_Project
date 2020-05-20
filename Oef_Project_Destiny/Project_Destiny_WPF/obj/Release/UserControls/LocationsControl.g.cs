﻿#pragma checksum "..\..\..\UserControls\LocationsControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CB380972A25EB8A72982B66ABD982F4F403404BA960C568A34B4F6EF350C7A50"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Project_Destiny_WPF.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Project_Destiny_WPF.UserControls {
    
    
    /// <summary>
    /// LocationsUserControl
    /// </summary>
    public partial class LocationsUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblLocations;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbWorld;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dbLocations;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLocations;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtArea;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnWijzig;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnToeevogen;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\UserControls\LocationsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnVerwijderen;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Project_Destiny_WPF;component/usercontrols/locationscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\LocationsControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\UserControls\LocationsControl.xaml"
            ((Project_Destiny_WPF.UserControls.LocationsUserControl)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblLocations = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.cmbWorld = ((System.Windows.Controls.ComboBox)(target));
            
            #line 26 "..\..\..\UserControls\LocationsControl.xaml"
            this.cmbWorld.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbWorld_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dbLocations = ((System.Windows.Controls.DataGrid)(target));
            
            #line 30 "..\..\..\UserControls\LocationsControl.xaml"
            this.dbLocations.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dbLocations_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtLocations = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtArea = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.BtnWijzig = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\UserControls\LocationsControl.xaml"
            this.BtnWijzig.Click += new System.Windows.RoutedEventHandler(this.BtnWijzig_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BtnToeevogen = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\UserControls\LocationsControl.xaml"
            this.BtnToeevogen.Click += new System.Windows.RoutedEventHandler(this.BtnToeevogen_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BtnVerwijderen = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\UserControls\LocationsControl.xaml"
            this.BtnVerwijderen.Click += new System.Windows.RoutedEventHandler(this.BtnVerwijderen_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

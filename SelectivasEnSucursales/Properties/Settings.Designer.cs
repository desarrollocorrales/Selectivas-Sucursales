﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.17929
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SelectivasEnSucursales.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::ClasesDeConfiguracion.ConfiguracionFB Configuracion {
            get {
                return ((global::ClasesDeConfiguracion.ConfiguracionFB)(this["Configuracion"]));
            }
            set {
                this["Configuracion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int Almacen {
            get {
                return ((int)(this["Almacen"]));
            }
            set {
                this["Almacen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Sucursal")]
        public string Sucursal {
            get {
                return ((string)(this["Sucursal"]));
            }
            set {
                this["Sucursal"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://")]
        public string URLWebService {
            get {
                return ((string)(this["URLWebService"]));
            }
            set {
                this["URLWebService"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://servidorx:82/SegCoServices/ServicioWeb.asmx")]
        public string SelectivasEnSucursales_SegConService_SegConWebService {
            get {
                return ((string)(this["SelectivasEnSucursales_SegConService_SegConWebService"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://servidor:84/SegConFriolala/ServicioWeb.asmx")]
        public string URLWebServiceFriolala {
            get {
                return ((string)(this["URLWebServiceFriolala"]));
            }
            set {
                this["URLWebServiceFriolala"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://200.23.18.85:84/segconfriolala/ServicioWeb.asmx")]
        public string SelectivasEnSucursales_WebServiceFriolala_ServicioWeb {
            get {
                return ((string)(this["SelectivasEnSucursales_WebServiceFriolala_ServicioWeb"]));
            }
        }
    }
}

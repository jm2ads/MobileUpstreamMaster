﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Frontend.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MessageText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessageText() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Frontend.Core.Resources.MessageText", typeof(MessageText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cerrar.
        /// </summary>
        public static string Close {
            get {
                return ResourceManager.GetString("Close", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Campo obligatorio.
        /// </summary>
        public static string FieldRequired {
            get {
                return ResourceManager.GetString("FieldRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ListaEmpleados.
        /// </summary>
        public static string GoToListaEmpleados {
            get {
                return ResourceManager.GetString("GoToListaEmpleados", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La última sincronización no fue completada. Por favor revise el listado de fallas..
        /// </summary>
        public static string HasErrorSyncMessage {
            get {
                return ResourceManager.GetString("HasErrorSyncMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hubo un problema, reintente mas tarde..
        /// </summary>
        public static string HuboUnProblema {
            get {
                return ResourceManager.GetString("HuboUnProblema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sin Conexion.
        /// </summary>
        public static string NotConection {
            get {
                return ResourceManager.GetString("NotConection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Intente obtener una conexion estable de datos 3G, 4G o WIFI para poder realizar la validación.
        /// </summary>
        public static string NotConectionTry {
            get {
                return ResourceManager.GetString("NotConectionTry", resourceCulture);
            }
        }
    }
}
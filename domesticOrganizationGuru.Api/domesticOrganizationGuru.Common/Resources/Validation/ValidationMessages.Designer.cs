﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace domesticOrganizationGuru.Common.Resources.Validation {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("domesticOrganizationGuru.Common.Resources.Validation.ValidationMessages", typeof(ValidationMessages).Assembly);
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
        ///   Looks up a localized string similar to Note should last longer than 0 minutes for anyone to see it.
        /// </summary>
        public static string LifetimeInformation {
            get {
                return ResourceManager.GetString("LifetimeInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Note must have a name.
        /// </summary>
        public static string NameEmptyInformation {
            get {
                return ResourceManager.GetString("NameEmptyInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no note. Please orovide one!.
        /// </summary>
        public static string NoteEmptyInformation {
            get {
                return ResourceManager.GetString("NoteEmptyInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Note lenght should stay within the boarders of sanity, which is in our case .
        /// </summary>
        public static string NoteMaxLengthPrefix {
            get {
                return ResourceManager.GetString("NoteMaxLengthPrefix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  characters.
        /// </summary>
        public static string NoteMaxLengthSufix {
            get {
                return ResourceManager.GetString("NoteMaxLengthSufix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Every entity shall be named to exist in universe.
        /// </summary>
        public static string ProvideNameInformation {
            get {
                return ResourceManager.GetString("ProvideNameInformation", resourceCulture);
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeBlue.Api.VinylShop.Presentation {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class BadRequestMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BadRequestMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BeBlue.Api.VinylShop.Presentation.BadRequestMessages", typeof(BadRequestMessages).Assembly);
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
        ///   Looks up a localized string similar to A cart must have a list with wished albums id&apos;s  to buy.
        /// </summary>
        public static string CartMustHaveAListWithAlbumsIds {
            get {
                return ResourceManager.GetString("CartMustHaveAListWithAlbumsIds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A request to create a cart can&apos;t be null.
        /// </summary>
        public static string CartRequestCantBeNull {
            get {
                return ResourceManager.GetString("CartRequestCantBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Album Id can&apos;t be null or white space.
        /// </summary>
        public static string MustProvideAlbumId {
            get {
                return ResourceManager.GetString("MustProvideAlbumId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Genre can&apos;t be null  or whtie space.
        /// </summary>
        public static string MustProvideGenre {
            get {
                return ResourceManager.GetString("MustProvideGenre", resourceCulture);
            }
        }
    }
}

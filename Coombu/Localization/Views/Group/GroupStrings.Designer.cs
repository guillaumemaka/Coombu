﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18033
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ViewRes {
    using System;
    
    
    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GroupStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GroupStrings() {
        }
        
        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Localization.Views.Group.GroupStrings", typeof(GroupStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
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
        ///   Recherche une chaîne localisée semblable à Entrez le nom du groupe à créer.
        /// </summary>
        public static string CreateButtonPlaceHolder {
            get {
                return ResourceManager.GetString("CreateButtonPlaceHolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Créer le groupe.
        /// </summary>
        public static string CreateSubmitButtonTitle {
            get {
                return ResourceManager.GetString("CreateSubmitButtonTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Vous ({0} {1}).
        /// </summary>
        public static string IsOwnerMessage {
            get {
                return ResourceManager.GetString("IsOwnerMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Rejoindre.
        /// </summary>
        public static string Join {
            get {
                return ResourceManager.GetString("Join", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Membre.
        /// </summary>
        public static string Member {
            get {
                return ResourceManager.GetString("Member", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Nombre de membre: .
        /// </summary>
        public static string MemberCount {
            get {
                return ResourceManager.GetString("MemberCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Membres.
        /// </summary>
        public static string Members {
            get {
                return ResourceManager.GetString("Members", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Aucun membre n&apos;a encore rien posté, ne soyez pas timide, lancez vous !.
        /// </summary>
        public static string NoEntry {
            get {
                return ResourceManager.GetString("NoEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Il n&apos;existe aucun groupe pour le moment soyez le premier à en créer un !.
        /// </summary>
        public static string NoGroupEntry {
            get {
                return ResourceManager.GetString("NoGroupEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Propriétaire.
        /// </summary>
        public static string Owner {
            get {
                return ResourceManager.GetString("Owner", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Groupe {0}.
        /// </summary>
        public static string StringGroup {
            get {
                return ResourceManager.GetString("StringGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Recherche une chaîne localisée semblable à Veuillez saisir le nom du groupe à créer !.
        /// </summary>
        public static string ValidationRequiredMessage {
            get {
                return ResourceManager.GetString("ValidationRequiredMessage", resourceCulture);
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.FoodManagerServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Food", Namespace="http://schemas.datacontract.org/2004/07/FoodManagerService.Modele")]
    [System.SerializableAttribute()]
    public partial class Food : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ExpirationDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdCategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdSubCategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int QuantityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ExpirationDate {
            get {
                return this.ExpirationDateField;
            }
            set {
                if ((this.ExpirationDateField.Equals(value) != true)) {
                    this.ExpirationDateField = value;
                    this.RaisePropertyChanged("ExpirationDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdCategory {
            get {
                return this.IdCategoryField;
            }
            set {
                if ((this.IdCategoryField.Equals(value) != true)) {
                    this.IdCategoryField = value;
                    this.RaisePropertyChanged("IdCategory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdSubCategory {
            get {
                return this.IdSubCategoryField;
            }
            set {
                if ((this.IdSubCategoryField.Equals(value) != true)) {
                    this.IdSubCategoryField = value;
                    this.RaisePropertyChanged("IdSubCategory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Quantity {
            get {
                return this.QuantityField;
            }
            set {
                if ((this.QuantityField.Equals(value) != true)) {
                    this.QuantityField = value;
                    this.RaisePropertyChanged("Quantity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Unit {
            get {
                return this.UnitField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitField, value) != true)) {
                    this.UnitField = value;
                    this.RaisePropertyChanged("Unit");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Recipe", Namespace="http://schemas.datacontract.org/2004/07/FoodManagerService.Modele")]
    [System.SerializableAttribute()]
    public partial class Recipe : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImagePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextXmlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Author {
            get {
                return this.AuthorField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthorField, value) != true)) {
                    this.AuthorField = value;
                    this.RaisePropertyChanged("Author");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImagePath {
            get {
                return this.ImagePathField;
            }
            set {
                if ((object.ReferenceEquals(this.ImagePathField, value) != true)) {
                    this.ImagePathField = value;
                    this.RaisePropertyChanged("ImagePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TextXml {
            get {
                return this.TextXmlField;
            }
            set {
                if ((object.ReferenceEquals(this.TextXmlField, value) != true)) {
                    this.TextXmlField = value;
                    this.RaisePropertyChanged("TextXml");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FoodManagerServiceReference.IFoodManagerService")]
    public interface IFoodManagerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/GetFoodList", ReplyAction="http://tempuri.org/IFoodManagerService/GetFoodListResponse")]
        Client.FoodManagerServiceReference.Food[] GetFoodList(string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/GetFoodList", ReplyAction="http://tempuri.org/IFoodManagerService/GetFoodListResponse")]
        System.Threading.Tasks.Task<Client.FoodManagerServiceReference.Food[]> GetFoodListAsync(string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/CreateFood", ReplyAction="http://tempuri.org/IFoodManagerService/CreateFoodResponse")]
        System.Nullable<int> CreateFood(Client.FoodManagerServiceReference.Food food, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/CreateFood", ReplyAction="http://tempuri.org/IFoodManagerService/CreateFoodResponse")]
        System.Threading.Tasks.Task<System.Nullable<int>> CreateFoodAsync(Client.FoodManagerServiceReference.Food food, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/UpdateFood", ReplyAction="http://tempuri.org/IFoodManagerService/UpdateFoodResponse")]
        System.Nullable<bool> UpdateFood(Client.FoodManagerServiceReference.Food food, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/UpdateFood", ReplyAction="http://tempuri.org/IFoodManagerService/UpdateFoodResponse")]
        System.Threading.Tasks.Task<System.Nullable<bool>> UpdateFoodAsync(Client.FoodManagerServiceReference.Food food, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/DeleteFood", ReplyAction="http://tempuri.org/IFoodManagerService/DeleteFoodResponse")]
        System.Nullable<bool> DeleteFood(Client.FoodManagerServiceReference.Food food, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/DeleteFood", ReplyAction="http://tempuri.org/IFoodManagerService/DeleteFoodResponse")]
        System.Threading.Tasks.Task<System.Nullable<bool>> DeleteFoodAsync(Client.FoodManagerServiceReference.Food food, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/CreateRecipe", ReplyAction="http://tempuri.org/IFoodManagerService/CreateRecipeResponse")]
        System.Nullable<int> CreateRecipe(Client.FoodManagerServiceReference.Recipe recipe, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/CreateRecipe", ReplyAction="http://tempuri.org/IFoodManagerService/CreateRecipeResponse")]
        System.Threading.Tasks.Task<System.Nullable<int>> CreateRecipeAsync(Client.FoodManagerServiceReference.Recipe recipe, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/GetRecipesList", ReplyAction="http://tempuri.org/IFoodManagerService/GetRecipesListResponse")]
        Client.FoodManagerServiceReference.Recipe[] GetRecipesList(string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/GetRecipesList", ReplyAction="http://tempuri.org/IFoodManagerService/GetRecipesListResponse")]
        System.Threading.Tasks.Task<Client.FoodManagerServiceReference.Recipe[]> GetRecipesListAsync(string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/UpdateRecipe", ReplyAction="http://tempuri.org/IFoodManagerService/UpdateRecipeResponse")]
        System.Nullable<bool> UpdateRecipe(Client.FoodManagerServiceReference.Recipe recipe, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/UpdateRecipe", ReplyAction="http://tempuri.org/IFoodManagerService/UpdateRecipeResponse")]
        System.Threading.Tasks.Task<System.Nullable<bool>> UpdateRecipeAsync(Client.FoodManagerServiceReference.Recipe recipe, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/DeleteRecipe", ReplyAction="http://tempuri.org/IFoodManagerService/DeleteRecipeResponse")]
        System.Nullable<bool> DeleteRecipe(Client.FoodManagerServiceReference.Recipe recipe, string userToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFoodManagerService/DeleteRecipe", ReplyAction="http://tempuri.org/IFoodManagerService/DeleteRecipeResponse")]
        System.Threading.Tasks.Task<System.Nullable<bool>> DeleteRecipeAsync(Client.FoodManagerServiceReference.Recipe recipe, string userToken);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFoodManagerServiceChannel : Client.FoodManagerServiceReference.IFoodManagerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FoodManagerServiceClient : System.ServiceModel.ClientBase<Client.FoodManagerServiceReference.IFoodManagerService>, Client.FoodManagerServiceReference.IFoodManagerService {
        
        public FoodManagerServiceClient() {
        }
        
        public FoodManagerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FoodManagerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FoodManagerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FoodManagerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Client.FoodManagerServiceReference.Food[] GetFoodList(string userToken) {
            return base.Channel.GetFoodList(userToken);
        }
        
        public System.Threading.Tasks.Task<Client.FoodManagerServiceReference.Food[]> GetFoodListAsync(string userToken) {
            return base.Channel.GetFoodListAsync(userToken);
        }
        
        public System.Nullable<int> CreateFood(Client.FoodManagerServiceReference.Food food, string userToken) {
            return base.Channel.CreateFood(food, userToken);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<int>> CreateFoodAsync(Client.FoodManagerServiceReference.Food food, string userToken) {
            return base.Channel.CreateFoodAsync(food, userToken);
        }
        
        public System.Nullable<bool> UpdateFood(Client.FoodManagerServiceReference.Food food, string userToken) {
            return base.Channel.UpdateFood(food, userToken);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<bool>> UpdateFoodAsync(Client.FoodManagerServiceReference.Food food, string userToken) {
            return base.Channel.UpdateFoodAsync(food, userToken);
        }
        
        public System.Nullable<bool> DeleteFood(Client.FoodManagerServiceReference.Food food, string userToken) {
            return base.Channel.DeleteFood(food, userToken);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<bool>> DeleteFoodAsync(Client.FoodManagerServiceReference.Food food, string userToken) {
            return base.Channel.DeleteFoodAsync(food, userToken);
        }
        
        public System.Nullable<int> CreateRecipe(Client.FoodManagerServiceReference.Recipe recipe, string userToken) {
            return base.Channel.CreateRecipe(recipe, userToken);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<int>> CreateRecipeAsync(Client.FoodManagerServiceReference.Recipe recipe, string userToken) {
            return base.Channel.CreateRecipeAsync(recipe, userToken);
        }
        
        public Client.FoodManagerServiceReference.Recipe[] GetRecipesList(string userToken) {
            return base.Channel.GetRecipesList(userToken);
        }
        
        public System.Threading.Tasks.Task<Client.FoodManagerServiceReference.Recipe[]> GetRecipesListAsync(string userToken) {
            return base.Channel.GetRecipesListAsync(userToken);
        }
        
        public System.Nullable<bool> UpdateRecipe(Client.FoodManagerServiceReference.Recipe recipe, string userToken) {
            return base.Channel.UpdateRecipe(recipe, userToken);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<bool>> UpdateRecipeAsync(Client.FoodManagerServiceReference.Recipe recipe, string userToken) {
            return base.Channel.UpdateRecipeAsync(recipe, userToken);
        }
        
        public System.Nullable<bool> DeleteRecipe(Client.FoodManagerServiceReference.Recipe recipe, string userToken) {
            return base.Channel.DeleteRecipe(recipe, userToken);
        }
        
        public System.Threading.Tasks.Task<System.Nullable<bool>> DeleteRecipeAsync(Client.FoodManagerServiceReference.Recipe recipe, string userToken) {
            return base.Channel.DeleteRecipeAsync(recipe, userToken);
        }
    }
}

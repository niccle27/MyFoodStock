   M S B A M L   `   `   `  ���� A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Client.UserControlClient.UserControl  3clr-namespace:Client.ConverterClient.Converter  A    ;Client.UserControl.SubView.MainWindowSubView.MyFoodstockGUI   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:Client.UserControl     4	converterclr-namespace:Client.Converter       � 	Resources   �� 
TargetType �� Property �� Value��%J   '��       �� " ��.���  � ��$ 0,0,20,0q���  � ��$	 120��.����  �� Orientation$ Vertical=�.+��  � ColumnDefinitions �� $
��auto ��� $
��auto ��� $��* �.+��� -   $��
Categories��$��0�� �� DisplayMemberPath$
 Name������  �� Path$ ListFoodCategoryAndSubs� �� SelectedItem �� $ SelectedCategory�	 �� Mode$	 TwoWay=��� -   $��SubCategories��$��1��$	 Key������ 
 �� ElementName$
 
Categories��$ SelectedItem.SubCategory� �� $ SelectedSubCategory�$	 TwoWay=��� -   $��FoodListView��$��Visible=�$	��350������ $ 	ListFoods� �� *SelectedFood+$	 OneWayToSource=�  6   0Client.Converter.QuantityUnitMultiValueConverter #  QuantityUnitMultiValueConverter��%   &	           � ContextMenu �� .@�m� $��Remove�� m� Command �� $ RemoveFoodCommand� m� CommandParameter �� *SelectedFood+$	 OneWay=�m� $��Update�� �� $ UpdateFoodCommand� �� *SelectedFood+$	 OneWay=� �� View �� .P���  �� Width$
 auto��$
��Name�� �� DisplayMemberBinding �� $
 Name��� $
 auto��$��Quantity�� `�  `� 	Converter# [  .1��� $ Quantity��� $
 Unit��� $
 auto��$��Expiration Date�� �� $ ExpirationDate� �� StringFormat$ 
dd/MM/yyyy��
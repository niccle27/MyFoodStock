   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Client.UserControlClient.UserControl  5      3clr-namespace:Client.ConverterClient.Converter  5      A    ;Client.UserControl.SubView.MainWindowSubView.MyFoodstockGUI   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:Client.UserControl     5      4	converterclr-namespace:Client.Converter     5        � 	Resources  5
      (  "Client.Converter.CheckDateValidity   CheckDateValidity �� 
TargetType �� Property �� Value��%�   &	        '��        5   
   6&   �� 5   
   " ��6   .�5      ��  � ��$ 0,0,20,0q�6'   �� 5       � ��$	 120��6&   5      5      .��5      ��  �� Orientation$ Vertical=�6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   �� 5      $
��auto �6#   �� 5      $
��auto �6#   �� 5      $��* �6#   .+�5      �� $��0��6    d� 	IsChecked 6(   ��  �� Path$ IsShowAllCategoryChecked�.��5      �� .��6   ShowAllCategory6)   5      �� 5      $��1��6    6'   �� $' !IsShowOnlyOutdatedProductsChecked�.��5      �� .��6   Show only outdated products65   5      �� 5       -   5        $��
Categories��5       $��2��6)    �� DisplayMemberPath$
 Name��5"      ��5!      �� $ ListFoodCategoryAndSubs�	 �� SelectedItem	 5#      �� $ SelectedCategory�
 �� Mode$
 TwoWay=�{�5$      �� $ IsShowAllCategoryChecked�  InverseBooleanConverter �� 	Converter# [ �� 5%      -   5        $��SubCategories��5%      $��3��6,   $	 Key��5'      ��5&      ��  �� ElementName$ 
Categories��$ SelectedItem.SubCategory�	 5(      �� $ SelectedSubCategory�$
 TwoWay=�{�5)      �� $ IsShowAllCategoryChecked�# [ 5*      �� 5+   
   -   5        $��FoodListView��5+      $��Visible=�5,      $	��350��5-      ��5+   '   �� $ 	ListFoods�	 5.      �� *SelectedFood+$
 OneWayToSource=�  5/      6   0Client.Converter.QuantityUnitMultiValueConverter # QuantityUnitMultiValueConverter��%   &	         50      6<   51       � ContextMenu 52      �� 53      .@�54      m� $��Remove��6    m� Command 6/   �� $ RemoveFoodCommand� m� CommandParameter 6Z   �� *SelectedFood+$
 OneWay=�m� 55      $��Update��6    6/   �� $ UpdateFoodCommand� 6Z   �� *SelectedFood+$
 OneWay=�56       �� View 58      �� 59      .P�5:      ��  �� Width$
 auto��6%   $
��Name��62    �� DisplayMemberBinding 6@   �� $
 Name��� 5;      $
 auto��6%   $��Quantity��62    5<      `� 5=       `� 	Converter# [ 6+   .1�5>   "   �� $ Quantity�6*   �� 5?   "   $
 Unit�6*   5@      5B      �� 5C      $
 auto��6%   $��Expiration Date��62    6K   �� $ ExpirationDate� �� StringFormat$ 
dd/MM/yyyy��5D      5F      5G      5H      
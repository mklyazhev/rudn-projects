   M S B A M L   `   `   `  ����K  GPhonesRepository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PhonesRepositoryPhonesRepository  5   	   !    PhonesRepository.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:PhonesRepository     5   	   -   5          � Title$  Phones��5   	   $	��350��6   $	��800��6%   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      �� -   5         ��Name$ AddNewButton��5      $	��➕��6+    � ToolTip$0 *Добавить новый телефон��67   $��30��5      $��30��6    $��16(�6,   6:   �� 5      -   5        $ DeleteButton��5      $��🗑️��6+   $6 0Удалить выбранный телефон��69   $��30��5      $��30��6    $��16(�6,   $��5,0,0,0q�6:   5      5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��0.8* �6#   .+�5      �� $��0��6   ��6&   �� *
PhonesList+ �� SelectedItem 6I   �� *SelectedPhone+��5      �� 5       .R�5!      �� $��5q�6%   .+�5"      �� $��18(�6(   ��66   ��  �� Path$ Title��� 5#      ��6(   �� $ Company��� 5$      ��6(   �� $ Price�5%      5&      5(      �� 5*      $��1��6    � DataContext 6)   �� *SelectedPhone+.+�5+      �� $��16(�6   $
��Bold"�6*   $'��!Выбранный элемент��6<   �� 5,      $
��Bold"�6   $��Модель��6.   �� 5-      -   5        $��
PhoneTitle��5-      ��6,   �� *Title+�� 5.      $
��Bold"�6   $ ��Производитель��6.   �� 5/      -   5        $��PhoneCompany��5/      ��6.   �� *	Company+�� 50      $
��Bold"�6   $��Цена��6.   �� 51      -   5        $��
PhonePrice��51      ��6,   �� *Price+�� 52      -   5        $��Сохранить��52      $��0,10,0,0q�6F   53      54      55      56      
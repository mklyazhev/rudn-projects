   M S B A M L   `   `   `  ����I  ECarsRepository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CarsRepositoryCarsRepository  5   	       CarsRepository.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:CarsRepository     5   	   -   5          � Title$
  Cars��5   	   $	��400��6   $	��800��6#   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      �� -   5         ��Name$ FilterComboBox��5      $	��150��5      $��0,0,10,0q�6#   ��5      �� *
BrandsList+ �� SelectedItem 5      �� *SelectedBrand+5   #   �� 5      -   5        $	��➕��5       � ToolTip$. (Добавить новую машину��6!   $��30��5      $��30��6    6,   �� 5      -   5        $��🗑️��5      $4 .Удалить выбранную машину��6#   $��30��5      $��30��6    $��5,0,0,0q�6,   5      5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5       $��* �6#   �� 5!      $
��0.8* �6#   .+�5%      �� $��0��6   ��6&   �� *FilteredCarsList+ 6O   �� *SelectedCar+��5&      �� 5'      .R�5(      �� $��5q�6%   .+�5)      �� $��16(�6(   $
��Bold"�6R   ��66   ��  �� Path$ Brand�6R   �� 5*      ��6(   �� $ Model��� 5+      ��6(   �� $
 Year��� 5,      ��6(   �� $ Mileage� �� StringFormat$ Пробег: {0} км��5-      5.      50      �� 53      $��1��6   $��10q�6M   	 � DataContext	 6)   �� *SelectedCar+.+�54      �� $��Марка:��6   $
��Bold"�6*   �� 55      -   5        $��Brand��55      $��0,0,0,5q�6>   ��6'   �� *Brand+6>   �� 56      $��Модель:��6   $
��Bold"�6+   �� 57      -   5        $��Model��57      $��0,0,0,5q�6>   ��6'   �� *Model+6>   �� 58      $��Год выпуска:��6   $
��Bold"�60   �� 59      -   5        $
��Year��59      $��0,0,0,5q�6<   ��6&   �� *Year+6<   �� 5:      $��Пробег:��6   $
��Bold"�6+   �� 5;      -   5        $��Mileage��5;      $��0,0,0,10q�6B   ��6)   �� *	Mileage+6B   �� 5<      -	   5        $��Сохранить��5<      6-   5=      5>      5?      5@      